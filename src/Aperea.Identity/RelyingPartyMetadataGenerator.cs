using System;
using System.IO;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using Aperea.Identity.Settings;

namespace Aperea.Identity
{
    public class RelyingPartyMetadataGenerator : IRelyingPartyMetadataGenerator
    {
        readonly IMetadataContactSettings contactSettings;
        readonly IRelyingPartyConfiguration configuration;

        public RelyingPartyMetadataGenerator(IMetadataContactSettings contactSettings,
                                             IRelyingPartyConfiguration configuration)
        {
            this.contactSettings = contactSettings;
            this.configuration = configuration;
        }

        public string GenerateAsString()
        {
            return GenerateCore();
        }

        string CreateFederationMetadataAsString(EntityDescriptor entity)
        {
            var serializer = new MetadataSerializer();
            var stream = new MemoryStream();
            serializer.WriteMetadata(stream, entity);
            stream.Seek(0, SeekOrigin.Begin);
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        string GenerateCore()
        {
            var entity = new EntityDescriptor(new EntityId(configuration.IssuerUri));
            if (configuration.Sign)
            {
                entity.SigningCredentials = new X509SigningCredentials(configuration.SigningCertificate);
            }

            if (contactSettings.Contact != null)
            {
                entity.Contacts.Add(contactSettings.Contact);
                entity.Organization = contactSettings.Organization;
            }

            entity.RoleDescriptors.Add(GetTokenServiceDescriptor());
            return CreateFederationMetadataAsString(entity);
        }

        KeyDescriptor GetSingingKeyDescriptor(X509Certificate2 cert)
        {
            var clause = (new X509SecurityToken(cert)).CreateKeyIdentifierClause<X509RawDataKeyIdentifierClause>();
            var descriptor = new KeyDescriptor(new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[] {clause}));
            descriptor.Use = KeyType.Encryption;
            return descriptor;
        }

        SecurityTokenServiceDescriptor GetTokenServiceDescriptor()
        {
            var tokenService = new SecurityTokenServiceDescriptor();

            if (configuration.Encrypt)
            {
                tokenService.Keys.Add(GetSingingKeyDescriptor(configuration.EncryptionCertificate));
            }
            tokenService.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));
            foreach (DisplayClaim claim in configuration.GetClaimTypsRequested())
            {
                tokenService.ClaimTypesRequested.Add(claim);
            }
            AddSecurityTokenEndpoints(tokenService);
            AddTargetScopes(tokenService);
            AddPassiveEndpoints(tokenService);
            return tokenService;
        }

        void AddSecurityTokenEndpoints(SecurityTokenServiceDescriptor tokenService)
        {
            foreach (string item in configuration.GetSecurityTokenServiceEndpoints())
            {
                var endpoint = new EndpointAddress(item);
//                tokenService.SecurityTokenServiceEndpoints.Add(endpoint);
            }
        }

        void AddTargetScopes(SecurityTokenServiceDescriptor tokenService)
        {
            foreach (string item in configuration.GetPassiveRequestorEndpoints())
            {
                var endpoint = new EndpointAddress(item);
//                tokenService.TargetScopes.Add(endpoint);
            }
        }

        void AddPassiveEndpoints(SecurityTokenServiceDescriptor tokenService)
        {
            foreach (string item in configuration.GetPassiveRequestorEndpoints())
            {
                var endpoint = new EndpointAddress(item);
//                tokenService.PassiveRequestorEndpoints.Add(endpoint);
            }
        }
    }
}
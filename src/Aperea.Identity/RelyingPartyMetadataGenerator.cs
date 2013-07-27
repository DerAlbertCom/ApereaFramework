using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IO;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Aperea.Identity.Settings;

namespace Aperea.Identity
{
    [UsedImplicitly]
    public class RelyingPartyMetadataGenerator : IRelyingPartyMetadataGenerator
    {
        private readonly IMetadataContactSettings _contactSettings;
        private readonly IRelyingPartyConfiguration _configuration;

        public RelyingPartyMetadataGenerator(IMetadataContactSettings contactSettings,
            IRelyingPartyConfiguration configuration)
        {
            _contactSettings = contactSettings;
            _configuration = configuration;
        }

        public string GenerateAsString()
        {
            return GenerateCore();
        }

        private string CreateFederationMetadataAsString(EntityDescriptor entity)
        {
            var serializer = new MetadataSerializer();
            var stream = new MemoryStream();
            serializer.WriteMetadata(stream, entity);
            stream.Seek(0, SeekOrigin.Begin);
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        private string GenerateCore()
        {
            var entity = new EntityDescriptor(new EntityId(_configuration.IssuerUri));
            if (_configuration.Sign)
            {
                entity.SigningCredentials = new X509SigningCredentials(_configuration.SigningCertificate);
            }

            if (_contactSettings.Contact != null)
            {
                entity.Contacts.Add(_contactSettings.Contact);
                entity.Organization = _contactSettings.Organization;
            }

            entity.RoleDescriptors.Add(GetTokenServiceDescriptor());
            return CreateFederationMetadataAsString(entity);
        }

        private KeyDescriptor GetSingingKeyDescriptor(X509Certificate2 cert)
        {
            var clause = (new X509SecurityToken(cert)).CreateKeyIdentifierClause<X509RawDataKeyIdentifierClause>();
            var descriptor = new KeyDescriptor(new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[] {clause}))
            {
                Use = KeyType.Encryption
            };
            return descriptor;
        }

        private SecurityTokenServiceDescriptor GetTokenServiceDescriptor()
        {
            var tokenService = new SecurityTokenServiceDescriptor();

            if (_configuration.Encrypt)
            {
                tokenService.Keys.Add(GetSingingKeyDescriptor(_configuration.EncryptionCertificate));
            }
            tokenService.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));
            foreach (DisplayClaim claim in _configuration.GetClaimTypsRequested())
            {
                tokenService.ClaimTypesRequested.Add(claim);
            }
            AddSecurityTokenEndpoints(tokenService);
            AddTargetScopes(tokenService);
            AddPassiveEndpoints(tokenService);
            return tokenService;
        }

        private void AddSecurityTokenEndpoints(SecurityTokenServiceDescriptor tokenService)
        {
            foreach (string item in _configuration.GetSecurityTokenServiceEndpoints())
            {
                tokenService.SecurityTokenServiceEndpoints.Add(new EndpointReference(item));
            }
        }

        private void AddTargetScopes(SecurityTokenServiceDescriptor tokenService)
        {
            foreach (string item in _configuration.GetPassiveRequestorEndpoints())
            {
                tokenService.TargetScopes.Add(new EndpointReference(item));
            }
        }

        private void AddPassiveEndpoints(SecurityTokenServiceDescriptor tokenService)
        {
            foreach (string item in _configuration.GetPassiveRequestorEndpoints())
            {
                tokenService.PassiveRequestorEndpoints.Add(new EndpointReference(item));
            }
        }
    }
}
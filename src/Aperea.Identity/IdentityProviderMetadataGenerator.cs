using System;
using System.IO;
using System.IdentityModel.Metadata;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;
using Aperea.Identity.Settings;

namespace Aperea.Identity
{
    public class IdentityProviderMetadataGenerator : IIdentityProviderMetadataGenerator
    {
        readonly IMetadataContactSettings _contactSettings;
        readonly ISigningSettings _signingSettings;
        readonly IEncryptionSettings _encryptionSettings;
        readonly IIdentityProviderConfiguration _configuration;


        public IdentityProviderMetadataGenerator(IMetadataContactSettings contactSettings,
                                                 ISigningSettings signingSettings,
                                                 IEncryptionSettings encryptionSettings,
                                                 IIdentityProviderConfiguration configuration)
        {
            _contactSettings = contactSettings;
            _signingSettings = signingSettings;
            _encryptionSettings = encryptionSettings;
            _configuration = configuration;
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
            var entity = new EntityDescriptor(new EntityId(_configuration.IssuerUri));
            if (_signingSettings.Sign)
            {
                entity.SigningCredentials = new X509SigningCredentials(_signingSettings.Certificate);
            }

            if (_contactSettings.Contact != null)
            {
                entity.Contacts.Add(_contactSettings.Contact);
                entity.Organization = _contactSettings.Organization;
            }

            entity.RoleDescriptors.Add(GetTokenServiceDescriptor());
            return CreateFederationMetadataAsString(entity);
        }

        KeyDescriptor GetSingingKeyDescriptor(X509Certificate2 cert)
        {
            var clause = (new X509SecurityToken(cert)).CreateKeyIdentifierClause<X509RawDataKeyIdentifierClause>();
            var descriptor = new KeyDescriptor(new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[] {clause}));
            descriptor.Use = KeyType.Signing;
            return descriptor;
        }

        SecurityTokenServiceDescriptor GetTokenServiceDescriptor()
        {
            var tokenService = new SecurityTokenServiceDescriptor {ServiceDescription = _configuration.ServiceName};
            tokenService.Keys.Add(GetSingingKeyDescriptor(_signingSettings.Certificate));
            tokenService.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));
            tokenService.TokenTypesOffered.Add(new Uri("urn:oasis:names:tc:SAML:1.0:assertion"));
            foreach (DisplayClaim claim in _configuration.Claims)
            {
                tokenService.ClaimTypesOffered.Add(claim);
            }

            AddActiceEndpoints(tokenService);

            bool addPassiveEndpointsAsActive = tokenService.SecurityTokenServiceEndpoints.Count == 0;
            AddPassiveEndpoints(tokenService, addPassiveEndpointsAsActive);
            return tokenService;
        }

        void AddPassiveEndpoints(SecurityTokenServiceDescriptor tokenService, bool addPassiveEndpointsAsActive)
        {
            foreach (string item in _configuration.PassiveEndpoints)
            {
                var endpoint = new EndpointReference("");
                tokenService.PassiveRequestorEndpoints.Add(endpoint);
                if (addPassiveEndpointsAsActive)
                {
                    tokenService.SecurityTokenServiceEndpoints.Add(endpoint);
                }
            }
        }

        void AddActiceEndpoints(SecurityTokenServiceDescriptor tokenService)
        {
            foreach (string uri in _configuration.ActiveEndpoints)
            {
                var set = new MetadataSet();
                var metadata = new MetadataReference(new EndpointAddress(string.Format("{0}/mex", uri)),
                                                     AddressingVersion.WSAddressing10);
                var item = new MetadataSection(MetadataSection.MetadataExchangeDialect, null, metadata);
                set.MetadataSections.Add(item);
                var sb = new StringBuilder();
                var w = new StringWriter(sb);
                var writer = new XmlTextWriter(w);
                set.WriteTo(writer);
                writer.Flush();
                w.Flush();
                var input = new StringReader(sb.ToString());
                var reader = new XmlTextReader(input);

                XmlDictionaryReader metadataReader = XmlDictionaryReader.CreateDictionaryReader(reader);
                var address = new EndpointReference(uri)
                                  {
//                                      Details = metadataReader.
                                  };
  //                  , null, null, metadataReader, null);
                tokenService.SecurityTokenServiceEndpoints.Add(address);
            }
        }
    }
}
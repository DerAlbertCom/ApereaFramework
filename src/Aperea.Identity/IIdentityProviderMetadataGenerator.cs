using System;

namespace Aperea.Identity
{
    public interface IIdentityProviderMetadataGenerator
    {
        string GenerateAsString();
    }
}
using System.Collections.Generic;
using Microsoft.IdentityModel.Protocols.WSIdentity;

namespace Aperea.Identity
{
    public interface IIdentityProviderConfiguration
    {
        string IssuerUri { get;  }
        string ServiceName { get;  }
        IEnumerable<string> ActiveEndpoints { get; }
        IEnumerable<string> PassiveEndpoints { get; }
        IEnumerable<DisplayClaim> Claims { get; }
    }
}
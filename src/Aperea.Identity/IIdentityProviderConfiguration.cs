using System.Collections.Generic;
using System.IdentityModel.Metadata;

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
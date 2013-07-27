using System.IdentityModel.Protocols.WSTrust;
using System.ServiceModel;

namespace Aperea.Identity
{
    public static class EndpointExtensions
    {
        public static EndpointReference ToEndpointReference(this EndpointAddress address)
        {
            return new EndpointReference(address.Uri.ToString());
        }
    }
}
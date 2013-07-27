using System;
using System.IdentityModel.Protocols.WSTrust;
using System.ServiceModel;

namespace Aperea.Identity.Configuration
{
    public interface IRelyingPartyClientConfiguration 
    {
        EndpointAddress GetStsEndpoint();
        EndpointAddress GetEndpointFor<T>();
        EndpointReference GetEndpointReferenceFor<T>();
    }
}
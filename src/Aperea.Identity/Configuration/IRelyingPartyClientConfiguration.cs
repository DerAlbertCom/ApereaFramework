using System;
using System.ServiceModel;

namespace Aperea.Identity.Configuration
{
    public interface IRelyingPartyClientConfiguration 
    {
        EndpointAddress GetStsEndpoint();
        EndpointAddress GetEndpointFor<T>();
    }
}
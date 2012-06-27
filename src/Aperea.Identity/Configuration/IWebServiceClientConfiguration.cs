using System;
using System.ServiceModel;

namespace Aperea.Identity.Configuration
{
    public interface IWebServiceClientConfiguration
    {
        EndpointAddress GetEndpoint();
    }
}
using System;

namespace Aperea.Identity.Configuration
{
    public interface IRelyingPartyClientConfigurator
    {
        IRelyingPartyClientConfigurator ConfigureService<T>(Action<IWebServiceClientConfigurator> serviceConfiguration);
        IRelyingPartyClientConfigurator StsEndpoint(string endpointAddresse);
    }
}
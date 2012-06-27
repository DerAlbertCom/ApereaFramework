using System;

namespace Aperea.Identity.Configuration
{
    public interface IWebServiceServerConfiguration 
    {
        Type ServiceType { get; }
    }
}
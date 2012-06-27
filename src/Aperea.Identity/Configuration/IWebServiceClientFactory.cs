using System;

namespace Aperea.Identity.Configuration
{
    public interface IWebServiceClientFactory
    {
        void CreateUserToken<T>(string username, string password);
        T CreateChannel<T>();
    }
}
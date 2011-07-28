using System;
using Raven.Client;

namespace Aperea.Infrastructure.Data
{
    public interface IDocumentSessionFactory : IDisposable
    {
        IDocumentSession CreateDocumentSession();
        IDocumentStore DocumentStore { get; }
    }
}
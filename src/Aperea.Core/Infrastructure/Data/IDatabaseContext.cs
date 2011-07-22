using System;
using Raven.Client;

namespace Aperea.Infrastructure.Data
{
    public interface IDatabaseContext : IDisposable
    {
        IDocumentSession DbContext { get; }
    }
}
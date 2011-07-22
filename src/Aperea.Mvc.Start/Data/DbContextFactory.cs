using System;
using System.Data.Entity;
using Aperea.Infrastructure.Data;
using Raven.Client;
using Raven.Client.Document;

namespace ApereaStart.Data
{
    public class DbContextFactory : IDbContextFactory
    {
        public IDocumentSession CreateDbContext()
        {
            return new DocumentStore().OpenSession("Foobar");
        }
    }
}
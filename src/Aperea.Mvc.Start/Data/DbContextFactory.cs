using System;
using System.Data.Entity;
using Aperea.Infrastructure.Data;

namespace ApereaStart.Data
{
    public class DbContextFactory : IDbContextFactory
    {
        public DbContext CreateDbContext()
        {
            return new DbEntities();
        }
    }
}
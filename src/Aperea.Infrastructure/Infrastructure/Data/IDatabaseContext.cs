using System;
using System.Data.Entity;

namespace Aperea.Infrastructure.Data
{
    public interface IDatabaseContext : IDisposable
    {
        DbContext CreateDbContext();
    }
}
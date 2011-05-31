using System.Data.Entity;

namespace Aperea.Infrastructure.Data
{
    public interface IDbContextFactory
    {
        DbContext CreateDbContext();
    }
}
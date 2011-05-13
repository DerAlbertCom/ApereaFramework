using System;
using System.Data.Entity;

namespace Aperea.Infrastructure.Data
{
    public class EntityDatabaseContext : IDatabaseContext
    {
        private readonly Lazy<DbContext> _context;

        public EntityDatabaseContext(IDbContextFactory contextFactory)
        {
            _context = new Lazy<DbContext>(() => contextFactory.Context);
        }

        public DbContext CreateDbContext()
        {
            return _context.Value;
        }

        public void Dispose()
        {
            if (_context.IsValueCreated)
            {
                _context.Value.Dispose();
            }
        }
    }
}
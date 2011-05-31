using System;
using System.Data.Entity;

namespace Aperea.Infrastructure.Data
{
    public class DatabaseContext : IDatabaseContext
    {
        readonly Lazy<DbContext> _context;

        public DatabaseContext(IDbContextFactory contextFactory)
        {
            _context = new Lazy<DbContext>(contextFactory.CreateDbContext);
        }

        public DbContext DbContext
        {
            get { return _context.Value; }
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
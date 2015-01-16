using System;
using System.Data.Entity;

namespace Aperea.Data
{
    public class DatabaseContext : IDatabaseContext
    {
        Lazy<DbContext> dbContext;

        public DatabaseContext(IDbContextFactory cbContextfactory)
        {
            dbContext = new Lazy<DbContext>(cbContextfactory.Create);
        }

        public DbContext DbContext
        {
            get
            {
#if DEBUG
                if (dbContext == null)
                    throw new InvalidOperationException(
                        "the db context is null, it seems that the DbContext is access directly on a view, this is forbidden. Please use the Controller to gather all data for the view");
#endif
                return dbContext.Value;
            }
        }

        public void Dispose()
        {
            if (dbContext == null) return;

            if (dbContext.IsValueCreated)
            {
                dbContext.Value.Dispose();
            }
            dbContext = null;
        }
    }
}

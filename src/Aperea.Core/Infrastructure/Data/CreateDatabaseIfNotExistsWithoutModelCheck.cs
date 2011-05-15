using System.Data.Entity;
using System.Transactions;

namespace Aperea.Infrastructure.Data
{
    public class CreateDatabaseIfNotExistsWithoutModelCheck<TContext> : IDatabaseInitializer<TContext>
        where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
            bool databaseExists;

            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                databaseExists = context.Database.Exists();
            }

            if (databaseExists)
            {
                return;
            }
            context.Database.Create();
            Seed(context);
            context.SaveChanges();
        }

        protected virtual void Seed(TContext context)
        {
        }
    }
}
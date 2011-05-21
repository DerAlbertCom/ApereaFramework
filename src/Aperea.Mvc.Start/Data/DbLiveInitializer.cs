using Aperea.Data;
using Aperea.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace ApereaStart.Data
{
    public class DbLiveInitializer : CreateDatabaseIfNotExistsWithoutModelCheck<DbEntities>
    {
        protected override void Seed(DbEntities context)
        {
            base.Seed(context);
            DatabaseSeeder.SeedDatabase();
        }
    }
}
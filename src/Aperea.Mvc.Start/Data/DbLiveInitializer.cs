using Aperea.Data;
using Aperea.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace ApereaStart.Data
{
    public class ApereaStartDbLiveInitializer : CreateDatabaseIfNotExistsWithoutModelCheck<ApereaStartDbEntities>
    {
        protected override void Seed(ApereaStartDbEntities context)
        {
            base.Seed(context);
            DatabaseSeeder.SeedDatabase();
        }
    }
}
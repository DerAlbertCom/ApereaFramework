using System.Data.Entity;
using Aperea.Data;
using Microsoft.Practices.ServiceLocation;

namespace ApereaStart.Data
{
    public class ApereaStartDbDevInitializer : DropCreateDatabaseIfModelChanges<ApereaStartDbEntities>
    {
        protected override void Seed(ApereaStartDbEntities context)
        {
            base.Seed(context);
            DatabaseSeeder.SeedDatabase();
        }
    }
}
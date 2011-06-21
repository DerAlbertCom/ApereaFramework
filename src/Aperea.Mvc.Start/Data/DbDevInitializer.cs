using System.Data.Entity;
using Aperea.Data;

namespace ApereaStart.Data
{
    public class DbDevInitializer : DropCreateDatabaseIfModelChanges<DbEntities>
    {
        protected override void Seed(DbEntities context)
        {
            base.Seed(context);
            DatabaseSeeder.SeedDatabase();
        }
    }
}
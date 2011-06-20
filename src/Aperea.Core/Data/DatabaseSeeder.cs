using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase()
        {
            var seeder = ServiceLocator.Current.GetAllInstances<IDatabaseSeeder>().OrderBy(s => s.Order);
            foreach (var databaseSeeder in seeder)
            {
                databaseSeeder.Seed();
            }
        }
    }
}
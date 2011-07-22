using Aperea.Data;
using Aperea.Infrastructure.Bootstrap;
using Raven.Client.Document;

namespace Aperea.Initialize
{
    public class DatabaseInitialiser : IBootstrapItem
    {
        public void Execute()
        {
            DatabaseSeeder.SeedDatabase();

        }

        public int Order
        {
            get { return -1; }
        }
    }
}
using Aperea.Data;
using Aperea.Infrastructure.Bootstrap;
using Aperea.Infrastructure.Data;
using Raven.Client.Document;

namespace Aperea.Initialize
{
    public class DatabaseInitializer : IBootstrapItem
    {
        IDocumentSessionFactory _factory;

        public DatabaseInitializer(IDocumentSessionFactory factory)
        {
            _factory = factory;
        }

        public void Execute()
        {
            DatabaseSeeder.SeedDatabase();
        }

        public int Order
        {
            get { return -1; }
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}
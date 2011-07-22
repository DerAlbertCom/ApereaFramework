using Raven.Client;
using Raven.Client.Document;

namespace Aperea.Infrastructure.Data
{
    public class DocumentSessionFactory : IDocumentSessionFactory
    {
        readonly DocumentStore _documentStore;

        public DocumentSessionFactory()
        {
            _documentStore = new DocumentStore()
                             {
                                 Url = "http://localhost:8080"
                             };

            _documentStore.Conventions.JsonContractResolver = new ApereaContractResolver(true);

            _documentStore.Initialize();
        }

        public IDocumentSession CreateDbContext()
        {
            return _documentStore.OpenSession("Foobar");
        }
    }
}
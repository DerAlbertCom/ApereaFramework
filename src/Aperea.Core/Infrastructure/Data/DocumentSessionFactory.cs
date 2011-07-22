using Raven.Client;
using Raven.Client.Document;

namespace Aperea.Infrastructure.Data
{
    public class DocumentSessionFactory : IDocumentSessionFactory
    {
        readonly DocumentStore _documentStore;

        public DocumentSessionFactory()
        {
            _documentStore = new DocumentStore();
            _documentStore.ConnectionStringName = "RavenDb";
            _documentStore.Conventions.JsonContractResolver = new PrivateSetterContractResolver(true);
            _documentStore.Initialize();
        }

        public IDocumentSession CreateDocumentSession()
        {
            return _documentStore.OpenSession("Foobar");
        }
    }
}
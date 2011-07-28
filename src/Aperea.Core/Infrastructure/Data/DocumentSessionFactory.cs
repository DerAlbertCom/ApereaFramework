using System;
using Aperea.Settings;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace Aperea.Infrastructure.Data
{
    public class DocumentSessionFactory : IDocumentSessionFactory
    {
        readonly IDatabaseSettings _settings;
        readonly Lazy<DocumentStore> _lazyStore;

        public DocumentSessionFactory(IDatabaseSettings settings)
        {
            _settings = settings;
            _lazyStore = new Lazy<DocumentStore>(CreateDocumentStore);
        }

        DocumentStore CreateDocumentStore()
        {
            var store = new DocumentStore
                        {
                            ConnectionStringName = _settings.ConnectionStringName,
                            Conventions = {JsonContractResolver = new PrivateSetterContractResolver(true)}
                        };
            store.Initialize();
            store.DatabaseCommands.EnsureDatabaseExists(_settings.DatabaseName);
            return store;
        }

        public IDocumentSession CreateDocumentSession()
        {
            return _lazyStore.Value.OpenSession(_settings.DatabaseName);
        }
            
        public IDocumentStore DocumentStore
        {
            get { return _lazyStore.Value; }
        }

        public void Dispose()
        {
            if (_lazyStore.IsValueCreated)
            {
                _lazyStore.Value.Dispose();
            }
        }
    }
}
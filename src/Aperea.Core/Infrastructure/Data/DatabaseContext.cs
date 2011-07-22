using System;
using Raven.Client;

namespace Aperea.Infrastructure.Data
{
    public class DatabaseContext : IDatabaseContext
    {
        readonly Lazy<IDocumentSession> _context;

        public DatabaseContext(IDocumentSessionFactory contextFactory)
        {
            _context = new Lazy<IDocumentSession>(contextFactory.CreateDocumentSession);
        }

        public IDocumentSession DbContext
        {
            get { return _context.Value; }
        }

        public void Dispose()
        {
            if (_context.IsValueCreated)
            {
                _context.Value.Dispose();
            }
        }
    }
}
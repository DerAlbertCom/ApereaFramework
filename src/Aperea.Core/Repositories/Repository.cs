using System;
using System.Linq;
using System.Linq.Expressions;
using Aperea.Infrastructure.Data;
using Raven.Client.Linq;

namespace Aperea.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        readonly IDatabaseContext _databaseContext;


        public Repository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path)
        {
            return Set;
        }

        public IQueryable<T> Include(params string[] paths)
        {
            return Set;
        }

        internal IRavenQueryable<T> Set
        {
            get { return _databaseContext.DbContext.Query<T>(); }
        }

        public IQueryable<T> Entities
        {
            get { return Set; }
        }

        public void Add(T entity)
        {
            _databaseContext.DbContext.Store(entity);
        }

        public void Remove(T entity)
        {
            _databaseContext.DbContext.Delete(entity);
        }

        public void SaveAllChanges()
        {
            _databaseContext.DbContext.SaveChanges();
        }
    }
}
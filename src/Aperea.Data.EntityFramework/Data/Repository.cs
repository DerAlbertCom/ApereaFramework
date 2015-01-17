using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aperea.Data
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        readonly IDatabaseContext context;

        readonly Lazy<DbSet<T>> database;

        public Repository(IDatabaseContext context)
        {
            this.context = context;
            database = new Lazy<DbSet<T>>(() => context.DbContext.Set<T>(), isThreadSafe: true);
        }

        public IQueryable<T> Entities
        {
            get { return database.Value; }
        }

        public void Add(T entity)
        {
            database.Value.Add(entity);
        }

        public void Remove(T entity)
        {
            database.Value.Remove(entity);
        }

        public IQueryable<T> Include<TProperty>(params Expression<Func<T, TProperty>>[] expressions)
        {
            DbQuery<T> query = database.Value;
            foreach (var expression in expressions)
            {
                return database.Value.Include(expression);
            }
            return query;
        }

        public void Update(T person)
        {
            context.DbContext.Entry(person).State = EntityState.Modified;
        }

        public Task<int> SaveAllChangesAsync()
        {
            return context.DbContext.SaveChangesAsync();
        }

        public IQueryable<T> Include(params string[] paths)
        {
            DbQuery<T> query = database.Value;
            foreach (string path in paths)
            {
                query = query.Include(path);
            }
            return query;
        }

        public void SaveAllChanges()
        {
            context.DbContext.SaveChanges();
        }
    }
}
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Aperea.Infrastructure.Data;

namespace Aperea.Repositories
{

    public class Repository<T> : IRepository<T>
        where T : class
    {
        readonly IDatabaseContext _databaseContext;

        DbSet<T> _objectSet;

        public Repository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path)
        {
            return Set.Include(path);
        }

        public IQueryable<T> Include(params string[] paths)
        {
            DbQuery<T> query = Set;
            foreach (var path in paths) {
                query = query.Include(path);
            }
            return query;
        }

        internal DbSet<T> Set
        {
            get
            {
                if (_objectSet == null) {
                    _objectSet = _databaseContext.CreateDbContext().Set<T>();
                }
                return _objectSet;
            }
        }

        public IQueryable<T> Entities
        {
            get { return Set; }
        }

        public void Add(T entity)
        {
            Set.Add(entity);
        }

        public void Remove(T entity)
        {
            Set.Remove(entity);
        }

        public void SaveAllChanges()
        {
            try {
                _databaseContext.CreateDbContext().ChangeTracker.DetectChanges();
                _databaseContext.CreateDbContext().SaveChanges();
            }
            catch (DbEntityValidationException e) {
                var sb = new StringBuilder();
                sb.AppendLine();
                foreach (var error in e.EntityValidationErrors) {
                    sb.AppendLine("Entity: " + error. Entry.Entity.GetType());
                    foreach (var validationError in error.ValidationErrors)
                    {
                        sb.AppendFormat("  {1} {0}", validationError.ErrorMessage, validationError.PropertyName);
                        sb.AppendLine();
                    }
                    sb.AppendLine();
                }
                throw new InvalidDataException(sb.ToString(), e);
            }
        }
    }
}
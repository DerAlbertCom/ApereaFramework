using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aperea.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }
        void Add(T entity);
        void Remove(T entity);
        void SaveAllChanges();
        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path);
        IQueryable<T> Include(params string[] paths);
    }
}
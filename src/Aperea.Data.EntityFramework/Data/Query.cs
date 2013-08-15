using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Aperea.Data
{
    public class Query<T> : IQuery<T> where T : class
    {
        private readonly IRepository<T> repository;

        public Query(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return repository.Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression
        {
            get { return repository.Entities.Expression; }
        }

        public Type ElementType
        {
            get { return repository.Entities.ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return repository.Entities.Provider; }
        }
    }
}
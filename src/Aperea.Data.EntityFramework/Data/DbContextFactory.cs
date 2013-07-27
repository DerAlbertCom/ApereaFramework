using System;
using System.Data.Entity;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Aperea.Data
{
    [UsedImplicitly]
    public sealed class DbContextFactory : IDbContextFactory
    {
        readonly IContainer _container;
        static Type _dbContextType;

        public DbContextFactory(IContainer container)
        {
            _container = container;
        }

        public DbContext Create()
        {
            if (_dbContextType == null)
            {
                throw new InvalidOperationException(
                    "DbContextType is not set, use DbContextFactory.SetDbContextType<T>() before accessing the data");
            }
            return (DbContext) _container.TryGetInstance(_dbContextType) ??
                      (DbContext) Activator.CreateInstance(_dbContextType);
        }

        public static void SetDbContextType<T>() where T : DbContext
        {
            _dbContextType = typeof (T);
        }
    }
}
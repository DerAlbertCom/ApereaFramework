using System;
using System.Data.Entity;

namespace Aperea.Data
{
    [UsedImplicitly]
    public sealed class DbContextFactory : IDbContextFactory
    {
        static Type _dbContextType;

        public DbContext Create()
        {
            if (_dbContextType == null)
            {
                throw new InvalidOperationException(
                    "DbContextType is not set, use DbContextFactory.SetDbContextType<T>() before accessing the data");
            }
            return (DbContext) Activator.CreateInstance(_dbContextType);
        }

        public static void SetDbContextType<T>() where T : DbContext
        {
            _dbContextType = typeof (T);
        }
    }
}
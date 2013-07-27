using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Aperea.Data.Configuration
{
    public abstract class EntityConfigurationBase<T> : IEntityConfiguration<T> where T : class
    {
        public EntityTypeConfiguration<T> Configure(DbModelBuilder modelBuilder)
        {
            Configure(modelBuilder.Entity<T>());
            return modelBuilder.Entity<T>();
        }

        protected abstract void Configure(EntityTypeConfiguration<T> configuration);
    }
}
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Aperea.Data.Configuration
{
    public interface IEntityConfiguration<T> where T : class
    {
        EntityTypeConfiguration<T> Configure(DbModelBuilder modelBuilder);
    }

    public interface IEntityConfiguration
    {
        void Configure(DbModelBuilder modelBuilder);
    }
}
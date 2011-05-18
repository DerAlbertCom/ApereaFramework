using System.Data.Entity;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Data
{
    public abstract class ApereaDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var modelBuilders = ServiceLocator.Current.GetAllInstances<IDatabaseModelBuilder>();
            foreach (var builder in modelBuilders)
            {
                builder.ModelCreating(modelBuilder);
            }
        }
    }
}
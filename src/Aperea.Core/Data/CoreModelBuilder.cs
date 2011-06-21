using System.Data.Entity;
using Aperea.EntityModels;

namespace Aperea.Data
{
    public class CoreModelBuilder : IDatabaseModelBuilder
    {
        public void ModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RemoteAction>();
            modelBuilder.Entity<SystemLanguage>();
            modelBuilder.Entity<Module>();
        }
    }
}
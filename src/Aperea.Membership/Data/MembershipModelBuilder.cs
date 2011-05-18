using System;
using System.Data.Entity;
using Aperea.EntityModels;

namespace Aperea.Data
{
    public class MembershipModelBuilder : IDatabaseModelBuilder
    {
        public void ModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SecurityRole>();
            modelBuilder.Entity<LoginGroup>().HasMany(g => g.Roles).WithMany();
            modelBuilder.Entity<Login>().HasMany(l => l.Groups).WithMany();
        }
    }
}
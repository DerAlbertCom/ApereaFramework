using System;
using System.Data.Entity;
using Aperea.EntityModels;

namespace Aperea.Data
{
    public class MailModelBuilder : IDatabaseModelBuilder
    {
        public void ModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailTemplate>();
        }
    }
}
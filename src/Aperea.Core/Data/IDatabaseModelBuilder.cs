using System;
using System.Data.Entity;

namespace Aperea.Data
{
    public interface IDatabaseModelBuilder
    {
        void ModelCreating(DbModelBuilder modelBuilder);
    }
}
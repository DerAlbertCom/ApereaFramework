using System;
using System.Data.Entity;

namespace Aperea.Data
{
    public interface IDbContextFactory
    {
        DbContext Create();
    }
}
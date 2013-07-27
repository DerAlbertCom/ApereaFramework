using System;
using System.Data.Entity;

namespace Aperea.Data
{
    public interface IDatabaseContext : IDisposable
    {
        DbContext DbContext { get; }
    }
}
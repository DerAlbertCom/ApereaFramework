using System;

namespace Aperea.Data
{
    public interface IDatabaseSeeder
    {
        void Seed();
        int Order { get; }
        int Migrate(int version);
    }
}
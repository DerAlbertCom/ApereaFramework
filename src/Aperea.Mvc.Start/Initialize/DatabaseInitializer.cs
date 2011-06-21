using System.Data.Entity;
using Aperea.Infrastructure.Bootstrap;
using ApereaStart.Data;

namespace ApereaStart.Initialize
{
    public class DatabaseInitializer : IBootstrapItem
    {
        public void Execute()
        {
            Database.SetInitializer(new DbDevInitializer());
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
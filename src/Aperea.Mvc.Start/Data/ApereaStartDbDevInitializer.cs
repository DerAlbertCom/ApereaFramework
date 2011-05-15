using System.Data.Entity;

namespace ApereaStart.Data
{
    public class ApereaStartDbDevInitializer : DropCreateDatabaseIfModelChanges<ApereaStartEntities>
    {
        protected override void Seed(ApereaStartEntities context)
        {
            base.Seed(context);
        }
    }
}
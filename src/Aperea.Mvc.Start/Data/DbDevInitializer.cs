using System.Data.Entity;

namespace ApereaStart.Data
{
    public class ApereaStartDbDevInitializer : DropCreateDatabaseIfModelChanges<ApereaStartDbEntities>
    {
        protected override void Seed(ApereaStartDbEntities context)
        {
            base.Seed(context);
        }
    }
}
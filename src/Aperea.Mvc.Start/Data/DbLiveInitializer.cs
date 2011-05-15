using Aperea.Infrastructure.Data;

namespace ApereaStart.Data
{
    public class ApereaStartDbLiveInitializer : CreateDatabaseIfNotExistsWithoutModelCheck<ApereaStartDbEntities>
    {
        protected override void Seed(ApereaStartDbEntities context)
        {
            base.Seed(context);
        }
    }
}
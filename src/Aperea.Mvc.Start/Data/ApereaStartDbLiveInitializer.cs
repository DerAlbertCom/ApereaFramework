using Aperea.Infrastructure.Data;

namespace ApereaStart.Data
{
    public class ApereaStartDbLiveInitializer : CreateDatabaseIfNotExistsWithoutModelCheck<ApereaStartEntities>
    {
        protected override void Seed(ApereaStartEntities context)
        {
            base.Seed(context);
        }
    }
}
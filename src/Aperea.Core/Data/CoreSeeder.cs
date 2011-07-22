using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;
using Aperea.Settings;

namespace Aperea.Data
{
    public class CoreSeeder : IDatabaseSeeder
    {
        private const int CurrentVersion = 1;
        readonly IRepository<SystemLanguage> _repository;
        readonly ICultureSettings _settings;

        public CoreSeeder(IRepository<SystemLanguage> repository, ICultureSettings settings)
        {
            _repository = repository;
            _settings = settings;
        }

        public void Seed()
        {
            Migrate(0);
        }

        public int Order
        {
            get { return -30000; }
        }

        public int Migrate(int version)
        {
            if (version < 1)
            {
                if (!_repository.Entities.Any(sl => sl.Culture == _settings.DefaultCulture))
                {
                    _repository.Add(new SystemLanguage(_settings.DefaultCulture));
                    _repository.SaveAllChanges();
                }
            }

            return CurrentVersion;
        }
    }
}
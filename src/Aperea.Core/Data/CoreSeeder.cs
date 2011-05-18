using System;
using Aperea.EntityModels;
using Aperea.Repositories;
using Aperea.Settings;

namespace Aperea.Data
{
    public class CoreSeeder : IDatabaseSeeder
    {
        readonly IRepository<SystemLanguage> _repository;
        readonly ICultureSettings _settings;

        public CoreSeeder(IRepository<SystemLanguage> repository, ICultureSettings settings)
        {
            _repository = repository;
            _settings = settings;
        }

        public void Seed()
        {
            _repository.Add(new SystemLanguage(_settings.DefaultCulture));
            _repository.SaveAllChanges();
        }

        public int Order
        {
            get { return -30000; }
        }
    }
}
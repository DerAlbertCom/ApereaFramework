using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aperea.Repositories;
using Machine.Fakes;

namespace Aperea.Specs.Repositories
{
    public class FakeRepository<T>
        where T : class
    {
        private readonly IFakeAccessor _fakeAccessor;
        private readonly List<T> _entities = new List<T>();
        private readonly List<T> _addEntities = new List<T>();
        private readonly List<T> _removeEntities = new List<T>();

        public FakeRepository(IFakeAccessor fakeAccessor)
        {
            IdGeneration.ResetId(typeof (T));
            _fakeAccessor = fakeAccessor;
            SetRepositoryBehavior();
        }

        public FakeRepository(IFakeAccessor fakeAccessor, IEnumerable<T> existings)
        {
            _fakeAccessor = fakeAccessor;
            _entities.AddRange(existings);
            SetRepositoryBehavior();
        }

        private void SetRepositoryBehavior()
        {
            GetRepositoryFake()
                .WhenToldTo(r => r.Entities)
                .Return(_entities.AsQueryable());

            GetRepositoryFake()
                .WhenToldTo(r => r.Add(Param<T>.IsNotNull))
                .Callback<T>(login => _addEntities.Add(login));

            GetRepositoryFake()
                .WhenToldTo(r => r.Remove(Param<T>.IsNotNull))
                .Callback<T>(login => _removeEntities.Remove(login));

            GetRepositoryFake()
                .WhenToldTo(r => r.SaveAllChanges())
                .Callback(() =>
                              {
                                  SetIds(_addEntities);
                                  _entities.AddRange(_addEntities);
                                  _addEntities.Clear();
                                  _removeEntities.All(entity => _entities.Remove(entity));
                                  _removeEntities.Clear();
                              });
        }

        private void SetIds(IEnumerable<T> addEntities)
        {
            foreach (var addEntity in addEntities)
            {
                SetId(addEntity);
            }
        }


        private static void SetId(T addEntity)
        {
            var property = addEntity.GetType().GetProperty("Id");
            if (property == null)
                return;
            var id = (int) property.GetValue(addEntity, null);
            if (id != 0)
            {
                throw new InvalidDataException(string.Format("The entity {0} can only added once to the repository",
                                                             addEntity));
            }
            property.SetValue(addEntity, IdGeneration.GetNextId(addEntity.GetType()), null);
        }

        private IRepository<T> GetRepositoryFake()
        {
            return _fakeAccessor.The<IRepository<T>>();
        }

        public T this[int index]
        {
            get { return _entities[index]; }
        }

        public void Add(T item)
        {
            GetRepositoryFake().Add(item);
        }

        public void Remove(T item)
        {
            GetRepositoryFake().Remove(item);
        }

        public void SaveAllChanges()
        {
            GetRepositoryFake().SaveAllChanges();
        }
    }
}
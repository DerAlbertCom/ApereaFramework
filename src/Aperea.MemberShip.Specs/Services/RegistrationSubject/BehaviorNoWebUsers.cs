using System.Collections.Generic;
using System.Linq;
using Aperea.EntityModels;
using Aperea.Repositories;
using Machine.Fakes;

namespace Aperea.Specs.Services
{
    internal class BehaviorNoWebUsers
    {
        OnEstablish _context = fakeAccessor =>
                               fakeAccessor.The<IRepository<Login>>()
                                   .WhenToldTo(r => r.Entities)
                                   .Return(new List<Login>().AsQueryable);
    }
}
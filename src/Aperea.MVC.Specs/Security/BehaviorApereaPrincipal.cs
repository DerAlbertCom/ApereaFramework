using System.Collections.Generic;
using System.Web;
using Aperea.EntityModels;
using Aperea.Repositories;
using Aperea.Services;
using Aperea.Specs.Repositories;
using Machine.Fakes;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.MVC.Specs.Security
{
    public class BehaviorApereaPrincipal
    {
        OnEstablish context = fakeAccessor =>
        {
            ServiceLocator.SetLocatorProvider(fakeAccessor.The<IServiceLocator>);
            var rep = new FakeRepository<Login>(fakeAccessor);
            fakeAccessor.The<IServiceLocator>().WhenToldTo(sl => sl.GetInstance<IRolesFinder>()).Return(new RolesFinder(fakeAccessor.The<IRepository<Login>>()));
            fakeAccessor.The<IServiceLocator>().WhenToldTo(sl => sl.GetInstance<HttpContextBase>()).Return(fakeAccessor.The<HttpContextBase>());
            var memory = new Dictionary<string, object>();
            fakeAccessor.The<HttpContextBase>().WhenToldTo(h => h.Items).Return(memory);
        };
    }
}
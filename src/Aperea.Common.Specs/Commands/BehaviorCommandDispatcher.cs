using System;
using System.Linq;
using Aperea.Commands;
using Aperea.Infrastructure.IoC;
using Machine.Fakes;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Aperea.Common.Specs.Commands
{
    public class BehaviorCommandDispatcher
    {
        OnEstablish context = accessor =>
        {
            var container = ObjectFactory.Container;
            container.Configure(c => c.AddRegistry<CommonCommandRegistry>());

            accessor.The<IServiceLocator>()
                .WhenToldTo(l => l.GetAllInstances(Param.IsAny<Type>()))
                .Return<Type>(type => container.GetAllInstances(type).Cast<object>());
            accessor.Configure<ICommandDispatcher>(new CommandDispatcher(accessor.The<IServiceLocator>()));
        };

        OnCleanup Cleanup = subject =>
        {
            ObjectFactory.ResetDefaults();
            ObjectFactory.Initialize(expression => { });
        };
    }
}
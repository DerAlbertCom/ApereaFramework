using Aperea.CQRS.Commands;
using Aperea.CQRS.EventStore;
using Aperea.CQRS.EventStore.Storage;
using Aperea.Infrastructure.IoC;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Aperea.CQRS.Infrastructure
{
    public class EventStoreRegistry : Registry
    {
        public EventStoreRegistry()
        {
            For<IDomainRepository<IDomainEvent>>().Use<DomainRepository<IDomainEvent>>();
            For<IEventStoreUnitOfWork<IDomainEvent>>().Use<EventStoreUnitOfWork<IDomainEvent>>();
            For<IIdentityMap<IDomainEvent>>().Use<EventStoreIdentityMap<IDomainEvent>>();
        }
    }

    public class CommandsRegistry : Registry
    {
        public CommandsRegistry()
        {
            Scan(s =>
            {
                s.AssembliesForApplication();
                s.TheCallingAssembly();
                s.ConnectImplementationsToTypesClosing(typeof (ICommandHandler<>));
            });
        }
    }
}
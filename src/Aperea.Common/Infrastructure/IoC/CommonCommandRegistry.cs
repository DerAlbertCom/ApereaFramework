using Aperea.Commands;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Aperea.Infrastructure.IoC
{
    public class CommonCommandRegistry : Registry
    {
        public CommonCommandRegistry()
        {
            Scan(s =>
            {
                s.AssembliesForApplication();
                s.ConnectImplementationsToTypesClosing(typeof (ICommandHandler<>));
                s.ConnectImplementationsToTypesClosing(typeof (ICommandValidator<>));
                s.ConnectImplementationsToTypesClosing(typeof (IQueryHandler<,>));
            });
            For<ICommandDispatcher>().Singleton().Use<CommandDispatcher>();
            bool addCommandExecuter = false;
            bool addQueryExecuter = false;
            Configure(graph =>
            {
                addCommandExecuter = !graph.HasFamily(typeof (ICommandExecutor));
                addQueryExecuter = !graph.HasFamily(typeof (IQueryExecutor));
            });
            if (addCommandExecuter)
            {
                For<ICommandExecutor>().Singleton().Use<CommandExecutor>();
            }
            if (addQueryExecuter)
            {
                For<IQueryExecutor>().Singleton().Use<QueryExecutor>();
            }
        }
    }
}
﻿using Aperea.Infrastructure.IoC;
using StructureMap.Configuration.DSL;

namespace Aperea.Common.Infrastructure.IoC
{
    public class CommonCommandRegistry : Registry
    {
        public CommonCommandRegistry()
        {
            Scan(s =>
            {
                s.AssembliesForApplication();
                s.ConnectImplementationsToTypesClosing(typeof (ICommandHandler<>));
                s.ConnectImplementationsToTypesClosing(typeof(ICommandValidator<>));
            });
            For<ICommandDispatcher>().Singleton().Use<CommandDispatcher>();

            Configure(graph =>
            {
                if (!graph.ContainsFamily(typeof(ICommandExecutor)))
                {
                    For<ICommandExecutor>().Singleton().Use<CommandExecutor>();
                }
            });
        }
    }
}
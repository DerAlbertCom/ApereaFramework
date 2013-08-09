using System;
using System.Linq;
using Aperea.Common.Infrastructure.IoC;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Aperea.Common.Specs
{
    public class FooHandler1 : CommandHandler<FooCommand>
    {
        public override void Execute(FooCommand command)
        {
            command.Executed++;
        }
    }

    public class FooHandler2 : CommandHandler<FooCommand>
    {
        public override void Execute(FooCommand command)
        {
            command.Executed = command.Executed + 2;
        }
    }

    public class BarHandler1 : CommandHandler<BarCommand>
    {
        public override void Execute(BarCommand command)
        {
            command.Executed = command.Executed + 1;
        }
    }

    public class BarHandler2 : CommandHandler<BarCommand, string>
    {
        public override string Execute(BarCommand command)
        {
            command.Executed = command.Executed + 2;
            return "executed";
        }
    }

    public class When_executing_the_fooCommand : WithSubject<CommandExecutor>
    {
        static FooCommand command;

        Establish context = () =>
        {
            With<BehaviorCommandDispatcher>();
            command = new FooCommand();
        };

        Because of = () => Subject.ExecuteCommands(new[] {command});

        It should_fooCommand_should_called_by_both_handlers = () => command.Executed.ShouldEqual(3);
    }

    public class When_executing_the_barCommand : WithSubject<CommandExecutor>
    {
        static BarCommand command;

        Establish context = () =>
        {
            With<BehaviorCommandDispatcher>();
            command = new BarCommand();
        };

        Because of = () => Subject.ExecuteCommands(new[] {command});

        It should_barCommand_should_called_by_both_handlers = () => command.Executed.ShouldEqual(3);

        It should_the_result_in_bar_command_is_executed = () => command.Result.ShouldEqual("executed");
    }

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
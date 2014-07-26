using Aperea.Commands;
using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;

namespace Aperea.Common.Specs.Commands
{
    [Subject(typeof(CommandExecutor))]
    public class When_executing_the_fooCommand : WithSubject<CommandExecutor>
    {
        static FooCommand command;

        Establish context = () =>
        {
            With<BehaviorDispatchers>();
            command = new FooCommand();
        };

        Because of = () => Subject.ExecuteCommands(new[] {command});

        It should_fooCommand_should_called_by_both_handlers = () => command.Executed.Should().Be(3);
    }

    [Subject(typeof(CommandExecutor))]
    public class When_executing_the_barCommand : WithSubject<CommandExecutor>
    {
        static BarCommand command;

        Establish context = () =>
        {
            With<BehaviorDispatchers>();
            command = new BarCommand();
        };

        Because of = () => Subject.ExecuteCommands(new[] {command});

        It should_barCommand_should_called_by_both_handlers = () => command.Executed.Should().Be(3);

        It should_the_result_in_bar_command_is_executed = () => command.Result.Should().Be("executed");
    }
}
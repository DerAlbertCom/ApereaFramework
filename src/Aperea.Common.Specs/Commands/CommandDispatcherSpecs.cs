using System.Linq;
using Aperea.Commands;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Common.Specs.Commands
{
    [Subject(typeof (CommandDispatcher))]
    public class When_getting_the_validators_for_fooCommand : WithSubject<CommandDispatcher>
    {
        Because of = () => Subject.GetValidators(new FooCommand());

        It should_call_the_serviceLocator_for_iCommandValidator_of_fooCommand =
            () => The<IServiceLocator>().WasToldTo(s => s.GetAllInstances(typeof (ICommandValidator<FooCommand>)));
    }

    [Subject(typeof (CommandDispatcher))]
    public class When_getting_the_handlers_for_fooCommand : WithSubject<CommandDispatcher>
    {
        Because of = () => Subject.GetHandlers(new FooCommand()).ToList();

        It should_call_the_serviceLocator_for_iCommandHandler_of_fooCommand = ()
            => The<IServiceLocator>().WasToldTo(s => s.GetAllInstances(typeof (ICommandHandler<FooCommand>)));
    }

    [Subject(typeof (CommandDispatcher))]
    public class When_getting_the_validators_for_barCommand : WithSubject<CommandDispatcher>
    {
        Because of = () => Subject.GetValidators(new BarCommand());

        It should_call_the_serviceLocator_for_iCommandValidator_of_barCommand =
            () => The<IServiceLocator>().WasToldTo(s => s.GetAllInstances(typeof (ICommandValidator<BarCommand>)));
    }

    [Subject(typeof (CommandDispatcher))]
    public class When_getting_the_handlers_for_barCommand : WithSubject<CommandDispatcher>
    {
        Because of = () => Subject.GetHandlers(new BarCommand()).ToList();

        It should_call_the_serviceLocator_for_iCommandHandler_of_barCommand = ()
            => The<IServiceLocator>().WasToldTo(s => s.GetAllInstances(typeof (ICommandHandler<BarCommand>)));
    }
}
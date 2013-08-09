using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Common.Specs
{
    public class FooCommand : ICommand
    {
        public int Executed;

        public void Execute()
        {
            Executed++;
        }
    }

    public class BarCommand : ICommand<string>
    {
        public int Executed;

        public void Execute()
        {
            Executed++;
        }

        public string Result { get; set; }
    }

    public class When_getting_the_validators_for_fooCommand : WithSubject<CommandDispatcher>
    {
        Because of = () => Subject.GetValidators(new FooCommand());
        It should_call_the_serviceLocator_for_iCommandValidator_of_fooCommand =
            () => The<IServiceLocator>().WasToldTo(s => s.GetAllInstances(typeof (ICommandValidator<FooCommand>)));
    }

    public class When_getting_the_handlers_for_fooCommand : WithSubject<CommandDispatcher>
    {
        Because of = () => Subject.GetHandlers(new FooCommand()).ToList();
        It should_call_the_serviceLocator_for_iCommandHandler_of_fooCommand = ()
            => The<IServiceLocator>().WasToldTo(s => s.GetAllInstances(typeof(ICommandHandler<FooCommand>)));

    }

    public class When_getting_the_validators_for_barCommand : WithSubject<CommandDispatcher>
    {
        Because of = () => Subject.GetValidators(new BarCommand());
        It should_call_the_serviceLocator_for_iCommandValidator_of_barCommand =
            () => The<IServiceLocator>().WasToldTo(s => s.GetAllInstances(typeof (ICommandValidator<BarCommand>)));
    }

    public class When_getting_the_handlers_for_barCommand : WithSubject<CommandDispatcher>
    {
        Because of = () => Subject.GetHandlers(new BarCommand()).ToList();
        It should_call_the_serviceLocator_for_iCommandHandler_of_barCommand = ()
            => The<IServiceLocator>().WasToldTo(s => s.GetAllInstances(typeof (ICommandHandler<BarCommand>)));
    }
}
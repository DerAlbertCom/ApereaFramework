using System;
using Aperea.Commands;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Common.Specs.Commands
{
    [Subject(typeof (QueryDispatcher))]
    public class When_getting_the_handlers_for_fooQuery : WithSubject<QueryDispatcher>
    {
        Because of = () => Subject.GetHandler(new FooQuery("hello"));

        It should_call_the_serviceLocator_for_iCommandHandler_of_fooCommand = ()
            => The<IServiceLocator>().WasToldTo(s => s.GetInstance(typeof (IQueryHandler<FooQuery, string>)));
    }

    [Subject(typeof(QueryDispatcher))]
    public class When_getting_the_handlers_for_barQuery : WithSubject<QueryDispatcher>
    {
        Because of = () => Subject.GetHandler(new BarQuery());

        It should_call_the_serviceLocator_for_iCommandHandler_of_fooCommand = ()
            => The<IServiceLocator>().WasToldTo(s => s.GetInstance(typeof(IQueryHandler<BarQuery, DateTime>)));
    }
}
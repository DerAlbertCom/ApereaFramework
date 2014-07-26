using Aperea.Commands;
using FluentAssertions;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Practices.ServiceLocation;

namespace Aperea.Common.Specs.Commands
{
    [Subject(typeof (QueryExecutor))]
    public class When_executing_the_fooQuery_with_tbbt : WithSubject<QueryExecutor>
    {
        static FooQuery command;

        Establish context = () =>
        {
            With<BehaviorDispatchers>();
            command = new FooQuery("TBBT");
        };

        Because of = () => result = Subject.ExecuteQuery(command);

        It should_the_fooQueryHandler_should_return_tbbt = () => result.Should().Be("TBBT");
        static string result;
    }

    [Subject(typeof(QueryExecutor))]
    public class When_executing_the_fooQuery_with_himym : WithSubject<QueryExecutor>
    {
        static FooQuery command;

        Establish context = () =>
        {
            With<BehaviorDispatchers>();
            command = new FooQuery("HIMYM");
        };

        Because of = () => result = Subject.ExecuteQuery(command);

        It should_the_fooQueryHandler_should_return_himym = () => result.Should().Be("HIMYM");
        static string result;
    }
}
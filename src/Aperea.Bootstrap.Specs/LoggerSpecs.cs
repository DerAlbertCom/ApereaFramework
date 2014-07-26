using Aperea.Infrastructure.IoC;
using FluentAssertions;
using Machine.Specifications;
using StructureMap;

namespace Aperea.Bootstrap.Specs
{
    [Subject(typeof (ILogger))]
    public class When_injecting_an_ILogger_to_a_new_instance
    {
        private interface IFooBar
        {
            ILogger Logger { get; }
        }

        private sealed class FooBar : IFooBar
        {
            private readonly ILogger _logger;

            public FooBar(ILogger logger)
            {
                _logger = logger;
            }

            public ILogger Logger
            {
                get { return _logger; }
            }
        }

        private Establish context = () =>
        {
            container = new Container(c =>
            {
                c.AddRegistry(new LoggerRegistry());
                c.For<IFooBar>().Use<FooBar>();
            });
        };


        private Because of = () => { result = container.GetInstance<IFooBar>(); };

        private It should_the_loggingType_should_be_the_type_of_the_new_instance =
            () => result.Logger.LoggerName.Should().Be(result.GetType().FullName);

        private static Container container;
        private static IFooBar result;
    }
}
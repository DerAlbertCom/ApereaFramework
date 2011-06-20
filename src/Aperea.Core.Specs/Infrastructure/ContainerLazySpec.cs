using System;
using Aperea.Infrastructure.Registration;
using Machine.Specifications;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Aperea.Specs.Infrastructure
{
    public class TestRegistry : Registry
    {
        public TestRegistry()
        {
            For<ITest>().Use<TestImpl>();
        }
    }

    public class When_initializing_a_Lazy_itest_over_the_container
    {
        Establish context = () => RegisterStructureMap.Execute();

        Because of = () => result = RegisterStructureMap.Container.GetInstance<Lazy<ITest>>().Value;
        static ITest result;

        It should_be_of_type_testimpl = () => result.ShouldBeOfType<TestImpl>();
    }

    internal class TestImpl  : ITest
    {
    }

    internal interface ITest
    {
    }
}
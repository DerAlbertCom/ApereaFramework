using Aperea.Commands;

namespace Aperea.Common.Specs.Commands
{
    public class FooQuery : IQueryCommand<string>
    {
        public string Foo { get; private set; }

        public FooQuery(string foo)
        {
            Foo = foo;
        }
    }
}
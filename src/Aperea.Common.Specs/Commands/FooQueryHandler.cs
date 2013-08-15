using Aperea.Commands;

namespace Aperea.Common.Specs.Commands
{
    public class FooQueryHandler : QueryHandler<FooQuery, string>
    {
        public override string Query(FooQuery command)
        {
            return command.Foo;
        }
    }
}
using Aperea.Commands;

namespace Aperea.Common.Specs.Commands
{
    public class FooCommandHandler1 : CommandHandler<FooCommand>
    {
        public override void Execute(FooCommand command)
        {
            command.Executed++;
        }
    }
}
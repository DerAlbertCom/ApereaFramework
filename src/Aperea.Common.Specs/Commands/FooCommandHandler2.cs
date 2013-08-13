using Aperea.Commands;

namespace Aperea.Common.Specs.Commands
{
    public class FooCommandHandler2 : CommandHandler<FooCommand>
    {
        public override void Execute(FooCommand command)
        {
            command.Executed = command.Executed + 2;
        }
    }
}
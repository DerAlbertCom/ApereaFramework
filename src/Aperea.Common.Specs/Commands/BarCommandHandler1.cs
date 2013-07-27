using Aperea.Commands;

namespace Aperea.Common.Specs.Commands
{
    public class BarCommandHandler1 : CommandHandler<BarCommand>
    {
        public override void Execute(BarCommand command)
        {
            command.Executed = command.Executed + 1;
        }
    }
}
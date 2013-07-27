using Aperea.Commands;
using Machine.Specifications.Annotations;

namespace Aperea.Common.Specs.Commands
{
    [UsedImplicitly]
    public class BarCommandHandler2 : CommandHandler<BarCommand, string>
    {
        public override string Execute(BarCommand command)
        {
            command.Executed = command.Executed + 2;
            return "executed";
        }
    }
}
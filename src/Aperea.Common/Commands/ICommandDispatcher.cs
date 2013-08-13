using System.Collections.Generic;

namespace Aperea.Commands
{
    public interface ICommandDispatcher
    {
        IEnumerable<ICommandValidator> GetValidators(ICommand command);
        IEnumerable<ICommandHandler> GetHandlers(ICommand command);
    }
}
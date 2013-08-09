using System.Collections;
using System.Collections.Generic;

namespace Aperea.Common
{
    public interface ICommandDispatcher
    {
        IEnumerable<ICommandValidator> GetValidators(ICommand command);
        IEnumerable<ICommandHandler> GetHandlers(ICommand command);
    }
}
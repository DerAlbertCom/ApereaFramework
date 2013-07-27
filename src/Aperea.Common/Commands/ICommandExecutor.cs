using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aperea.Commands
{
    public interface ICommandExecutor
    {
        IEnumerable<ValidationResult> ValidateCommands(IEnumerable<ICommand> commands);
        void ExecuteCommands(IEnumerable<ICommand> commands);
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aperea.Commands
{
    public abstract class CommandValidatorBase<T> : ICommandValidator<T> where T : ICommand
    {
        public abstract IEnumerable<ValidationResult> Validate(T command);

        IEnumerable<ValidationResult> ICommandValidator.Validate(ICommand command)
        {
            return Validate((T) command);
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aperea.Commands
{
    public interface ICommandValidator
    {
        IEnumerable<ValidationResult> Validate(ICommand command);
    }

    public interface ICommandValidator<in T> : ICommandValidator where T : ICommand
    {
        IEnumerable<ValidationResult> Validate(T command);
    }
}
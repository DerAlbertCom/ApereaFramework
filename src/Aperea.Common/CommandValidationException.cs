using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Aperea.Common
{
    public class CommandValidationException : ValidationException
    {
        private readonly IEnumerable<ValidationResult> validationResults;

        public CommandValidationException(IEnumerable<ValidationResult> validationResults)
        {
            this.validationResults = validationResults.ToArray();
        }

        public IEnumerable<ValidationResult> ValidationResults
        {
            get { return validationResults; }
        }
    }
}
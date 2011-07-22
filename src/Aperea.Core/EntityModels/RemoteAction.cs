using System;
using System.ComponentModel.DataAnnotations;

namespace Aperea.EntityModels
{
    public class RemoteAction
    {
        protected RemoteAction()
        {
        }

        public string Id { get; private set; }

        [Required]
        public DateTime Created { get; private set; }

        [Required]
        [StringLength(128)]
        public string Action { get; set; }

        [Required]
        [StringLength(256)]
        public string Parameter { get; set; }

        [Required]
        public Guid ConfirmationKey { get; set; }

        public RemoteAction(string action, string parameter)
        {
            Created = DateTime.UtcNow;
            Action = action;
            Parameter = parameter;
            ConfirmationKey = Guid.NewGuid();
        }
    }
}
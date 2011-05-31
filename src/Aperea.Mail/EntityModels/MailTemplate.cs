using System.ComponentModel.DataAnnotations;

namespace Aperea.EntityModels
{
    public class MailTemplate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string TemplateName { get; set; }

        [Required]
        [StringLength(255)]
        public string Subject { get; set; }

        [Required]
        [MaxLength]
        public string Body { get; set; }

        [Required]
        public virtual SystemLanguage SystemLanguage { get; set; }
    }
}
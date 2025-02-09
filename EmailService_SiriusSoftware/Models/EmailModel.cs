using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EmailService_SiriusSoftware.Models
{
    public class EmailModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmail { get; set; }
        [Required]
        public string IdUser { get; set; } = null!;
        [ForeignKey(nameof(IdUser))]
        public ApplicationUser User { get; set; } = null!;
        public string? Sender { get; set; }
        public string? Recipient { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? SendStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

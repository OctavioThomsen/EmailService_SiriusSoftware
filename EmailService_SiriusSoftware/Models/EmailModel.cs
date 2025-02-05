using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmailService_SiriusSoftware.Models
{
    public class EmailModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id_email")]
        public int IdEmail { get; set; }
        public string? Sender { get; set; }
        public string? Recipient { get; set; }
        public string? Body { get; set; }
        public string? SendStatus { get; set; }
    }
}
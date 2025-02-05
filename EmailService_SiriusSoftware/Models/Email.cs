using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmailService_SiriusSoftware.Models
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id_email")]
        public int ID_EMAIL { get; set; }
        public string? SENDER { get; set; }
        public string? RECIPIENT { get; set; }
        public string? BODY { get; set; }
        public string? SEND_STATUS { get; set; }
    }
}

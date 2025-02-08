using System.Text.Json.Serialization;

namespace EmailService_SiriusSoftware.Dtos
{
    public class EmailDto
    {
        public int IdEmail { get; set; } = 0;
        public string Sender { get; set; } = "oti_thomsen98@hotmail.com";
        public string Recipient { get; set; } = string.Empty;
        public string Subject { get; set; } = "Sin asunto";
        public string Body { get; set; } = string.Empty;
    }

    public class EmailRequestDto
    {
        public string Recipient { get; set; } = string.Empty;
        public string Subject { get; set; } = "Sin asunto";
        public string Body { get; set; } = string.Empty;
    }
}

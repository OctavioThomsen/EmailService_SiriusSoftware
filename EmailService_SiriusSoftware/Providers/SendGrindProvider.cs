using EmailService_SiriusSoftware.Interfaces;
using EmailService_SiriusSoftware.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailService_SiriusSoftware.Providers;
public class SendGridProvider : IEmailProvider
{
    private readonly string _apiKey;

    public SendGridProvider(IConfiguration configuration)
    {
        _apiKey = configuration["SendGrid:ApiKey"] ?? throw new ArgumentNullException("SendGrid API Key is missing.");
    }


    public async Task<bool> SendEmailAsync(EmailModel email)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("oti_thomsen98@hotmail.com", "Emisor Octavio");
        var subject = "Envío de prueba utilizando SendGrid";
        var to = new EmailAddress("otithomsen99@gmail.com", "Receptor Octavio");
        var plainTextContent = "Esto es una prueba de envío.";
        var htmlContent = "Cuerpo del mail.";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        return response.StatusCode == System.Net.HttpStatusCode.Accepted;
    }
}

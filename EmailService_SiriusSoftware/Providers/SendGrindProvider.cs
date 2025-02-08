using EmailService_SiriusSoftware.Interfaces;
using EmailService_SiriusSoftware.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics.Eventing.Reader;

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
        var from = new EmailAddress(email.Sender, "Octavio Thomsen");
        var subject = email.Subject;
        var to = new EmailAddress(email.Recipient);
        var plainTextContent = email.Body;
        var htmlContent = email.Body;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {   
            return true;
        }
        return false;
    }
}

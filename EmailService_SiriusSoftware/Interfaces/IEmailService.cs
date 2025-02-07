using EmailService_SiriusSoftware.Models;

namespace EmailService_SiriusSoftware.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailModel email);
    }
}

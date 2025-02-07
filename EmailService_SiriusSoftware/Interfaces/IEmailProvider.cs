using EmailService_SiriusSoftware.Models;

namespace EmailService_SiriusSoftware.Interfaces
{
    public interface IEmailProvider
    {
        Task<bool> SendEmailAsync(EmailModel email);
    }
}

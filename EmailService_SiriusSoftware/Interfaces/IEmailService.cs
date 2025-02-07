using EmailService_SiriusSoftware.Models;

namespace EmailService_SiriusSoftware.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> AddEmailSended(EmailModel email);
        Task<IEnumerable<EmailModel>> GetEmails();
        Task<bool> SendEmailAsync(EmailModel email);
    }
}

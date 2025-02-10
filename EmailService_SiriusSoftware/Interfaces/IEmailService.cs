using EmailService_SiriusSoftware.Models;

namespace EmailService_SiriusSoftware.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SaveEmail(EmailModel email);
        Task<Dictionary<string, int>> GetEmailStatsForToday();
        Task<bool> SendEmailAsync(EmailModel email);
    }
}

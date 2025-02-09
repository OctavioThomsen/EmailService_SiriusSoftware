using EmailService_SiriusSoftware.DbContextConfig;
using EmailService_SiriusSoftware.Helpers;
using EmailService_SiriusSoftware.Interfaces;
using EmailService_SiriusSoftware.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailService_SiriusSoftware.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppDbContext _context;
        private readonly IEnumerable<IEmailProvider> _providers;

        public EmailService(IEnumerable<IEmailProvider> providers, AppDbContext appDbContext)
        {
            _providers = providers;
            _context = appDbContext;
        }

        public async Task<IEnumerable<EmailModel>> GetEmails()
        {
            return await _context.Email.ToListAsync();
        }

        public async Task<bool> SendEmailAsync(EmailModel email)
        {
            var emailLimitHelper = new EmailLimitHelper(_context);
            bool hasExceededLimit = await emailLimitHelper.HasExceededDailyLimitAsync(email.IdUser, DateTime.UtcNow.AddHours(-3));

            if (hasExceededLimit)
            {
                throw new InvalidOperationException("You have reached the daily email limit.");
            }

            foreach (var provider in _providers)
            {
                if (await provider.SendEmailAsync(email))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> AddEmailSended(EmailModel email)
        {
            try
            {
                _context.Email.Add(email);
                await _context.SaveChangesAsync();
                return true; 
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

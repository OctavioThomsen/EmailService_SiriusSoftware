using EmailService_SiriusSoftware.Interfaces;
using EmailService_SiriusSoftware.Models;

namespace EmailService_SiriusSoftware.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEnumerable<IEmailProvider> _providers;

        public EmailService(IEnumerable<IEmailProvider> providers)
        {
            _providers = providers;
        }

        public async Task<bool> SendEmailAsync(EmailModel email)
        {
            foreach (var provider in _providers)
            {
                if (await provider.SendEmailAsync(email))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

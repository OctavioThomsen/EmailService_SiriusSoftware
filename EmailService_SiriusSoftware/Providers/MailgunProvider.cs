using EmailService_SiriusSoftware.Interfaces;
using EmailService_SiriusSoftware.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace EmailService_SiriusSoftware.Providers
{
    public class MailgunProvider : IEmailProvider
    {
        private readonly string _apiKey;
        private readonly string _domain;

        public MailgunProvider(IConfiguration configuration)
        {
            _apiKey = configuration["Mailgun:ApiKey"] ?? throw new ArgumentNullException("Mailgun API Key is missing.");
            _domain = configuration["Mailgun:Domain"] ?? throw new ArgumentNullException("Mailgun domain is missing.");
        }

        public async Task<bool> SendEmailAsync(EmailModel email)
        {
            var options = new RestClientOptions($"https://api.mailgun.net/v3/{_domain}")
            {
                Authenticator = new HttpBasicAuthenticator("api", _apiKey)
            };

            var client = new RestClient(options);
            var request = new RestRequest("messages", Method.Post);

            request.AddParameter("from", $"Octavio Thomsen <mailgun@{_domain}>");
            request.AddParameter("to", email.Recipient);
            request.AddParameter("subject", email.Subject);
            request.AddParameter("text", email.Body);

            var response = await client.ExecuteAsync(request);
            return response.IsSuccessful;
        }
    }
}

using RestSharp;
using RestSharp.Authenticators;

namespace EmailService_SiriusSoftware.Providers
{
    public class Mailgun
    {
        public static RestResponse SendSimpleMessage()
        {
            var apiKey = "ffd6e8c9314fed22b2307d83f70a975c-667818f5-26a7e0ae";
            var domain = "sandbox4199700b098f41138e6542c3aba5d462.mailgun.org";

            var options = new RestClientOptions($"https://api.mailgun.net/v3/{domain}")
            {
                Authenticator = new HttpBasicAuthenticator("api", apiKey)
            };

            var client = new RestClient(options);
            var request = new RestRequest("messages", Method.Post);

            request.AddParameter("from", "Excited User <mailgun@" + domain + ">");
            request.AddParameter("to", "bar@example.com");
            request.AddParameter("to", "YOU@" + domain);
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomeness!");

            return client.Execute(request);
        }
    }
}

using EmailService_SiriusSoftware.Models;

namespace EmailService_SiriusSoftware.Interfaces;
public interface ITokenService
{
    string GenerateToken(string userName, ApplicationUser user, IList<string> roles);
}

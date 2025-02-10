using EmailService_SiriusSoftware.Models;
using System.Security.Claims;

namespace EmailService_SiriusSoftware.Helpers
{
    public static class UserHelper
    {
        internal static void CompleteEmailWithClaims(EmailModel email, ClaimsPrincipal user)
        {
            email.UserName = GetClaimValue(user, "Sub");
            email.IdUser = GetClaimValue(user, "UserId");
            email.Sender = GetClaimValue(user, "Email");
        }

        internal static string GetClaimValue(ClaimsPrincipal user, string claimName)
        {
            return user.FindFirst(claimName)?.Value;
        }
    }
}

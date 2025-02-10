using EmailService_SiriusSoftware.Models;
using System.Security.Claims;

public static class UserHelper
{
    public static void CompleteEmailWithClaims(EmailModel email, ClaimsPrincipal user)
    {
        email.UserName = GetClaimValue(user, "Sub");
        email.IdUser = GetClaimValue(user, "UserId");
        email.Sender = GetClaimValue(user, "Email");
    }

    public static string GetClaimValue(ClaimsPrincipal user, string claimName)
    {
        return user.FindFirst(claimName)?.Value;
    }
}

using EmailService_SiriusSoftware.Models;
using System.Security.Claims;

public class UserHelperTests
{
    [Fact]
    public void CompleteEmailWithClaims_SetsEmailModelFieldsCorrectly()
    {
        var email = new EmailModel();
        var claims = new[]
        {
            new Claim("Sub", "TestUser"),
            new Claim("UserId", "12345"),
            new Claim("Email", "test@example.com")
        };
        var identity = new ClaimsIdentity(claims);
        var user = new ClaimsPrincipal(identity);

        UserHelper.CompleteEmailWithClaims(email, user);

        Assert.Equal("TestUser", email.UserName);
        Assert.Equal("12345", email.IdUser);
        Assert.Equal("test@example.com", email.Sender);
    }

    [Fact]
    public void GetClaimValue_ReturnsCorrectValue()
    {
        var claims = new[]
        {
            new Claim("TestClaim", "ClaimValue")
        };
        var identity = new ClaimsIdentity(claims);
        var user = new ClaimsPrincipal(identity);

        var value = UserHelper.GetClaimValue(user, "TestClaim");

        Assert.Equal("ClaimValue", value);
    }

    [Fact]
    public void GetClaimValue_ReturnsNull_WhenClaimNotFound()
    {
        var claims = new[]
        {
            new Claim("SomeOtherClaim", "Value")
        };
        var identity = new ClaimsIdentity(claims);
        var user = new ClaimsPrincipal(identity);

        var value = UserHelper.GetClaimValue(user, "NonExistentClaim");

        Assert.Null(value);
    }
}

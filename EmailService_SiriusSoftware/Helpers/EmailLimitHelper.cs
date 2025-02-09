using EmailService_SiriusSoftware.DbContextConfig;
using Microsoft.EntityFrameworkCore;

namespace EmailService_SiriusSoftware.Helpers;
public class EmailLimitHelper
{
    private readonly AppDbContext _context;

    public EmailLimitHelper(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasExceededDailyLimitAsync(string userId, DateTime date)
    {
        const int maxEmailsPerDay = 1000;
        var startOfDay = date.Date;

        int sentEmails = await _context.Email
            .Where(e => e.IdUser == userId && e.CreatedDate.Date == startOfDay)
            .CountAsync();

        return sentEmails >= maxEmailsPerDay;
    }
}


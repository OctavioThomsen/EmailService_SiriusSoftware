﻿using EmailService_SiriusSoftware.DbContextConfig;
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

        public async Task<Dictionary<string, int>> GetEmailStatsForToday()
        {
            var today = DateTime.UtcNow.Date;

            var stats = await _context.Email
                .Where(e => e.CreatedDate.Date == today)
                .GroupBy(e => e.IdUser)
                .Select(g => new
                {
                    IdUser = g.Key,
                    EmailCount = g.Count()
                })
                .ToListAsync();

            var userIds = stats.Select(s => s.IdUser).ToList();

            var userNames = await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.UserName ?? string.Empty);

            return stats
                .Where(s => userNames.ContainsKey(s.IdUser) && !string.IsNullOrEmpty(userNames[s.IdUser]))
                .ToDictionary(s => userNames[s.IdUser]!, s => s.EmailCount);
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
                    email.SendStatus = "Sent";
                    await SaveEmail(email);
                    return true;
                }
            }
            email.SendStatus = "Error";
            await SaveEmail(email);
            throw new Exception("All email providers failed.");
        }

        public async Task<bool> SaveEmail(EmailModel email)
        {
            _context.Email.Add(email);
            await _context.SaveChangesAsync();
            return true; 
        }
    }
}

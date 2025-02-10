using EmailService_SiriusSoftware.DbContextConfig;
using EmailService_SiriusSoftware.Helpers;
using EmailService_SiriusSoftware.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Tests
{
    public class EmailLimitHelperTests
    {
        private async Task<AppDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new AppDbContext(options);
            await databaseContext.Database.EnsureCreatedAsync();
            return databaseContext;
        }

        [Fact]
        public async Task HasExceededDailyLimitAsync_ShouldReturnFalse_WhenNoEmailsSent()
        {
            var dbContext = await GetDatabaseContext();
            var helper = new EmailLimitHelper(dbContext);
            var result = await helper.HasExceededDailyLimitAsync("user1", DateTime.UtcNow);
            Assert.False(result);
        }

        [Fact]
        public async Task HasExceededDailyLimitAsync_ShouldReturnFalse_WhenEmailsAreBelowLimit()
        {
            var dbContext = await GetDatabaseContext();
            var helper = new EmailLimitHelper(dbContext);
            var userId = "user1";
            var date = DateTime.UtcNow.Date;

            dbContext.Email.AddRange(Enumerable.Range(1, 999).Select(i => new EmailModel
            {
                IdUser = userId,
                CreatedDate = date
            }));
            await dbContext.SaveChangesAsync();

            var result = await helper.HasExceededDailyLimitAsync(userId, date);
            Assert.False(result);
        }

        [Fact]
        public async Task HasExceededDailyLimitAsync_ShouldReturnTrue_WhenEmailsReachLimit()
        {
            var dbContext = await GetDatabaseContext();
            var helper = new EmailLimitHelper(dbContext);
            var userId = "user1";
            var date = DateTime.UtcNow.Date;

            dbContext.Email.AddRange(Enumerable.Range(1, 1000).Select(i => new EmailModel
            {
                IdUser = userId,
                CreatedDate = date
            }));
            await dbContext.SaveChangesAsync();

            var result = await helper.HasExceededDailyLimitAsync(userId, date);
            Assert.True(result);
        }

        [Fact]
        public async Task HasExceededDailyLimitAsync_ShouldReturnTrue_WhenEmailsExceedLimit()
        {
            var dbContext = await GetDatabaseContext();
            var helper = new EmailLimitHelper(dbContext);
            var userId = "user1";
            var date = DateTime.UtcNow.Date;

            dbContext.Email.AddRange(Enumerable.Range(1, 1001).Select(i => new EmailModel
            {
                IdUser = userId,
                CreatedDate = date
            }));
            await dbContext.SaveChangesAsync();

            var result = await helper.HasExceededDailyLimitAsync(userId, date);
            Assert.True(result);
        }
    }
}

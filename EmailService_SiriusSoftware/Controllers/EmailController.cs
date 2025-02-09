using EmailService_SiriusSoftware.Dtos;
using EmailService_SiriusSoftware.Interfaces;
using EmailService_SiriusSoftware.Mappers;
using EmailService_SiriusSoftware.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailService_SiriusSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _emailService.GetEmailStatsForToday();
            return Ok(stats);
        }


        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto emailRequestDto)
        {
            try
            {
                var email = emailRequestDto.ToEmailModel();
                email.IdUser = User.FindFirst("UserId")?.Value;


                if (await _emailService.SendEmailAsync(email))
                {
                    email.SendStatus = "Sent";
                    await _emailService.AddEmailSended(email);
                    return Ok("Email sent successfully.");
                }
                email.SendStatus = "Error";
                return StatusCode(400, "Error sending email.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"All email providers failed. { ex.Message}");
            }
        }
    }
}
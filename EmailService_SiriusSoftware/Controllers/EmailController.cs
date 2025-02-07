using EmailService_SiriusSoftware.Dtos;
using EmailService_SiriusSoftware.Interfaces;
using EmailService_SiriusSoftware.Mappers;
using EmailService_SiriusSoftware.Models;
using Microsoft.AspNetCore.Mvc;



namespace EmailService_SiriusSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
//    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("GetEmails")]
        public async Task<ActionResult<IEnumerable<EmailDto>>> GetEmails()
        {
            try
            {
                IEnumerable<EmailModel> emails = await _emailService.GetEmails();

                var emailDtos = emails.Select(email => email.ToEmailDto()).ToList();
                return Ok(emailDtos);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto emailDto)
        {
            try
            {
                var email = emailDto.ToEmailModel();

                if (await _emailService.SendEmailAsync(email))
                {
                    email.SendStatus = "Sent";
                    await _emailService.AddEmailSended(email);
                    return Ok("Email sent successfully.");
                }
                return StatusCode(400, "Error sending email.");
            }
            catch 
            {
                return StatusCode(500, "All email providers failed.");
            }
        }
    }
}
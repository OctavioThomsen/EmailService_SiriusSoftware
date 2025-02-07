using EmailService_SiriusSoftware.DbContextConfig;
using EmailService_SiriusSoftware.Interfaces;
using EmailService_SiriusSoftware.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailService_SiriusSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
//    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public EmailController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet("GetEmails")]
        public async Task<ActionResult<IEnumerable<EmailModel>>> GetEmails() 
        {
            return await _context.Email.ToListAsync();
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel email)
        {
            if (await _emailService.SendEmailAsync(email))
            {
                email.SendStatus = "Sent";
                _context.Email.Add(email);
                await _context.SaveChangesAsync();
                return Ok("Email sent successfully.");
            }
            return StatusCode(500, "All email providers failed.");
        }

    }
}
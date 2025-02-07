using EmailService_SiriusSoftware.DbContextConfig;
using EmailService_SiriusSoftware.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailService_SiriusSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmailController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetEmails")]
        public async Task<ActionResult<IEnumerable<EmailModel>>> GetEmails()
        {
            return await _context.Email.ToListAsync();
        }
    }
}
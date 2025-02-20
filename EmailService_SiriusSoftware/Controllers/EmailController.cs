﻿using EmailService_SiriusSoftware.Dtos;
using EmailService_SiriusSoftware.Helpers;
using EmailService_SiriusSoftware.Interfaces;
using EmailService_SiriusSoftware.Mappers;
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
                UserHelper.CompleteEmailWithClaims(email, User);
                
                await _emailService.SendEmailAsync(email);

                return Ok(email.ToEmailDto());
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: { ex.Message}");
            }
        }
    }
}
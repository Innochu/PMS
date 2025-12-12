using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMS.Api.Dtos;
using PMS.Api.Models;
using PMS.Api.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ILogger<StaffController> _logger;

        public StaffController(IUserService userService, IEmailService emailService, ILogger<StaffController> logger)
        {
            _userService = userService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var result = await _userService.GetStaffAsync(pageNumber, pageSize);
            return Ok(result);
        }


        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] InviteRequest request)
        {
            try
            {
               
                await _emailService.SendEmailAsync8(
                    toEmail: request.Email,
                    subject: "Invitation to PMS",
            htmlBody: $@"
                <p>you have been invited to PMS workspace, click the link below to accept invitation</p>
                <p><a href=""{request.link}"">Accept invitation</a></p>"
                );

                return Ok("Email sent successfully using EmailService!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        [HttpPost("invite")]
        public async Task<IActionResult> SendInvite([FromBody] InviteRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("Email is required.");
            var acceptUrl = "";
                var host = Request?.Host.Value ?? "example.com";
                var scheme = Request?.Scheme ?? "https";
                acceptUrl = $"{scheme}://{host}/accept-invite?email={Uri.EscapeDataString(request.Email)}";
   

            var subject = "Invitation to PMS";
            var html = $@"
                <p>you have been invited to PMS workspace, click the link below to accept invitation</p>
                <p><a href=""{acceptUrl}"">Accept invitation</a></p>";

            try
            {
                await _emailService.SendEmailAsync(request.Email, subject, html);
                return Accepted(new { email = request.Email, acceptUrl });
            }
            catch (SmtpFailedRecipientException ex)
            {
                _logger.LogError(ex, "SMTP failed recipient error: {Recipient}", ex.FailedRecipient);
                throw; // rethrow if needed
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, "SMTP exception: StatusCode={StatusCode}, Message={Message}", ex.StatusCode, ex.Message);
                throw; // rethrow if needed
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send invite to {Email}", request.Email);
                return StatusCode(500, "Failed to send invite email.");
            }
        }
    }
}
//ensure to log the errors from render
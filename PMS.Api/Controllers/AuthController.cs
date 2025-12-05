using Microsoft.AspNetCore.Mvc;
using PMS.Api.Dtos;
using PMS.Api.Services;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _users;

        public AuthController(IUserService users)
        {
            _users = users;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "Username and password are required." });

            var user = await _users.ValidateCredentialsAsync(request.Username, request.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials." });

            var response = new LoginResponse
            {
                UserId = user.Id,
                Username = user.Username,
                Message = "Login successful"
            };

            return Ok(response);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("")]  // Empty route prefix to allow root-level actions
    public class HealthController : ControllerBase
    {
        // Handles both root (/) and /ping with the same simple response
        [HttpGet("")]
        [HttpGet("/")]
        [HttpGet("ping")]
        public IActionResult Get()
        {
            return Ok("OK");  // Returns 200 OK with plain text "OK" (tiny and perfect for pings)
        }
    }
}

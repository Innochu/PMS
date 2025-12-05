using Microsoft.AspNetCore.Mvc;
using PMS.Api.Services;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IUserService _userService;

        public StaffController(IUserService userService)
        {
            _userService = userService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var result = await _userService.GetStaffAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}

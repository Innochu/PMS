using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;

namespace PMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfoliosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PortfoliosController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var portfolios = await _db.Portfolios
                                      .Include(p => p.Projects)
                                      .ToListAsync();

            return Ok(portfolios);
        }
    }
}

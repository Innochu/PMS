using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;     // Required for .Include() and .FirstOrDefaultAsync()
using PMS.Api.Data;
using PMS.Api.Dtos;
using PMS.Api.Services;
using System.Text.Json;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ApplicationDbContext _db;

        public ProjectsController(IProjectService projectService, ApplicationDbContext db)
        {
            _projectService = projectService;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _projectService.CreateProjectAsync(request);
            if (created == null)
                return BadRequest(new { message = "Invalid request or portfolio not found." });

            return Ok(created);
        }

        // Fixed: parameter name must match route
        [HttpGet("{id:guid}")]  // Use guid, not int
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpGet("{id:guid}/preview")]  // Use guid
        public async Task<ActionResult<ProjectPreviewDto>> GetProjectPreview(Guid id)
        {
            var project = await _db.Projects
                .Include(p => p.Portfolio)
                .Include(p => p.CreatedBy) // Make sure you have navigation property named CreatedBy
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            var pinForm = await _db.PinForms
                .Include(pf => pf.Dg1DecisionBy) // To get FullName
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            var section1 = pinForm?.Section1Data?.RootElement
                         ?? JsonDocument.Parse("{}").RootElement;

            var preview = new ProjectPreviewDto
            {
                ProjectId = project.Id,
                ProjectNumber = project.ProjectNumber,
                Name = project.Name,
                Title = project.Title ?? project.Name,
                PortfolioName = project.Portfolio?.PortfolioName ?? "N/A",
                CurrentPhase = project.CurrentPhase,
                OverallHealth = project.OverallHealth,
                CreatedAt = project.CreatedAt,
                CreatedBy = project.CreatedBy?.FirstName ?? "Unknown",

                // PIN Form Status
                PinStatus = pinForm?.Status ?? "Not Started",
                SubmittedAt = pinForm?.SubmittedAt,
                Dg1Decision = pinForm?.Dg1Decision,
                Dg1DecisionBy = pinForm?.Dg1DecisionBy?.FirstName,
                Dg1DecisionAt = pinForm?.Dg1DecisionAt,
                Dg1Comments = pinForm?.Dg1Comments,

                // Section 1 Data
                ProposalNumber = section1.TryGetProperty("proposalNumber", out var pn) ? pn.GetInt32() : null,
                Location = section1.TryGetProperty("location", out var loc) ? loc.GetString() : null,
                UnitNumber = section1.TryGetProperty("unitNumber", out var un) ? un.GetInt32() : null,
                ExecutiveSummary = section1.TryGetProperty("executiveSummary", out var es) ? es.GetString() : null,
                ProjectObjective = section1.TryGetProperty("projectObjective", out var po) ? po.GetString() : null,
                Remark = section1.TryGetProperty("remark", out var r) ? r.GetString() : null,
                DescriptionOfRAM = section1.TryGetProperty("descriptionOfRAM", out var ram) ? ram.GetString() : null,
                DescriptionOfCO2 = section1.TryGetProperty("descriptionOfCO2", out var co2) ? co2.GetString() : null,

                HeadOfSafety = section1.TryGetProperty("headOfSafety", out var hos)
                    ? new HeadOfSafetyDto
                    {
                        Name = hos.TryGetProperty("name", out var n) ? n.GetString() : null,
                        Identification = hos.TryGetProperty("identification", out var i) ? i.GetString() : null
                    }
                    : null
            };

            return Ok(preview);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;   
using PMS.Api.Data;
using PMS.Api.Dtos;
using PMS.Api.Services;

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

        [HttpGet("portfolio/{portfolioId:guid}")]
        public async Task<IActionResult> GetByPortfolio(Guid portfolioId)
        {
            var projects = await _projectService.GetByPortfolioAsync(portfolioId);
            return Ok(projects);
        }

        [HttpGet("{id:guid}")] 
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpGet("{id:guid}/preview")]
        public async Task<ActionResult<ProjectPreviewDto>> GetProjectPreview(Guid id)
        {
            var project = await _db.Projects
                .AsNoTracking()
                .Include(p => p.Portfolio)
                .Include(p => p.CreatedBy)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return NotFound();

            var pinForm = await _db.PinForms
                .AsNoTracking()
                .Include(pf => pf.Dg1DecisionBy)
                .Include(pf => pf.RamAssessment)        
                .Include(pf => pf.Co2Screening)           
                .FirstOrDefaultAsync(pf => pf.ProjectId == id);

            var preview = new ProjectPreviewDto
            {
                ProjectId = project.Id,
                ProjectNumber = project.ProjectNumber,
                Title = project.Title,
                PortfolioName = project.Portfolio?.PortfolioName ?? "N/A",
                CurrentPhase = project.CurrentPhase,
                OverallHealth = project.OverallHealth,
                CreatedAt = project.CreatedAt,
                CreatedBy = project.CreatedBy?.FirstName ?? "Unknown",

                // PIN Form Workflow Status
                PinStatus = pinForm?.Status ?? "Not Started",
                SubmittedAt = pinForm?.SubmittedAt,
                Dg1Decision = pinForm?.Dg1Decision,
                Dg1DecisionBy = pinForm?.Dg1DecisionBy?.FirstName,
                Dg1DecisionAt = pinForm?.Dg1DecisionAt,
                Dg1Comments = pinForm?.Dg1Comments,

                // Section 1 – Now real properties (no JSON parsing!)
                ProposalNumber = pinForm?.ProposalNo,
                Location = project?.LocationArea,
                UnitNumber = project?.TrainUnitNo,
                ExecutiveSummary = pinForm?.ExecutiveSummary,
                ProjectObjective = pinForm?.ProjectObjective,
                Remark = pinForm?.ShutdownRemarks ?? pinForm?.InterfaceDetails,

                // Optional: Pull from specialist entities if they exist
                DescriptionOfRAM = pinForm?.RamAssessment?.ScenarioDescription,
                DescriptionOfCO2 = pinForm?.Co2Screening?.ScenarioDescription,

                // Head of Safety sign-off (from RAM assessment)
                HeadOfSafety = pinForm?.RamAssessment != null
                    ? new HeadOfSafetyDto
                    {
                        Name = pinForm.RamAssessment.HeadSafetyName,
                        Identification = pinForm.RamAssessment.HeadSafetyRef
                    }
                    : null
            };

            return Ok(preview);
        }
    }
}
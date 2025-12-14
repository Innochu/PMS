using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;
using PMS.Api.Dtos;
using PMS.Api.Models;

namespace PMS.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _db;

        public ProjectService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ProjectDto>> GetByPortfolioAsync(Guid portfolioId)
        {
            var projects = await _db.Projects
                .AsNoTracking()
                .Where(p => p.PortfolioId == portfolioId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    PortfolioId = p.PortfolioId,
                    ProjectNumber = p.ProjectNumber,
                    Name = p.Title
                })
                .ToListAsync();

            return projects;
        }

        public async Task<ProjectDto?> CreateProjectAsync(CreateProjectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return null;

            var portfolio = await _db.Portfolios.FindAsync(request.PortfolioId);
            if (portfolio == null)
                return null;

            // Generate unique ProjectNumber (e.g., 25MOCPDR001P)
            string projectNumber;
            do
            {
                projectNumber = GenerateProjectNumber(); // We'll fix this to match NLNG format
            } while (await _db.Projects.AnyAsync(p => p.ProjectNumber == projectNumber));

            // 1. Create the official Project
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Title = request.Name.Trim(),
                ProjectNumber = request.DefaultNumber,                    // Official number
                PortfolioId = request.PortfolioId,
                CreatedById = request.CreatedById,
                CreatedAt = DateTime.UtcNow,
                CurrentPhase = "Identify",
                OverallHealth = "Green"
            };

            // 2. Create the PIN Form — fully populated with real fields
            var pinForm = new PinForm
            {
                Id = Guid.NewGuid(),
                ProjectId = project.Id,
                Project = project,  // Optional: if you have navigat

                // SECTION 1.1 - General Detail
                ProposalNo = request.ProposalNumber,
                MtoNo = "GHTY78",
                DateRegistered = DateOnly.FromDateTime(DateTime.UtcNow),
                ProposedProjectNo = request.DefaultNumber,         // User-proposed number (if any)


                // SECTION 1.3 & 1.4
                ExecutiveSummary = request.ExecutiveSummary,
                ProjectObjective = request.ProjectObjective,
                ProblemStatements = "My Problem Statement",
                BusinessImpactAnalysis = "Business Impact Analysis",
                FinancialBenefitsAnalysis = "Financial Benefit Analysis",
                OpportunityCostNonImplementation = "opportunity cost non implementation",
                ValueAtRisk = "Value at risk",
                CurrentMitigations = "currentMitigation",
                FactsAndAssumptions = "Fact and assumption",

                // Optional remarks
                ShutdownRemarks = request.Remark,

                // Status & Workflow
                Status = "Draft",
                IsBreakIn = false,
                CreatedBy = request.CreatedById,
                CreatedAt = DateTime.UtcNow
            };

            // Optional: Pre-fill RAM & CO2 description if provided
            if (!string.IsNullOrWhiteSpace(request.DescriptionOfRAM))
            {
                pinForm.RamAssessment = new ProjectPinRamAssessment
                {
                    ScenarioDescription = request.DescriptionOfRAM,
                    HeadSafetyName = request.HeadOfSafetyName,
                    HeadSafetyRef = request.HeadOfSafetyIdentification
                    // Signature & date filled later during sign-off
                };
            }

            if (!string.IsNullOrWhiteSpace(request.DescriptionOfCO2))
            {
                pinForm.Co2Screening = new ProjectPinCo2Screening
                {
                    ScenarioDescription = request.DescriptionOfCO2
                };
            }

            // Add to context
            _db.Projects.Add(project);
            _db.PinForms.Add(pinForm);

            await _db.SaveChangesAsync();

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Title,
                ProjectNumber = project.ProjectNumber,
                PortfolioId = project.PortfolioId,
             //   PinStatus = pinForm.Status
            };
        }
        private string GenerateProjectNumber()
        {
            const string prefix = "25MOCPDR";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var suffix = new string(Enumerable.Range(0, 6)
                                             .Select(_ => chars[random.Next(chars.Length)])
                                             .ToArray());
            return prefix + suffix;
        }


        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var p = await _db.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (p == null) return null;
            return new ProjectDto
            {
                Id = p.Id,
                Name = p.Title,
                PortfolioId = p.PortfolioId
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;
using PMS.Api.Dtos;
using PMS.Api.Models;
using System.Text.Json;

namespace PMS.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _db;

        public ProjectService(ApplicationDbContext db)
        {
            _db = db;
        }

        //public async Task<ProjectDto?> CreateProjectAsync(CreateProjectRequest request)
        //{
        //    if (string.IsNullOrWhiteSpace(request.Name))
        //        return null;

        //    var portfolio = await _db.Portfolios.FindAsync(request.PortfolioId);
        //    if (portfolio == null)
        //        return null;

        //    // Generate unique ProjectNumber
        //    string projectNumber;
        //    do
        //    {
        //        projectNumber = GenerateProjectNumber();
        //    }
        //    while (await _db.Projects.AnyAsync(p => p.ProjectNumber == projectNumber));

        //    var project = new Project
        //    {
        //        Name = request.Name.Trim(),
        //        PortfolioId = request.PortfolioId,
        //        CreatedById = request.CreatedById,
        //        ProjectNumber = projectNumber
        //    };

        //    var pinform = new PinForm
        //    {
        //        Name = request.Name.Trim(),
        //        PortfolioId = request.PortfolioId,
        //        CreatedById = request.CreatedById,
        //        ProjectNumber = projectNumber
        //    };

        //    _db.Projects.Add(project);
        //    await _db.SaveChangesAsync();

        //    return new ProjectDto
        //    {
        //        Id = project.Id,
        //        Name = project.Name,
        //        PortfolioId = project.PortfolioId,
        //        ProjectNumber = project.ProjectNumber
        //    };


        //}


        public async Task<ProjectDto?> CreateProjectAsync(CreateProjectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return null;

            var portfolio = await _db.Portfolios.FindAsync(request.PortfolioId);
            if (portfolio == null)
                return null;

            // Generate unique ProjectNumber (e.g., 25MOCPDR001)
            string projectNumber;
            do
            {
                projectNumber = GenerateProjectNumber(); // Your existing logic
            }
            while (await _db.Projects.AnyAsync(p => p.ProjectNumber == projectNumber));

            // Create Project
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Title = request.Name.Trim(),
                PortfolioId = request.PortfolioId,
                ProjectNumber = projectNumber,
                CreatedById = request.CreatedById,
                CreatedAt = DateTime.UtcNow,
                CurrentPhase = "Identify",
                OverallHealth = "Green"
            };

            // Build Section1Data JSON from the request
            var section1Data = new
            {
                title = request.Name.Trim(),
                proposalNumber = request.ProposalNumber,
                defaultNumber = request.DefaultNumber,
                location = request.Location,
                unitNumber = request.UnitNumber,
                executiveSummary = request.ExecutiveSummary,
                projectObjective = request.ProjectObjective,
                remark = request.Remark,
                descriptionOfRAM = request.DescriptionOfRAM,
                headOfSafety = new
                {
                    name = request.HeadOfSafetyName,
                    identification = request.HeadOfSafetyIdentification
                },
                descriptionOfCO2 = request.DescriptionOfCO2
            };

            // Create PinForm linked to the Project
            var pinForm = new PinForm
            {
                Id = Guid.NewGuid(),
                ProjectId = project.Id,
                Section1Data = JsonDocument.Parse(JsonSerializer.Serialize(section1Data)),
                Section2Data = JsonDocument.Parse("{}"), 
                SpecialistInputs = JsonDocument.Parse("{}"),
                Status = "Draft",
                SubmittedAt = null,
                Dg1Decision = null,
                Dg1DecisionById = null,
                Dg1DecisionAt = null,
                Dg1Comments = null
            };

            // Add both to context
            _db.Projects.Add(project);
            _db.PinForms.Add(pinForm);

            // Save once — both Project and PinForm are created together
            await _db.SaveChangesAsync();

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                PortfolioId = project.PortfolioId,
                ProjectNumber = project.ProjectNumber,
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
                Name = p.Name,
                PortfolioId = p.PortfolioId
            };
        }
    }
}

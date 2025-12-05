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

        public async Task<ProjectDto?> CreateProjectAsync(CreateProjectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return null;

            // Ensure portfolio exists
            var portfolio = await _db.Portfolios.FindAsync(request.PortfolioId);
            if (portfolio == null)
                return null;

            var project = new Project
            {
                Name = request.Name.Trim(),
                PortfolioId = request.PortfolioId
            };

            _db.Projects.Add(project);
            await _db.SaveChangesAsync();

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                PortfolioId = project.PortfolioId
            };
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
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

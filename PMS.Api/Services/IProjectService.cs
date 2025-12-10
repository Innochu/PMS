using PMS.Api.Dtos;

namespace PMS.Api.Services
{
    public interface IProjectService
    {
        Task<ProjectDto?> CreateProjectAsync(CreateProjectRequest request);
        Task<ProjectDto?> GetByIdAsync(Guid id);
    }
}

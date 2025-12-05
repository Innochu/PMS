using PMS.Api.Models;
using PMS.Api.Dtos;

namespace PMS.Api.Services
{
    public interface IUserService
    {
        Task<User?> FindByUsernameAsync(string username);
        Task<User?> ValidateCredentialsAsync(string username, string password);
        Task<User> CreateUserAsync(string username, string password, string? email = null);

        Task<PagedResult<StaffDto>> GetStaffAsync(int pageNumber, int pageSize);
    }
}

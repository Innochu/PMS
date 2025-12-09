using PMS.Api.Dtos;
using PMS.Api.Models;

namespace PMS.Api.Services
{
    public interface IUserService
    {
        Task<User?> FindByUsernameAsync(string username);
        Task<User?> ValidateCredentialsAsync(string username, string password);
        Task<User> CreateUserAsync(
            string username,
            string password,
            string email,
            string firstName,
            string lastName,
            string role,
            string department,
            string status
        );

        Task<PagedResult<StaffDto>> GetStaffAsync(int pageNumber, int pageSize);

        // Update an existing user. Returns the updated user or null if not found.
        Task<User?> UpdateUserAsync(string id, UpdateStaffRequest request);
    }
}
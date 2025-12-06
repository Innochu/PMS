using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;
using PMS.Api.Models;
using PMS.Api.Dtos;

namespace PMS.Api.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly PasswordHasher<User> _hasher;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
            _hasher = new PasswordHasher<User>();
        }

        public async Task<User?> FindByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> ValidateCredentialsAsync(string username, string password)
        {
            var user = await FindByUsernameAsync(username);
            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<User> CreateUserAsync(
             string username,
             string password,
             string email,
             string firstName,
             string lastName,
             string role,
             string department)
        {
            var existing = await FindByUsernameAsync(username);
            if (existing != null) return existing;

            var user = new User
            {
                Username = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                role = role,
                Department = department
            };

            user.Password = _hasher.HashPassword(user, password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        // New: get paginated staff (returns StaffDto without password)
        public async Task<PagedResult<StaffDto>> GetStaffAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _db.Users.AsNoTracking().OrderBy(u => u.Id);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new StaffDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = u.role,
                    Department = u.Department,
                    Email = u.Email
                })
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<StaffDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }
    }
}

using FinanceApi.Data;
using FinanceApi.DTOs;
using FinanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Services
{
    public class UserService : IUserService
    {
        private readonly FinanceDbContext _context;
        private readonly IPasswordService _passwordService;

        public UserService(
            FinanceDbContext context,
            IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<UserResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            var passwordHash = _passwordService.HashPassword(request.Password);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
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
        private readonly IJwtService _jwtService;

        public UserService(FinanceDbContext context, IPasswordService passwordService, IJwtService jwtService)
        {
            _context = context;
            _passwordService = passwordService;
            _jwtService = jwtService;
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

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var passwordValid = _passwordService.VerifyPassword(
                request.Password,
                user.PasswordHash);

            if (!passwordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(
                user.Id,
                user.FullName,
                user.Email);

            return new LoginResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Token = token
            };
        }
    }
}
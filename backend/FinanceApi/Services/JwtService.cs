using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FinanceApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int userId, string fullName, string email)
        {
            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key is missing.");

            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer is missing.");

            var audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("JWT Audience is missing.");

            var expiryMinutes = int.Parse(
                _configuration["Jwt:ExpiryMinutes"] ?? "60");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, fullName),
                new Claim(ClaimTypes.Email, email)
            };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
using FinanceApi.DTOs;

namespace FinanceApi.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(RegisterRequestDto request);
    }
}
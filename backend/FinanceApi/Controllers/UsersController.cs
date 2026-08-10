using FinanceApi.DTOs;
using FinanceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register(
            RegisterRequestDto request)
        {
            var user = await _userService.RegisterAsync(request);

            return Ok(user);
        }
    }
}
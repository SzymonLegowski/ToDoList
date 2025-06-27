using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todolist_api.Dto;
using todolist_api.Interfaces;
using todolist_api.Model;

namespace todolist_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterAsync(UserDto userDto)
        {
            if (userDto.Username == "" || userDto.Password == "")
                return BadRequest("Nazwa użytkownika i/lub hasło nie mogą być puste.");

            var user = await authService.RegisterAsync(userDto);
            if (user is null)
                return BadRequest("Nazwa użytkownika jest w użyciu.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto userDto)
        {
            var loginOutput = await authService.LoginAsync(userDto);
            if (loginOutput is null)
                return BadRequest("Niepoprawna nazwa użytkownika lub hasło.");

            return Ok(loginOutput);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var result = await authService.RefreshTokensAsync(refreshTokenRequestDto);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Niepoprawny token odświeżania");

            return Ok(result);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var success = await authService.LogoutAsync(userId);
            if (!success)
            {
                return BadRequest("Nie znaleziono użytkownika.");
            }

            return Ok("Wylogowano pomyślnie.");
        }
    }
}
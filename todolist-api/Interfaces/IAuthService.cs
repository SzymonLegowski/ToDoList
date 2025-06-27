using todolist_api.Dto;
using todolist_api.Model;

namespace todolist_api.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto userDto);
        Task<TokenResponseDto?> LoginAsync(UserDto userDto);
        Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto refreshTokenRequestDto);
        Task<bool> LogoutAsync(int userId);
    }
}
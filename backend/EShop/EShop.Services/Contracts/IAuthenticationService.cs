using EShop.Shared.DataTransferObjects.AuthenticationDtos;

namespace EShop.Services.Contracts;

public interface IAuthenticationService
{
    Task<bool> Authenticate(AuthenticateUserDto authenticateUser);
    Task<TokenDto> CreateToken(bool Expired);
    Task<TokenDto> RefreshToken(TokenDto token);
}

using EShop.Shared.DataTransferObjects.AuthenticationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Contracts;

public interface IAuthenticationService
{
    Task<bool> Authenticate(AuthenticateUserDto authenticateUser);
    Task<TokenDto> CreateToken(bool Expired);
    Task<TokenDto> RefreshToken(TokenDto token);
}

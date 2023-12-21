using EShop.Contracts;
using EShop.Entities.ConfigurationModels;
using EShop.Entities.Exceptions;
using EShop.Entities.Models;
using EShop.LoggerService;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.AuthenticationDtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EShop.Services.Implementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly JwtConfiguration _jwtConfiguration;

    private User? _user;
    public AuthenticationService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IOptions<JwtConfiguration> configuration)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _jwtConfiguration = configuration.Value;
    }

    public async Task<bool> Authenticate(AuthenticateUserDto authenticateUser)
    {
        _user = await _repositoryManager.User.GetAsync(u => u.Email == authenticateUser.Email);

        var result = (_user is not null && VerifyPassword(_user.Password, authenticateUser.Password));
        if (!result)
            _loggerManager.LogWarn($"{nameof(authenticateUser)}: Authentication failed. Wrong user name or password");
        return result;
    }


    public async Task<TokenDto> CreateToken(bool Expired)
    {
        var signingCreds = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCreds, claims);

        var refreshToken = GenerateRefreshToken();

        _user.RefreshToken = refreshToken;

        if (Expired)
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        _repositoryManager.User.Update(_user);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new TokenDto(accessToken, refreshToken);
    }

    public async Task<TokenDto> RefreshToken(TokenDto token)
    {
        var principal = GetPrincipalFromExpiredToken(token.AccessToken);

        var user = await _repositoryManager.User.GetAsync(u => u.Email == principal.Identity.Name);
        if (user == null || user.RefreshToken != token.RefreshToken
            || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new RefreshTokenBadRequest();

        _user = user;

        return await CreateToken(false);
    }


    #region private methods

    private bool VerifyPassword(string hasedPassword, string password)
    {
        var response = BCrypt.Net.BCrypt.Verify(password, hasedPassword);
        return response;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCreds, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
                issuer: _jwtConfiguration.ValidIssuer,
        audience: _jwtConfiguration.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                signingCredentials: signingCreds
            );
        return tokenOptions;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.Email)
        };

        //var roles = await _repositoryManager.User.GetRolesAsync(_user);
        //foreach (var role in roles)
        //{
        //    claims.Add(new Claim(ClaimTypes.Role, role));
        //}
        return claims;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))),
            ValidateLifetime = true,
            ValidIssuer = _jwtConfiguration.ValidIssuer,
            ValidAudience = _jwtConfiguration.ValidAudience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token,
            tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken != null)
        {
            if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
        }
        return principal;
    }
    #endregion
}

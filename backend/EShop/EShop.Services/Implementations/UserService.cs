using AutoMapper;
using EShop.Contracts;
using EShop.Entities.Models;
using EShop.LoggerService;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.UserDtos;

namespace EShop.Services.Implementations;

public class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;

    public UserService(IRepositoryManager repositoryManager, 
                    ILoggerManager loggerManager,
                    IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
    }
    public async Task<UserIndexDto> CreateUserAsync(CreateUserDto user)
    {
        var encryptedPassword = GeneratePassword(user.Password);
        var userDocument =new User 
        {
            Name = user.Name,
            Email = user.Email,
            Password = encryptedPassword,
            IsAdmin = user.IsAdmin
        };
        
        await _repositoryManager.User.AddAsync(userDocument);

        return new UserIndexDto(userDocument.Id.ToString(), userDocument.Name, userDocument.Email);
    }

    public Task<(IEnumerable<UserIndexDto> Users, string ids)> CreateUserCollectionAsync(IEnumerable<CreateUserDto> userCollection)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserIndexDto>> GetAllUsersAsync()
    {
        var users = await _repositoryManager.User.GetAllAsync();

        var response = ConvertToUserDto(users);
        return response;
    }

    public Task<IEnumerable<UserIndexDto>> GetByIdsAsync(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public Task<UserIndexDto> GetUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    #region private methods
    private string GeneratePassword(string password)
    {
        string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        return encryptedPassword;
    }

    private List<UserIndexDto> ConvertToUserDto(IEnumerable<User> users)
    {
        var usersDto = new List<UserIndexDto>();
        foreach (var user in users)
        {
            var userDto = new UserIndexDto(
                user.Id.ToString(),
                user.Name,
                user.Email);
            usersDto.Add(userDto);
        }

        return usersDto;
    }

    #endregion
}

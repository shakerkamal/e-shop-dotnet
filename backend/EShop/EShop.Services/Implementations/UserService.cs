using AutoMapper;
using EShop.Contracts;
using EShop.Entities.Models;
using EShop.LoggerService;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.UserDtos;
using MongoDB.Bson;

namespace EShop.Services.Implementations;

public class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;

    public UserService(IRepositoryManager repositoryManager, 
                    ILoggerManager loggerManager)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
    }
    public async Task<UserIndexDto> CreateUserAsync(CreateUserDto user)
    {
        var encryptedPassword = GeneratePassword(user.Password);
        var userDocument = new User 
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

    public async Task DeleteUserAsync(string userId)
    {
        var objectId = new ObjectId(userId);
        await _repositoryManager.User.DeleteAsync(u => u.Id == objectId);
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

    public async Task<UserIndexDto> GetUserAsync(string userId)
    {
        var user = await _repositoryManager.User.GetAsync(userId);
        var userDto = new UserIndexDto(
               user.Id.ToString(),
               user.Name,
               user.Email);
        return userDto;
    }

    public async Task UpdateUserAsync(string id, UpdateUserDto user)
    {
        var userToUpdate = await _repositoryManager.User.GetAsync(id);

        userToUpdate.Name = user.Name;
        userToUpdate.Email = user.Email;

        _repositoryManager.User.Update(userToUpdate);
    }

    public async Task<bool> UserExist(string userId)
    {
        var objectId = new ObjectId(userId);
        return await _repositoryManager.User.DoesExistAsync(u => u.Id == objectId);
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

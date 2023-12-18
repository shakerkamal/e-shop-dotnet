using EShop.Shared.DataTransferObjects.UserDtos;

namespace EShop.Services.Contracts;

public interface IUserService
{
    Task<IEnumerable<UserIndexDto>> GetAllUsersAsync();
    Task<UserIndexDto> GetUserAsync(string userId);
    Task<UserIndexDto> CreateUserAsync(CreateUserDto User);
    Task<IEnumerable<UserIndexDto>> GetByIdsAsync(IEnumerable<string> ids);
    Task<(IEnumerable<UserIndexDto> Users, string ids)> CreateUserCollectionAsync(IEnumerable<CreateUserDto> userCollection);
    Task DeleteUserAsync(string userId);
    Task UpdateUserAsync(string userid, UpdateUserDto updateUser);
    Task<bool> UserExist(string userid);
}

using EShop.Shared.DataTransferObjects.UserDtos;
using EShop.Shared.DataTransferObjects.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Contracts;

public interface IUserService
{
    Task<IEnumerable<UserIndexDto>> GetAllUsersAsync();
    Task<UserIndexDto> GetUserAsync(string userId);
    Task<UserIndexDto> CreateUserAsync(CreateUserDto User);
    Task<IEnumerable<UserIndexDto>> GetByIdsAsync(IEnumerable<string> ids);
    Task<(IEnumerable<UserIndexDto> Users, string ids)> CreateUserCollectionAsync(IEnumerable<CreateUserDto> userCollection);
    Task DeleteUserAsync(string userId);
    //Task UpdateUserAsync(Guid Userid, UpdateUserDto UserForUpdate);
}

namespace EShop.Shared.DataTransferObjects.UserDtos;

public record CreateUserDto(string Name, string Email, string Password, bool IsAdmin);

using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;

namespace EShop.Application.Commands.CreateUser
{
    public sealed record CreateUserCommand(CreateUserDto CreateUser) : IRequest<UserIndexDto>;
}

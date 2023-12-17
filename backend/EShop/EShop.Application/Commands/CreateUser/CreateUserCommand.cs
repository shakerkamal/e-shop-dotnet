using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Commands.CreateUser
{
    public sealed record CreateUserCommand(CreateUserDto CreateUser) : IRequest<UserIndexDto>;
}

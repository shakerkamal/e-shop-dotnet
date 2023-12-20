using EShop.Shared.DataTransferObjects.AuthenticationDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Commands.AuthenticateUser;

public sealed record AuthenticateUserCommand(AuthenticateUserDto Authenticate) : IRequest<TokenDto>;

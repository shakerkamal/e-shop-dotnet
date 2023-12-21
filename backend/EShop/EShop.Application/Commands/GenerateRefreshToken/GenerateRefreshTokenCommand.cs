using EShop.Shared.DataTransferObjects.AuthenticationDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Commands.GenerateRefreshToken;

public sealed record GenerateRefreshTokenCommand(TokenDto Token) : IRequest<TokenDto>;

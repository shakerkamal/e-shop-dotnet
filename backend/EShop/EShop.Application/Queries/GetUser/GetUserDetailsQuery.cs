using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;

namespace EShop.Application.Queries.GetUser;

public sealed record GetUserDetailsQuery(string id) : IRequest<UserIndexDto>;

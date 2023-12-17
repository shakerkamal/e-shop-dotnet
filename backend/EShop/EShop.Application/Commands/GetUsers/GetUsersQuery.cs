using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;

namespace EShop.Application.Commands.GetUsers;

public sealed record class GetUsersQuery() : IRequest<IEnumerable<UserIndexDto>>;

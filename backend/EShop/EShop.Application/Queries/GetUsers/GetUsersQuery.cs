using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;

namespace EShop.Application.Queries.GetUsers;

public sealed record class GetUsersQuery() : IRequest<IEnumerable<UserIndexDto>>;

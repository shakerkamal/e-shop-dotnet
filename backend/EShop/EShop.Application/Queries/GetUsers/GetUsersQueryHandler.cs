﻿using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;

namespace EShop.Application.Queries.GetUsers;

internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserIndexDto>>
{
    private readonly IServiceManager _serviceManager;

    public GetUsersQueryHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<IEnumerable<UserIndexDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _serviceManager.UserService.GetAllUsersAsync();
        return users;
    }
}

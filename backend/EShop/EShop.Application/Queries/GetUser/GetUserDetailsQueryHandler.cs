using EShop.Entities.Exceptions;
using EShop.Services.Contracts;
using EShop.Shared.DataTransferObjects.UserDtos;
using MediatR;

namespace EShop.Application.Queries.GetUser;

internal sealed class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserIndexDto>
{
    private readonly IServiceManager _serviceManager;

    public GetUserDetailsQueryHandler(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<UserIndexDto> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var user = await _serviceManager.UserService.GetUserAsync(request.id);
        if (user is null)
            throw new UserNotFoundException(request.id);
        return user;
    }
}

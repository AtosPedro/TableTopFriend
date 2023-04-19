using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Users.Common;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Queries.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<UserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICachingService _cachingService;
    public GetUserQueryHandler(
        IUserRepository userRepository,
        ICachingService cachingService)
    {
        _userRepository = userRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<UserResult>> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var userCached = await _cachingService.GetCacheValueAsync<UserResult>(request.UserId.ToString());
        if (userCached is not null)
            return userCached;

        var user = await _userRepository.GetById(
            UserId.Create(request.UserId),
            cancellationToken);

        if (user is null)
            return Errors.Authentication.UserNotRegistered;

        var result = user.Adapt<UserResult>();
        await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
        return result;
    }
}

using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Users.Common;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService)
    {
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<bool>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(
            UserId.Create(request.Id),
            cancellationToken);

        if (user is null)
            return Errors.Authentication.UserNotRegistered;

        user.MarkToDelete(_dateTimeProvider.UtcNow);
        var result = await _userRepository.Remove(user);
        await _cachingService.RemoveCacheValueAsync<UserResult>(result.Id.Value.ToString());
        return result is not null;
    }
}

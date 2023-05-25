using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Users.Common;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<bool>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider,
        ICachingService cachingService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
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

        return await _unitOfWork.Execute(async _ =>
        {
            var result = await _userRepository.Remove(user);
            await _cachingService.RemoveCacheValueAsync<UserResult>(result.Id.Value.ToString());
            return result is not null;
        }, cancellationToken);
    }
}

using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Users.Common;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Users.Commands.Delete;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<UserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICachingService _cachingService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
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

    public async Task<ErrorOr<UserResult>> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(UserId.Create(request.Id), cancellationToken);
        if (user is null)
            return Errors.Authentication.UserNotRegistered;

        user.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Role,
            _dateTimeProvider.UtcNow
        );

        return await _unitOfWork.Execute(async _ =>
        {
            await _userRepository.Update(user);
            var result = user.Adapt<UserResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        cancellationToken);
    }
}

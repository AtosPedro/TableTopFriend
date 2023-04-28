using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Users.Common;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Commands.Validate;

public class ValidateUserCommandHandler : IRequestHandler<ValidateUserCommand, ErrorOr<UserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    public ValidateUserCommandHandler(
        IUserRepository userRepository,
        ICachingService cachingService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _cachingService = cachingService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<UserResult>> Handle(
        ValidateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(UserId.Create(request.UserId), cancellationToken);
        if (user is null)
            return Errors.Authentication.UserNotRegistered;

        user.Validate(_dateTimeProvider.UtcNow);

        return await _unitOfWork.Execute(async _ =>{
            await _userRepository.Update(user);
            var result = user.Adapt<UserResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        cancellationToken);
    }
}

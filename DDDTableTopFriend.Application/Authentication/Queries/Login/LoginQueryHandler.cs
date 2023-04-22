using DDDTableTopFriend.Application.Authentication.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Authentication;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateUser.Events;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPublisher _publisher;

    public LoginQueryHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IPublisher publisher,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _publisher = publisher;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        LoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(
            request.Email,
            cancellationToken);

        if (user is null)
            return Errors.Authentication.UserNotRegistered;

        if (!user.IsValidPassword(request.Password))
            return Errors.Authentication.IncorrectPassword;

        var token = _jwtTokenGenerator.GenerateToken(
            user.Id.Value,
            user.FirstName,
            user.LastName);

        await _publisher.Publish(new UserAuthenticatedDomainEvent(
            UserId.Create(user.Id.Value),
            user.FirstName,
            user.LastName,
            user.Email,
            user.UserRole,
            _dateTimeProvider.UtcNow
        ),
        cancellationToken);

        return (user, token).Adapt<AuthenticationResult>();
    }
}

using TableTopFriend.Application.Authentication.Common;
using TableTopFriend.Application.Common.Interfaces.Authentication;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateUser.Events;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Authentication.Queries.Login;

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
            return Errors.Authentication.WrongCredentials;

        if (!user.Password.IsValid(request.Password))
            return Errors.Authentication.WrongCredentials;

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

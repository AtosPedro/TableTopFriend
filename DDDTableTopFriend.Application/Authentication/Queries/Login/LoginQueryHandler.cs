using DDDTableTopFriend.Application.Authentication.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Authentication;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHasher _hasher;
    public LoginQueryHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator, IHasher hasher)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _hasher = hasher;
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

        string passwordHashed = _hasher.ComputeHash(
            request.Password,
            user.PasswordSalt,
            _hasher.GetIterations());

        if (user.Password != passwordHashed)
            return Errors.Authentication.IncorrectPassword;

        var token = _jwtTokenGenerator.GenerateToken(
            user.Id.Value,
            user.FirstName,
            user.LastName);

        return (user, token).Adapt<AuthenticationResult>();
    }
}

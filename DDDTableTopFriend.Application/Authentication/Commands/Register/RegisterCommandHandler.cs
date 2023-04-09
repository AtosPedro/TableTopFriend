using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.Common.Errors;
using DDDTableTopFriend.Application.Common.Interfaces.Authentication;
using DDDTableTopFriend.Application.Authentication.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateUser;
using ErrorOr;
using MediatR;
using Mapster;

namespace DDDTableTopFriend.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IHasher _hasher;
    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IHasher hasher)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _hasher = hasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.Email, cancellationToken);

        if (user is not null)
            return Errors.Authentication.UserAlreadyRegistered;

        string salt = _hasher.GenerateSalt();
        string passwordHashed = _hasher.ComputeHash(
            request.Password,
            salt,
            _hasher.GetIterations());

        user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHashed,
            salt,
            request.Role);

        await _userRepository.Add(user, cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.Email, user.LastName);
        return (user, token).Adapt<AuthenticationResult>();
    }
}

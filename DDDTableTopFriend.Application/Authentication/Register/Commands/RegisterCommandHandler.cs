using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.Entities;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using DDDTableTopFriend.Application.Common.Interfaces.Authentication;
using MediatR;
using DDDTableTopFriend.Application.Authentication.Common;
using Mapster;
using DDDTableTopFriend.Application.Common.Interfaces.Services;

namespace DDDTableTopFriend.Application.Authentication.Register.Commands;

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
        var user = _userRepository.GetUserByEmail(request.Email);

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
            salt);

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.Email, user.LastName);
        return (user, token).Adapt<AuthenticationResult>();
    }
}

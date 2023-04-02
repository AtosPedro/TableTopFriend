using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Domain.Entities;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using DDDTableTopFriend.Application.Common.Interfaces.Authentication;
using MediatR;
using DDDTableTopFriend.Application.Authentication.Common;
using Mapster;

namespace DDDTableTopFriend.Application.Authentication.Register.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmail(request.Email);

        if (user is not null)
            return Errors.Authentication.UserAlreadyRegistered;

        user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.LastName);
        return (user, token).Adapt<AuthenticationResult>();
    }
}

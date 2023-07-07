using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Domain.Common.Errors;
using TableTopFriend.Application.Common.Interfaces.Authentication;
using TableTopFriend.Application.Authentication.Common;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.AggregateUser;
using ErrorOr;
using MediatR;
using Mapster;

namespace TableTopFriend.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(
            request.Email,
            cancellationToken);

        if (user is not null)
            return Errors.Authentication.UserAlreadyRegistered;

        var userOrError = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            null,
            request.Role,
            _dateTimeProvider.UtcNow
        );

        if (userOrError.IsError)
            return userOrError.Errors;

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _userRepository.Add(userOrError.Value, cancellationToken);
            string token = _jwtTokenGenerator.GenerateToken(
                userOrError.Value.Id.Value,
                userOrError.Value.FirstName,
                userOrError.Value.LastName
            );

            return (userOrError.Value, token).Adapt<AuthenticationResult>();
        },
        cancellationToken);
    }
}

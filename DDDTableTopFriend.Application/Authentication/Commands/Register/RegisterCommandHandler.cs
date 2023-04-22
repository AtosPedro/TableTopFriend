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

        user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.Role,
            _dateTimeProvider.UtcNow);

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _userRepository.Add(user, cancellationToken);
            string token = _jwtTokenGenerator.GenerateToken(
                user.Id.Value,
                user.FirstName,
                user.LastName);
            return (user, token).Adapt<AuthenticationResult>();
        },
        user.DomainEvents,
        cancellationToken);
    }
}

using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Users.Common;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Users.Queries.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<UserResult>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserResult>> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(
            UserId.Create(request.UserId),
            cancellationToken);

        if (user is null)
            return Errors.Authentication.UserNotRegistered;

        return user.Adapt<UserResult>();
    }
}

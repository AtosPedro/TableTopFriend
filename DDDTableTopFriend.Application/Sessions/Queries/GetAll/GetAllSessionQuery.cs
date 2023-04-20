using DDDTableTopFriend.Application.Sessions.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Queries.GetAll;

public record GetAllSessionQuery(

) : IRequest<ErrorOr<IReadOnlyList<SessionResult>>>;

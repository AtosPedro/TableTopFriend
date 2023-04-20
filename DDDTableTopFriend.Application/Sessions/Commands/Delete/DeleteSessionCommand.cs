using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Delete;

public record DeleteSessionCommand(
    Guid Id
) : IRequest<ErrorOr<bool>>;

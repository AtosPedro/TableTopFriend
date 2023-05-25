using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Campaigns.Commands.Delete;

public record DeleteSessionCommand(
    Guid Id
) : IRequest<ErrorOr<bool>>;

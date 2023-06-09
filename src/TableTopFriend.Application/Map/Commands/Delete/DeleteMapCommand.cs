using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Map.Commands.Delete;

public record DeleteMapCommand(
    Guid Id
) : IRequest<ErrorOr<bool>>;

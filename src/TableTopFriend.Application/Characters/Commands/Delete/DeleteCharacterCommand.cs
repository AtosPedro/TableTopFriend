using ErrorOr;
using MediatR;

namespace TableTopFriend.Application.Characters.Commands.Delete;

public record DeleteCharacterCommand(
    Guid CharacterId
) : IRequest<ErrorOr<bool>>;

using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Characters.Commands.Delete;

public record DeleteCharacterCommand(
    Guid CharacterId
) : IRequest<ErrorOr<bool>>;

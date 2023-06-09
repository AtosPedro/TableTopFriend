using ErrorOr;
using MediatR;
using TableTopFriend.Application.Map.Common;

namespace TableTopFriend.Application.Map.Commands.Create;

public record CreateMapCommand(

) : IRequest<ErrorOr<MapResult>>;

using ErrorOr;
using MediatR;
using TableTopFriend.Application.Map.Common;

namespace TableTopFriend.Application.Map.Commands.Update;

public record  UpdateMapCommand(

) : IRequest<ErrorOr<MapResult>>;

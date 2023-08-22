using Microsoft.AspNetCore.Http;
using TableTopFriend.Domain.AggregateMap.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;

namespace TableTopFriend.Application.Common.Interfaces.Services;

public interface IImageService
{
    Task<bool> UploadImage(UserId userId, MapId mapId, IFormFile file);
    Task<Stream?> GetImage(UserId userId, MapId mapId);
    Task<bool> DeleteImage(UserId userId, MapId mapId);
}

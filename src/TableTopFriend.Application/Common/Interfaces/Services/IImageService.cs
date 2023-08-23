using Microsoft.AspNetCore.Http;
using TableTopFriend.Domain.AggregateMap.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;

namespace TableTopFriend.Application.Common.Interfaces.Services;

public interface IImageService
{
    Task<bool> UploadImage(UserId userId, string key, IFormFile file);
    Task<Stream?> GetImage(UserId userId, string key);
    Task<bool> DeleteImage(UserId userId, string key);
}

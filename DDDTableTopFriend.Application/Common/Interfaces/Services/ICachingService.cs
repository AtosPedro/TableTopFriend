namespace DDDTableTopFriend.Application.Common.Interfaces.Services;

public interface ICachingService
{
    Task<T?> GetCacheValueAsync<T>(string key) where T: class;
    Task<IEnumerable<T>> GetManyCacheValueAsync<T>() where T: class;
    Task<bool> SetCacheValueAsync<T>(string key, T value);
    Task RemoveCacheValueAsync<T>(string key);
}

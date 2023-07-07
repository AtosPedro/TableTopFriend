using System.Linq.Expressions;

namespace TableTopFriend.Application.Common.Interfaces.Services;

public interface ICachingService
{
    Task<T?> GetCacheValueAsync<T>(string key) where T: class;
    Task<List<T>> GetManyCacheValueAsync<T>() where T: class;
    Task<List<T>> GetManyCacheValueAsync<T>(Expression<Func<T, bool>> predicate) where T: class;
    Task<bool> SetCacheValueAsync<T>(string key, T value);
    Task RemoveCacheValueAsync<T>(string key);
}

using System.Text.Json;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DDDTableTopFriend.Infrastructure.Services.Caching;

public class RedisCachingService : ICachingService
{
    private readonly CachingSettings _cachingSettings;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _database;
    private readonly IServer _server;
    public RedisCachingService(
        IOptions<CachingSettings> cachingSettings,
        IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _cachingSettings = cachingSettings.Value;
        _database = _connectionMultiplexer.GetDatabase();
        _server = _connectionMultiplexer.GetServer(_cachingSettings.ConnectionString);
    }

        public async Task<T?> GetCacheValueAsync<T>(string key) where T: class
        {
            var cod = $"cod-{typeof(T).Name}-{key}";
            var redisObj = await _database.StringGetAsync(cod);

            if (!redisObj.HasValue)
                return null;

            return JsonSerializer.Deserialize<T>(redisObj);
        }

        public async Task<IEnumerable<T>> GetManyCacheValueAsync<T>() where T: class
        {
            var cod = $"cod-{typeof(T).Name}-*";
            var redisObj = _server.KeysAsync(-1, cod);

            if (redisObj == null)
                return null;

            var cachedItems = new List<T>();
            await foreach (var key in redisObj)
            {
                string formattedKey = key.ToString()
                    .Replace("{", "")
                    .Replace("}", "")
                    .Replace($"cod-{typeof(T).Name}-", "");

                var item = await GetCacheValueAsync<T>(formattedKey);
                cachedItems.Add(item);
            }

            return cachedItems;
        }

        public async Task<bool> SetCacheValueAsync<T>(string key, T value)
        {
            var cod = $"cod-{typeof(T).Name}-{key}";

            string valueStr = JsonSerializer.Serialize(value);

            if (String.IsNullOrEmpty(valueStr))
                return false;

            return await _database.StringSetAsync(cod, valueStr);
        }

        public async Task RemoveCacheValueAsync<T>(string key)
        {
            var cod = $"cod-{typeof(T).Name}-{key}";
            await _database.StringGetDeleteAsync(cod);
        }
}

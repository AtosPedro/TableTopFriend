using System.Linq.Expressions;
using TableTopFriend.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace TableTopFriend.Infrastructure.Services.Caching;

public class RedisCachingService : ICachingService
{
    private readonly CachingSettings _cachingSettings;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _database;
    private readonly IServer _server;
    private readonly JsonSerializerSettings _jsonSerializerSettings;
    public RedisCachingService(
        IOptions<CachingSettings> cachingSettings,
        IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _cachingSettings = cachingSettings.Value;
        _database = _connectionMultiplexer.GetDatabase();
        _server = _connectionMultiplexer.GetServer(_cachingSettings.ConnectionString);

        _jsonSerializerSettings = new()
        {
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
    }

        public async Task<T?> GetCacheValueAsync<T>(string key) where T: class
        {
            var cod = $"cod-{typeof(T).Name}-{key}";
            var redisObj = await _database.StringGetAsync(cod);

            if (!redisObj.HasValue)
                return null;

            return JsonConvert.DeserializeObject<T>(redisObj!, _jsonSerializerSettings);
        }

        public async Task<List<T>> GetManyCacheValueAsync<T>() where T: class
        {
            var cod = $"cod-{typeof(T).Name}-*";
            var redisObj = _server.KeysAsync(-1, cod);

            if (redisObj == null)
                return new List<T>();

            var cachedItems = new List<T>();
            await foreach (var key in redisObj)
            {
                string formattedKey = key.ToString()
                    .Replace("{", "")
                    .Replace("}", "")
                    .Replace($"cod-{typeof(T).Name}-", "");

                var item = await GetCacheValueAsync<T>(formattedKey);
                if (item is not null)
                    cachedItems.Add(item);
            }

            return cachedItems;
        }

        public async Task<List<T>> GetManyCacheValueAsync<T>(Expression<Func<T, bool>> predicate) where T: class
        {
            var cod = $"cod-{typeof(T).Name}-*";
            var redisObj = _server.KeysAsync(-1, cod);

            if (redisObj == null)
                return new List<T>();

            var cachedItems = new List<T>();
            await foreach (var key in redisObj)
            {
                string formattedKey = key.ToString()
                    .Replace("{", "")
                    .Replace("}", "")
                    .Replace($"cod-{typeof(T).Name}-", "");

                var item = await GetCacheValueAsync<T>(formattedKey);
                if (item is not null)
                    cachedItems.Add(item);
            }

            return cachedItems.Where(predicate.Compile()).ToList();
        }

        public async Task<bool> SetCacheValueAsync<T>(string key, T value)
        {
            var cod = $"cod-{typeof(T).Name}-{key}";

            string valueStr = JsonConvert.SerializeObject(value);

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

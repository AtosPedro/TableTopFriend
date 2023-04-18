using DDDTableTopFriend.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace DDDTableTopFriend.Infrastructure.Services.Caching;

public class RedisCachingService : ICachingService
{
    private readonly CachingSettings _cachingServiceSettings;

    public RedisCachingService(IOptions<CachingSettings> cachingServiceSettings)
    {
        _cachingServiceSettings = cachingServiceSettings.Value;
    }
}

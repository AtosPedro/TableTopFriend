using DDDTableTopFriend.Application.Common.Interfaces.Services;

namespace DDDTableTopFriend.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

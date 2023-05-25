using TableTopFriend.Application.Common.Interfaces.Services;

namespace TableTopFriend.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

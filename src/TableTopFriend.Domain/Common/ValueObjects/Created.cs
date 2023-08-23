using ErrorOr;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.Common.ValueObjects;

public class CreatedInfo : ValueObject
{
    public DateTime At { get; set; }
    public string By { get; set; }

    public CreatedInfo(
        DateTime at,
        string by)
    {
        At = at;
        By = by;
    }

    public static ErrorOr<CreatedInfo> Create(
        DateTime at,
        string by)

    {
        if (at == DateTime.MinValue)
            return Errors.Errors.Created.InvalidAt;

        if (string.IsNullOrEmpty(by))
            return Errors.Errors.Created.NullOrEmptyBy;

        return new CreatedInfo(at, by);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return At;
        yield return By;
    }
}

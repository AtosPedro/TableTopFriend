using ErrorOr;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.Common.ValueObjects;

public class UpdatedInfo : ValueObject
{
    public DateTime At { get; set; }
    public string By { get; set; }

    public UpdatedInfo(
        DateTime at,
        string by)
    {
        At = at;
        By = by;
    }

    public static ErrorOr<UpdatedInfo> Create(
        DateTime at,
        string by)

    {
        if (at == DateTime.MinValue)
            return Errors.Errors.Updated.InvalidAt;

        if (String.IsNullOrEmpty(by))
            return Errors.Errors.Updated.NullOrEmptyBy;

        return new UpdatedInfo(at, by);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return At;
        yield return By;
    }
}

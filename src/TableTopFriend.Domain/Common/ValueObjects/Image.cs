using ErrorOr;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.Common.ValueObjects;

public class Image : ValueObject
{
    public int Length { get; set; }
    public int Width { get; set; }
    public byte[] Value { get; set; } = null!;

    public static ErrorOr<Image> Create(byte[] image)
    {
        return new();
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Length;
        yield return Width;
        yield return Value;
    }
}

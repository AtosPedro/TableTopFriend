using ErrorOr;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.Common.ValueObjects;

public class Image : ValueObject
{
    public int Length { get; set; }
    public int Width { get; set; }
    public byte[] Value { get; set; } = null!;

    private Image(
        int length,
        int width,
        byte[] value)
    {
        Length = length;
        Width = width;
        Value = value;
    }

    public static ErrorOr<Image> Create(
        byte[] image,
        int length,
        int width)
    {
        if (image.Length <= 0)
            return Errors.Errors.Image.InvalidImage;

        return new Image(length, width, image);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Length;
        yield return Width;
        yield return Value;
    }
}

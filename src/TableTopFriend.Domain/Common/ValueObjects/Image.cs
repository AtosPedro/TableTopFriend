using ErrorOr;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.Common.ValueObjects;

public class Image : ValueObject
{
    public int Height { get; set; }
    public int Width { get; set; }
    public string FileKey { get; set; } = null!;

    private Image(
        int length,
        int width,
        string fileKey)
    {
        Height = length;
        Width = width;
        FileKey = fileKey;
    }

    public static ErrorOr<Image> Create(
        string fileKey,
        int length,
        int width)
    {
        if (fileKey.Length <= 0)
            return Errors.Errors.Image.InvalidImage;

        return new Image(length, width, fileKey);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Height;
        yield return Width;
        yield return FileKey;
    }
}

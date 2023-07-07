using System.Net.Mime;
using ErrorOr;
using TableTopFriend.Domain.Common.Models;

namespace TableTopFriend.Domain.AggregateMap.ValueObjects;

public class Grid : ValueObject
{
    public int Length { get; set; }
    public int Width { get; set; }
    public int CellSize { get; set; }

    public static ErrorOr<Grid> Create(byte[] image)
    {
        return new();
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Length;
        yield return Width;
        yield return CellSize;
    }
}

using ErrorOr;
using TableTopFriend.Domain.AggregateMap.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateMap;

public sealed class Map : AggregateRoot<MapId, Guid>
{
    public Name Name { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public Image Image { get; set; } = null!;
    public Grid Grid { get; set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Map(MapId id) : base(id) { }

    private Map(
        Name name,
        Description description,
        Image image,
        Grid grid,
        DateTime createdAt)
    {
        Name = name;
        Description = description;
        Image = image;
        Grid = grid;
        CreatedAt = createdAt;
    }
    public static ErrorOr<Map> Create(
        string nameStr,
        string descriptionStr,
        byte[] imageT,
        DateTime createdAt)
    {
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);
        var grid = Grid.Create(imageT);
        var image = Image.Create(imageT, 10, 10);

        var errors = HandleErrors(
            name,
            description,
            grid,
            image
        );

        if (errors.Any())
            return errors;

        return new Map(
            name.Value,
            description.Value,
            image.Value,
            grid.Value,
            createdAt
        );
    }

#pragma warning disable CS8618
    private Map()
    {
    }
#pragma warning restore CS8618
}

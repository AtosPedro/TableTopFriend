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
    public CreatedInfo CreatedInfo { get; private set; } = null!;
    public UpdatedInfo? UpdatedInfo { get; private set; }
    public Map(MapId id) : base(id) { }

    private Map(
        Name name,
        Description description,
        Image image,
        CreatedInfo created)
    {
        Name = name;
        Description = description;
        Image = image;
        CreatedInfo = created;
    }
    public static ErrorOr<Map> Create(
        string nameStr,
        string descriptionStr,
        string fileKey,
        DateTime createdAt,
        string createdBy)
    {
        var name = Name.Create(nameStr);
        var description = Description.Create(descriptionStr);
        var image = Image.Create(fileKey, 10, 10);
        var created = CreatedInfo.Create(createdAt, createdBy);

        var errors = HandleErrors(
            name,
            description,
            image,
            created
        );

        if (errors.Any())
            return errors;

        return new Map(
            name.Value,
            description.Value,
            image.Value,
            created.Value
        );
    }

#pragma warning disable CS8618
    private Map()
    {
    }
#pragma warning restore CS8618
}

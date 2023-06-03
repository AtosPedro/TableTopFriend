using TableTopFriend.Domain.AggregateMap.ValueObjects;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Domain.Common.ValueObjects;

namespace TableTopFriend.Domain.AggregateMap;

public sealed class Map : AggregateRoot<MapId, Guid>
{
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public byte[] Image { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string CreatedBy { get; private set; }
    public string UpdatedBy { get; private set; }

    private Map(MapId id) : base(id) { }

#pragma warning disable CS8618
    private Map()
    {
    }
#pragma warning restore CS8618
}

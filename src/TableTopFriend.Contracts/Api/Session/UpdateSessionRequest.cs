namespace TableTopFriend.Contracts.Api.Skill;

public record UpdateSessionRequest(
    Guid Id,
    string Name,
    DateTime DateTime,
    TimeSpan Duration
);

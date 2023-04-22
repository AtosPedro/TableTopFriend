namespace DDDTableTopFriend.Contracts.Skill;

public record UpdateSessionRequest(
    Guid Id,
    string Name,
    DateTime DateTime,
    TimeSpan Duration
);

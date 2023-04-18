namespace DDDTableTopFriend.Application.Statuses.Common;

public record StatusResult(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    float Quantity,
    DateTime CreatedAt
);

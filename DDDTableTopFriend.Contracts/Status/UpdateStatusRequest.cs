namespace DDDTableTopFriend.Contracts.Status;
public record UpdateStatusRequest(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    float Quantity
);

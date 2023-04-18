namespace DDDTableTopFriend.Contracts.Status;
public record CreateStatusRequest(
    Guid UserId,
    string Name,
    string Description,
    float Quantity
);

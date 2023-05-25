namespace TableTopFriend.Contracts.Status;
public record CreateStatusRequest(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    float Quantity
);

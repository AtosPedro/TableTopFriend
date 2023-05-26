namespace TableTopFriend.Contracts.Api.Status;
public record CreateStatusRequest(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    float Quantity
);

namespace TableTopFriend.Contracts.Api.Status;
public record UpdateStatusRequest(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    float Quantity
);

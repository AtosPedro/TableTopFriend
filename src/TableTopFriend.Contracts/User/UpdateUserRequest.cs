namespace TableTopFriend.Contracts.User;

public record UpdateUserRequest(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    byte[] ProfileImage,
    string Password,
    int UserRole
);

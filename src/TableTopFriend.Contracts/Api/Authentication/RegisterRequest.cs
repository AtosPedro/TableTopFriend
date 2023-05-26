namespace TableTopFriend.Contracts.Api.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    int Role);

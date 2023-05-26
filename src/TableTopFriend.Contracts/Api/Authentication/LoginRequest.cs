namespace TableTopFriend.Contracts.Api.Authentication;

public record LoginRequest(
    string Email,
    string Password);

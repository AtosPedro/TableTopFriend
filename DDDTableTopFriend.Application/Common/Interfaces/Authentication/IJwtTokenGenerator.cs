namespace DDDTableTopFriend.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string firstNmae, string lastName);
}

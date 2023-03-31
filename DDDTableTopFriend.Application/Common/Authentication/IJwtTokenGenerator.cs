namespace DDDTableTopFriend.Application.Common.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string firstNmae, string lastName);
}

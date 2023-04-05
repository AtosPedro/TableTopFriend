namespace DDDTableTopFriend.Application.Common.Interfaces.Services;

public interface IHasher
{
    string ComputeHash(string srtToBeHashed, string salt, int iteration);
    string GenerateSalt();
    int GetIterations();
}

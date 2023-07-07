using System.Security.Cryptography;
using System.Text;
namespace TableTopFriend.Domain.Common.Services;

public static class Hasher
{
    public static string ComputeHash(string srtToBeHashed, string salt, int iteration)
    {
        if (iteration <= 0)
            return srtToBeHashed;

        using var sha256 = SHA256.Create();
        var passwordSaltPepper = $"{srtToBeHashed}{salt}e57f6b60a9507aa3f3708f96f23cc0830e0ceb12";
        var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
        var byteHash = sha256.ComputeHash(byteValue);
        var hash = Convert.ToBase64String(byteHash);
        return ComputeHash(hash, salt, iteration - 1);
    }

    public static string GenerateSalt()
    {
        using var rng = RandomNumberGenerator.Create();
        var byteSalt = new byte[16];
        rng.GetBytes(byteSalt);
        return Convert.ToBase64String(byteSalt);
    }
}

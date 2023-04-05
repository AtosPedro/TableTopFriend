using System.Security.Cryptography;
using System.Text;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
namespace DDDTableTopFriend.Infrastructure.Services.Security;

public class Hasher : IHasher
{
    private readonly HasherSettings _hasherSettings;
    public Hasher(IOptions<HasherSettings> hasherSettings)
    {
        _hasherSettings = hasherSettings.Value;
    }

    public string ComputeHash(string srtToBeHashed, string salt, int iteration)
    {
        if (iteration <= 0)
            return srtToBeHashed;

        using var sha256 = SHA256.Create();
        var passwordSaltPepper = $"{srtToBeHashed}{salt}{_hasherSettings.Pepper}";
        var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
        var byteHash = sha256.ComputeHash(byteValue);
        var hash = Convert.ToBase64String(byteHash);
        return ComputeHash(hash, salt, iteration - 1);
    }

    public string GenerateSalt()
    {
        using var rng = RandomNumberGenerator.Create();
        var byteSalt = new byte[16];
        rng.GetBytes(byteSalt);
        var salt = Convert.ToBase64String(byteSalt);
        return salt;
    }

    public int GetIterations()
    {
        return _hasherSettings.Iterations;
    }
}

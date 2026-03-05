using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace archolosDotNet.Models.UserNS;

public class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int DegreeOfParallelism = 4;
    private const int Iterations = 4;
    private const int MemorySize = 1024 * 16;

    public string HashPassword(string password)
    {
        byte[] salt = new byte[SaltSize];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        byte[] hash = HashPassword(password, salt);
        var combinedBytes = new byte[SaltSize + HashSize];

        Array.Copy(salt, 0, combinedBytes, 0, SaltSize);
        Array.Copy(hash, 0, combinedBytes, SaltSize, HashSize);

        return Convert.ToBase64String(combinedBytes);
    }

    public byte[] HashPassword(string password, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            Iterations = Iterations,
            MemorySize = MemorySize,
        };

        return argon2.GetBytes(HashSize);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] combinedBytes = Convert.FromBase64String(hashedPassword);

        byte[] salt = new byte[SaltSize];
        byte[] hash = new byte[HashSize];
        
        Array.Copy(combinedBytes, 0, salt, 0, SaltSize);
        Array.Copy(combinedBytes, SaltSize, hash, 0, HashSize);

        byte[] newHash = HashPassword(password, salt);

        return CryptographicOperations.FixedTimeEquals(hash, newHash);
    }
}

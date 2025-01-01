using System.Security.Cryptography;

namespace ScriptRunner
{
    public class KeyGenerator
    {
        public static string GenerateKey(int length)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes)
                          .TrimEnd('=')
                          .Replace('+', '-')
                          .Replace('/', '_');
        }
    }
}

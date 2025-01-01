using System.Security.Cryptography;

namespace ScriptRunner
{
    public class KeyGenerator
    {
        public static void RunKeyGeneratorScript(string[] args)
        {
            if (TryParseLengthArg(args, out int keyLength))
            {
                Console.WriteLine("[INFO] Starting key generation process...");
                string generatedKey = GenerateKey(keyLength);
                Console.WriteLine($"[INFO] Generated Key (Length {keyLength}): {generatedKey}");
            }
            else
            {
                Console.WriteLine("[ERROR] Invalid arguments for key generation.");
            }
        }

        private static bool TryParseLengthArg(string[] scriptArgs, out int keyLength)
        {
            keyLength = 0;

            if (scriptArgs.Length <= 0)
                return false;

            if (scriptArgs[0] != "-l" && scriptArgs[0] != "--length")
                return false;

            if (scriptArgs.Length < 2 || !int.TryParse(scriptArgs[1], out keyLength) || keyLength <= 0)
            {
                Console.WriteLine("[ERROR] Invalid key length argument. Please provide a positive integer after '-l' or '--length'.");
                return false;
            }

            return true;
        }
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

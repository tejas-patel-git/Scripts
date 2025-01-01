using System.Security.Cryptography;
using System.Text;

namespace ScriptRunner
{
    internal class Hash
    {
        public static void RunHashingScript(string[] args)
        {

            if (args.Length < 2 || !IsValidHashingArgs(args))
            {
                Console.WriteLine("[ERROR] Invalid arguments. Usage: --string <text> or --file <file-path> <hash-algorithm>");
                return;
            }

            string input = GetInputForHashing(args);
            string hashAlgorithm = args.Length > 2 ? args[2].ToUpper() : "SHA256"; // Default to SHA-256

            Console.WriteLine("[INFO] Starting hashing process...");
            string hash = GenerateHash(input, hashAlgorithm);
            Console.WriteLine($"[INFO] Generated {hashAlgorithm} Hash: {hash}");
        }

        private static bool IsValidHashingArgs(string[] args)
        {
            return (args[0] == "-s" || args[0] == "--string" || args[0] == "-f" || args[0] == "--file");
        }

        private static string GetInputForHashing(string[] args)
        {
            string input = string.Empty;

            if (args[0] == "-s" || args[0] == "--string")
            {
                input = args[1];
            }
            else if (args[0] == "-f" || args[0] == "--file")
            {
                string filePath = args[1];
                if (File.Exists(filePath))
                {
                    input = File.ReadAllText(filePath);
                }
                else
                {
                    Console.WriteLine("[ERROR] File not found.");
                    Environment.Exit(1); // Exit if file is not found
                }
            }

            return input;
        }

        private static string GenerateHash(string input, string hashAlgorithm)
        {
            using HashAlgorithm algorithm = GetHashAlgorithm(hashAlgorithm);
            byte[] data = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = algorithm.ComputeHash(data);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private static HashAlgorithm GetHashAlgorithm(string hashAlgorithm)
        {
            return hashAlgorithm switch
            {
                "MD5" => MD5.Create(),
                "SHA1" => SHA1.Create(),
                "SHA256" => SHA256.Create(),
                "SHA512" => SHA512.Create(),
                _ => throw new ArgumentException("Unsupported hash algorithm.")
            };
        }
    }
}

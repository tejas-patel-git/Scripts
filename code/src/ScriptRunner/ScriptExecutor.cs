namespace ScriptRunner
{
    public static class ScriptExecutor
    {
        /// <summary>
        /// Method to run a script based on its name and arguments.
        /// </summary>
        /// <param name="scriptName">The name of the script to execute.</param>
        /// <param name="args">The arguments for the script.</param>
        public static void RunScript(string scriptName, string[] args)
        {
            switch (scriptName.ToLower())
            {
                case "keygenerator":
                    RunKeyGeneratorScript(args);
                    break;

                default:
                    Console.WriteLine($"[ERROR] Unknown script '{scriptName}'. Please specify a valid script name.");
                    break;
            }
        }

        /// <summary>
        /// Runs the key generator script with the provided arguments.
        /// </summary>
        /// <param name="args">The arguments for the key generation script.</param>
        private static void RunKeyGeneratorScript(string[] args)
        {

            if (TryParseLengthArg(args, out int keyLength))
            {
                Console.WriteLine("[INFO] Starting key generation process...");
                string generatedKey = KeyGenerator.GenerateKey(keyLength);
                Console.WriteLine($"[INFO] Generated Key (Length {keyLength}): {generatedKey}");
            }
            else
            {
                Console.WriteLine("[ERROR] Invalid arguments for key generation.");
            }
        }

        /// <summary>
        /// Tries to parse the key length argument from the script arguments.
        /// Supports both the short form (-l) and long form (--length) options.
        /// </summary>
        /// <param name="scriptArgs">The arguments passed to the script. The first argument should be '-l' or '--length', followed by the key length.</param>
        /// <param name="keyLength">The parsed key length, if successful. If parsing fails, this will be set to 0.</param>
        /// <returns>True if the length argument is valid and successfully parsed; otherwise, false.</returns>
        private static bool TryParseLengthArg(string[] scriptArgs, out int keyLength)
        {
            keyLength = 0;

            if (scriptArgs.Length <= 0)
            {
                Console.WriteLine("[ERROR] Missing length argument.");
                return false;
            }

            if (scriptArgs[0] != "-l" && scriptArgs[0] != "--length")
            {
                Console.WriteLine("[ERROR] Invalid flag. Expected '-l' or '--length'.");
                return false;
            }

            if (scriptArgs.Length < 2 || !int.TryParse(scriptArgs[1], out keyLength) || keyLength <= 0)
            {
                Console.WriteLine("[ERROR] Invalid key length argument. Please provide a positive integer after '-l' or '--length'.");
                return false;
            }

            return true;
        }
    }
}

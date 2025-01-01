namespace ScriptRunner
{
    public static class ScriptExecutor
    {
        public static void RunScript(string scriptName, string[] args)
        {
            Console.WriteLine($"[INFO] Running script: {scriptName}");

            switch (scriptName.ToLower())
            {
                case "keygenerator":
                    KeyGenerator.RunKeyGeneratorScript(args);
                    break;

                case "hash":
                    Hash.RunHashingScript(args);
                    break;

                default:
                    Console.WriteLine($"[ERROR] Unknown script '{scriptName}'. Please specify a valid script name.");
                    break;
            }
        }
    }
}

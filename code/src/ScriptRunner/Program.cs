using ScriptRunner;

internal class Program
{
    private static void Main(string[] args)
    {
        // Ensure --script argument is present
        if (args.Length < 2 || args[0] != "--script")
        {
            Console.WriteLine("Usage: --script <ScriptName>");
            return;
        }

        // Get the script name (second argument)
        string scriptName = args[1];

        // Get the remaining arguments (if any)
        string[] scriptArgs = args.Skip(2).ToArray();

        // Execute the corresponding script using ScriptExecutor
        Console.WriteLine($"Running script: {scriptName}");
        ScriptExecutor.RunScript(scriptName, scriptArgs);
    }
}

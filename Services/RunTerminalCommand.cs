// add a method to run a bash command in the terminal
namespace Terminal
{
    public class RunTerminalCommand
    {
        public static async Task<string> RunCommand(string command)
        {
            var escapedArgs = command.Replace("\"", "\\\"");

            var process = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            await process.WaitForExitAsync();
            return result;
        }
    }
}
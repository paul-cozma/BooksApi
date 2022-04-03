// add a method to run a bash command in the terminal

namespace BookStore.Services
{
    public class RunTerminalCommand
    {
        public static string RunCommand(string command)
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
            process.WaitForExit();
            return result;
        }
    }
}
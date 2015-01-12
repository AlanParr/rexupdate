using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateAgent
{
    class Program
    {
        private static string downloadUrl;
        private static int processId;

        static void Main(string[] args)
        {
            var userHome = Environment.ExpandEnvironmentVariables("%TEMP%");
            Console.WriteLine(userHome);
            var outFile = Path.Combine(userHome, "updaterinput.txt");
            Console.WriteLine(outFile);

            File.WriteAllText(outFile, string.Join(",",args));

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-pkgurl":
                        downloadUrl = args[i + 1];
                        break;
                    case "-waitprocess":
                        processId = int.Parse(args[i + 1]);
                        break;
                }
            }

            while (!Process.GetProcessById(processId).HasExited)
            {
                
            }
            
            Console.WriteLine("Process {0} has exited, we can now update.",processId);
            
        }
    }
}

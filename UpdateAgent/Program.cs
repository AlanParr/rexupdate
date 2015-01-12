using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace UpdateAgent
{
    class Program
    {
        private static string _downloadUrl;
        private static int _processId;
        private static bool _autoDownload;
        private static AgentOptions _options;

        static void Main(string[] args)
        {
            _options = AgentOptions.FromArgs(args);

            
            
            while (!Process.GetProcessById(_processId).HasExited)
            {
                
            }
            
            Console.WriteLine("Process {0} has exited, we can now update.",_processId);
            
        }

        void DownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            
        }
    }
}

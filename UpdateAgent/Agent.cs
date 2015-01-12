using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using RexUpdate;

namespace UpdateAgent
{
    internal class Agent
    {
        private readonly AgentOptions _options;
        private readonly Updater _updater;
        private bool _fileDownloaded;
        private readonly string _downloadPath = Path.Combine(Path.GetTempPath(), string.Format("rexupdate-{0}.rex",Guid.NewGuid()));

        public Agent(string[] args)
        {
            _options = AgentOptions.FromArgs(args);
            _updater = new Updater();
        }

        public void Execute()
        {
            _updater.DownloadUpdateAsync(_options.DownloadUri, _downloadPath, DownloadCompleted);   
            if (_options.ProcessIdToWaitFor.HasValue)
            {
                while (ProcessRunning(_options.ProcessIdToWaitFor.Value) && !_fileDownloaded)
                {
                    //Loop until the process exits and the file download has completed.
                }
            }
            
            Console.WriteLine("Process {0} has exited, we can now update.",_options.ProcessIdToWaitFor);
        }

        void DownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            _fileDownloaded = true;
        }

        private static bool ProcessRunning(int processId)
        {
            try
            {
                var p = Process.GetProcessById(processId);
                return !p.HasExited;
            }
            catch (ArgumentException)
            {
                //ArgumentException means the process doesn't exist so it has exited.
                return true;
            }
        }

    }
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Timers;
using RexUpdate;

namespace UpdateAgent
{
    internal class Agent
    {
        private readonly AgentOptions _options;
        private readonly Updater _updater;
        private System.Timers.Timer _timer;
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

            _timer = new System.Timers.Timer(2000);
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (_options.ProcessIdToWaitFor.HasValue && ProcessRunning(_options.ProcessIdToWaitFor.Value))
            {
                Console.WriteLine("Process {0} has not exited", _options.ProcessIdToWaitFor.Value);
                return;
            }

            if (!_fileDownloaded)
            {
                Console.WriteLine("File hasn't been downloaded.");
                return;
            }

            Console.WriteLine("Process {0} has exited, we can now update.", _options.ProcessIdToWaitFor);
        }

        void DownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            if (args.Cancelled)
            {
                Console.WriteLine(args.Error.Message);
            }
            else
            {
                _fileDownloaded = true;
            }
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

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RexUpdate.Exceptions;

namespace RexUpdate
{
    public class Updater
    {
        /// <summary>
        /// Check for an update to the current version of the given software at given uri.
        /// </summary>
        /// <param name="checkUri">Uri to check for updates.</param>
        /// <param name="currentVersion">Current version of the application.</param>
        /// <param name="softwareName">Name of the software package to check for.</param>
        /// <returns></returns>
        public async Task<CheckResult> CheckAsync(string softwareName, Uri checkUri, string currentVersion)
        {
            var checkRequest = new CheckRequest(softwareName, currentVersion);
            var requestContent = new StringContent(JsonConvert.SerializeObject(checkRequest));

            using (var client = new HttpClient())
            using (var response = await client.PostAsync(checkUri, requestContent))
            using (var content = response.Content)
            {
                var result = await content.ReadAsStringAsync();

                if (result.Length == 0)
                {
                    throw new InvalidCheckResponseException("Received empty response from {0}", checkUri);
                }

                CheckResult serializedResult;

                try
                {
                    serializedResult = JsonConvert.DeserializeObject<CheckResult>(result);
                }
                catch (Exception ex)
                {
                    throw new InvalidCheckResponseException("Response was not a valid CheckResult", ex);
                }

                return serializedResult;
            }
        }

        public void DownloadAndInstallOnExit(CheckResult checkResult, string updateAgentPath="")
        {
            if (string.IsNullOrEmpty(updateAgentPath))
            {
                updateAgentPath = Path.Combine(Directory.GetCurrentDirectory(), "RexUpdate/UpdateAgent.exe");
            }

            const string argsMask = "-pkgurl {0} -waitprocess {1} -autodownload";

            var process = Process.GetCurrentProcess();

            Process.Start(updateAgentPath, string.Format(argsMask, checkResult.DownloadUrl, process.Id));
        }

        /// <summary>
        /// Asynchronously download update file and register action to be called when it completes.
        /// </summary>
        /// <param name="uri">Uri to download .rex file from.</param>
        /// <param name="destination">Location to download file to.</param>
        /// <param name="completionAction">Action to be called when the download completes.</param>
        public void DownloadUpdateAsync(Uri uri, string destination, Action<object, AsyncCompletedEventArgs> completionAction)
        {
            var wc = new WebClient();
            wc.DownloadFileCompleted += (sender, eventArgs) => completionAction(sender, eventArgs);
            wc.DownloadFileAsync(uri, destination);
        }
    }
}

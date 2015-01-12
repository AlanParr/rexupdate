using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RexUpdate.Exceptions;

namespace RexUpdate
{
    public class Updater
    {
        private readonly string _softwareName;
        private readonly Uri _checkUri;

        public Updater(string softwareName, Uri checkUri)
        {
            _softwareName = softwareName;
            _checkUri = checkUri;
        }

        public async Task<CheckResult> CheckAsync(string currentVersion)
        {
            var checkRequest = new CheckRequest(_softwareName, currentVersion);
            var requestContent = new StringContent(JsonConvert.SerializeObject(checkRequest));

            using (var client = new HttpClient())
            using (var response = await client.PostAsync(_checkUri,requestContent))
            using (var content = response.Content)
            {
                var result = await content.ReadAsStringAsync();

                if (result.Length == 0)
                {
                    throw new InvalidCheckResponseException("Received empty response from {0}", _checkUri);
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

            const string argsMask = "-pkgurl {0} -waitprocess {1}";

            var process = Process.GetCurrentProcess();


            Process.Start(updateAgentPath, string.Format(argsMask, checkResult.DownloadUrl, process.Id));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexUpdate
{
    public class CheckResult
    {
        public string SoftwareName { get; set; }
        public string ProvidedInstalledVersion { get; set; }
        public string AvailableVersion { get; set; }
        public string VersionNotes { get; set; }
        public bool IsUpgrade { get; set; }
        public bool Downloadable { get; set; }

        public string DownloadUrl { get; set; }
    }
}

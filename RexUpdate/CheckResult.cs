namespace RexUpdate
{
    public class CheckResult
    {
        public string SoftwareName { get; set; }
        public string ProvidedInstalledVersion { get; set; }
        public string AvailableVersion { get; set; }
        public string VersionNotes { get; set; }
        public bool IsAutoUpgrade { get; set; }
        public bool Downloadable { get; set; }

        public string DownloadUrl { get; set; }
    }
}

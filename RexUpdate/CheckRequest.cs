namespace RexUpdate
{
    public class CheckRequest
    {
        public CheckRequest(){}
        public CheckRequest(string softwareName, string currentVersion)
        {
            SoftwareName = softwareName;
            CurrentVersion = currentVersion;
        }
        public string SoftwareName { get; set; }
        public string CurrentVersion { get; set; }
    }
}

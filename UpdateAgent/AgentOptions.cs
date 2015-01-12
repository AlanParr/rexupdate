namespace UpdateAgent
{
    internal class AgentOptions
    {
        public AgentOptions(){}
        public AgentOptions(string downloadUrl, int? processIdToWaitFor)
        {
            DownloadUrl = downloadUrl;
            ProcessIdToWaitFor = processIdToWaitFor;
        }

        public string DownloadUrl { get; private set; }
        public int? ProcessIdToWaitFor { get; private set; }

        public static AgentOptions FromArgs(string[] args)
        {
            var result = new AgentOptions();
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-pkgurl":
                        result.DownloadUrl = args[i + 1];
                        break;
                    case "-waitprocess":
                        result.ProcessIdToWaitFor = int.Parse(args[i + 1]);
                        break;
                }
            }
            return result;
        }
    }
}

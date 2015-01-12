<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	Task.Run(()=>AsyncMain());
}

static async void AsyncMain(){
	  HttpListener listener = new HttpListener();
	  listener.Prefixes.Add ("http://localhost:12347/MyApp/");  // Listen on
	  listener.Start();                                         // port 51111.
	
	  // Await a client request:
	  HttpListenerContext context = await listener.GetContextAsync();
	  
	    // Get the data from the HTTP stream
	    var body = new StreamReader(context.Request.InputStream).ReadToEnd();
	
		body.Dump();
	
		var req = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(body,new{SoftwareName="",CurrentVersion=""});
	
		var res = new CheckResult();
		res.SoftwareName = req.SoftwareName;
		res.AvailableVersion = "1.0.1";
		res.Downloadable = true;
		res.DownloadUrl = "http://iamnotarealurl";
		res.IsUpgrade = true;
		res.VersionNotes = "Super Calli Fragilistic Expealidotious Version";
	
	    byte[] b = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(res));
	    context.Response.StatusCode = 200;
	    context.Response.KeepAlive = false;
	    context.Response.ContentLength64 = b.Length;
	
	    var output = context.Response.OutputStream;
		output.Write(b, 0, b.Length);
	    context.Response.Close();
	
	  Console.ReadLine();
	  listener.Stop();
}

// Define other methods and classes here
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
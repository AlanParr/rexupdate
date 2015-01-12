using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RexUpdate;

namespace RexUpdateTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => AsyncMain(args));

            Console.Read();
        }

        static async void AsyncMain(string[] args)
        {
            var u = new Updater("TestApplication", new Uri("http://localhost:12346/MyApp/"));
            var cr = await u.CheckAsync("1.0.0");
            Console.Write(JsonConvert.SerializeObject(cr));

            u.DownloadAndInstallOnExit(cr,@"M:\Rexson\Projects\RexUpdate\UpdateAgent\bin\Debug\UpdateAgent.exe");
        }
    }
}

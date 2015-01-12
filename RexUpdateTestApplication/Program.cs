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
            var u = new Updater();
            var cr = await u.CheckAsync("TestApplication", new Uri("http://localhost:12347/MyApp/"), "1.0.0");
            Console.Write(JsonConvert.SerializeObject(cr));

            u.DownloadAndInstallOnExit(cr);
            Console.ReadKey();
        }
    }
}

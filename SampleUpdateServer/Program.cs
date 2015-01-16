using System;
using Nancy;
using Nancy.Extensions;
using Nancy.Hosting.Self;
using RexUpdate;

namespace SampleUpdateServer
{
    class Program
    {
        static void Main(string[] args)
        {
            RunNancy();
        }

        static void RunNancy()
        {
            var uri = new Uri("http://localhost:74205");
            var config = new HostConfiguration {UrlReservations = {CreateAutomatically = true}};
            var host = new NancyHost(config, uri);
            host.Start();
        }
    }

    class NancyMain : NancyModule
    {
        public NancyMain()
        {
            Post["/check"] = parameters =>
            {
                var requestBody =Newtonsoft.Json.JsonConvert.DeserializeObject<CheckRequest>(this.Request.Body.AsString());
                return "hello";
            };
        }
    }
}

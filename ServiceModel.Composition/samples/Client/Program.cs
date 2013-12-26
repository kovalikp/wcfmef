using Client.Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string url;
            url = "http://localhost:49793/SampleService.svc";
            url = "http://localhost:12345/SampleService.svc";

            using (var client = new SampleServiceClient("WSHttpBinding_ISampleService", url))
            {
                Console.WriteLine(client.Ping(new PingRequest() { ClientId = 1 }));
                Console.WriteLine(client.Ping(new PingRequest() { ClientId = 1 }));
            }
            using (var client = new SampleServiceClient("WSHttpBinding_ISampleService", url))
            {
                Console.WriteLine(client.Ping(new PingRequest() { ClientId = 2 }));
                Console.WriteLine(client.Ping(new PingRequest() { ClientId = 2 }));
            }

            Console.ReadLine();


        }
    }
}
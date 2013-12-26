using ServiceModel.Composition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedService
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Listeners.Add(new ConsoleTraceListener());

            var catalog = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
            var container = new CompositionContainer(catalog);
            using (var serviceContainer = new SelfHostingContainer(container))
            {
                foreach (var serviceHost in serviceContainer.ServiceHosts)
                {
                    serviceHost.Opened += serviceHost_Opened;
                }
                serviceContainer.Open();
                Console.ReadLine();
            }
        }

        static void serviceHost_Opened(object sender, EventArgs e)
        {
            var serviceHost = (ServiceHost)sender;
            //serviceHost.Description.
        }
    }
}

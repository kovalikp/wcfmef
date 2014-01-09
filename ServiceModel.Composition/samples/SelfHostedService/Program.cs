namespace SelfHostedService
{
    using ServiceModel.Composition;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

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
                    serviceHost.Opening += serviceHost_Opening;
                    serviceHost.Closing += serviceHost_Closing;
                    serviceHost.Closed += serviceHost_Closed;
                    serviceHost.Faulted += serviceHost_Faulted;
                }
                
#if NET45
                // you can control open timeout using using cancellation token in net45
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));
                var task = serviceContainer.OpenAsync(cts.Token);
                try
                {
                    task.Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
#endif
#if NET40
                // async methods are not available for net40
                serviceContainer.Open();
#endif
                Console.WriteLine("Press RETURN to close hosts and exit...");
                Console.ReadLine();
            }
        }

        static void serviceHost_Faulted(object sender, EventArgs e)
        {
            var serviceHost = (ServiceCompositionHost)sender;
            Console.WriteLine("Faulted {0}", serviceHost.Description.ServiceType);
        }

        static void serviceHost_Closed(object sender, EventArgs e)
        {
            var serviceHost = (ServiceCompositionHost)sender;
            Console.WriteLine("Closed {0}", serviceHost.Description.ServiceType);
        }

        static void serviceHost_Closing(object sender, EventArgs e)
        {
            var serviceHost = (ServiceCompositionHost)sender;
            Console.WriteLine("Closing {0}", serviceHost.Description.ServiceType);
        }

        static void serviceHost_Opening(object sender, EventArgs e)
        {
            var serviceHost = (ServiceCompositionHost)sender;
            Console.WriteLine("Opening {0}", serviceHost.Description.ServiceType);
        }

        static void serviceHost_Opened(object sender, EventArgs e)
        {
            var serviceHost = (ServiceCompositionHost)sender;
            Console.WriteLine("Opened {0}", serviceHost.Description.ServiceType);
        }
    }
}

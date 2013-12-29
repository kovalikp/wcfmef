using ServiceModel.Composition.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
   public class SelfHostingContainer : IDisposable
   {
       CompositionContainer _container;
       string _compositionContractName;
       List<ServiceHostWithDescriptionsConfiguration> _serviceHosts;
       
       public SelfHostingContainer(CompositionContainer container)
           :this(container, null)
       {

       }
       
       public SelfHostingContainer(CompositionContainer container, string compositionContractName)
       {
           _container = container;
           _compositionContractName = compositionContractName;
       }

       public IReadOnlyCollection<ServiceHostWithDescriptionsConfiguration> ServiceHosts
       {
           get
           {
               if (_serviceHosts == null)
               {
                   Initialize();
               }
               return _serviceHosts;
           }
       }

       private void Initialize()
       {
           var exports = _container.GetExports<ISelfHostingConfiguration, Meta<TargetServices>>(_compositionContractName);
           var configurations = _container.GetExports<IServiceConfiguration, Meta<TargetServices>>(_compositionContractName);
           var serviceHosts = new List<ServiceHostWithDescriptionsConfiguration>();

           var serviceConfigurator = new CompositionConfigurator(_container);

           foreach (var export in exports)
           {
               foreach (var serviceType in export.Metadata.View.SelectMany(x => x.ServiceTypes))
               {
                   var baseAddresses = export.Value.GetBaseAddresses(serviceType);
                   var serviceHost = new ServiceHostWithDescriptionsConfiguration(serviceConfigurator, serviceType, baseAddresses);
                   configurations.ConfigureServiceHost(serviceHost);
                   serviceHosts.Add(serviceHost);
               }
           }
           _serviceHosts = serviceHosts;
       }

       private IEnumerable<ServiceHostWithDescriptionsConfiguration> OpenableServiceHosts
       {
           get
           {
               return ServiceHosts.Where(x => x.State == CommunicationState.Created);
           }
       }

       private IEnumerable<ServiceHostWithDescriptionsConfiguration> ClosableServiceHosts
       {
           get
           {
               return ServiceHosts.Where(x => x.State == CommunicationState.Opening || x.State == CommunicationState.Opened);
           }
       }

       public void Open()
       {
           foreach (var serviceHost in OpenableServiceHosts)
           {
               serviceHost.Open();
           }
       }

       public void Open(TimeSpan timeout)
       {
           foreach (var serviceHost in OpenableServiceHosts)
           {
               serviceHost.Open(timeout);
           }
       }

       public async Task OpenAsync()
       {
           var serviceHosts = _serviceHosts.Where(x => x.State == CommunicationState.Created);
           var tasks = new List<Task>();
           foreach (var serviceHost in OpenableServiceHosts)
           {
               var task = Task.Factory.FromAsync(serviceHost.BeginOpen, serviceHost.EndOpen, new { });
               tasks.Add(task);
           }
           await Task.WhenAll(tasks);
       }

       public async Task OpenAsync(TimeSpan timeout)
       {
           var tasks = new List<Task>();
           foreach (var serviceHost in _serviceHosts)
           {
               var task = Task.Factory.FromAsync(serviceHost.BeginOpen, serviceHost.EndOpen, timeout, new { });
               tasks.Add(task);
           }
           await Task.WhenAll(tasks);
       }

       public void Close()
       {
           List<Exception> exceptions = new List<Exception>();
           foreach (var serviceHost in ClosableServiceHosts)
           {
               try
               {
                   serviceHost.Close();
               }
               catch (Exception ex)
               {
                   exceptions.Add(ex);
               }
           }
           if (exceptions.Count > 0)
           {
               throw new AggregateException(exceptions);
           }
       }

       public void Close(TimeSpan timeout)
       {
           List<Exception> exceptions = new List<Exception>();
           foreach (var serviceHost in ClosableServiceHosts)
           {
               try
               {
                   serviceHost.Close(timeout);
               }
               catch (Exception ex)
               {
                   exceptions.Add(ex);
               }
           }
           if (exceptions.Count > 0)
           {
               throw new AggregateException(exceptions);
           }
       }

       public async Task CloseAsync()
       {
           var tasks = new List<Task>();
           foreach (var serviceHost in ClosableServiceHosts)
           {
               var task = Task.Factory.FromAsync(serviceHost.BeginClose, serviceHost.EndClose, new { });
               tasks.Add(task);
           }
           await Task.WhenAll(tasks);
       }

       public async Task CloseAsync(TimeSpan timeout)
       {
           var tasks = new List<Task>();
           foreach (var serviceHost in OpenableServiceHosts)
           {
               var task = Task.Factory.FromAsync(serviceHost.BeginClose, serviceHost.EndClose, timeout, new { });
               tasks.Add(task);
           }
           await Task.WhenAll(tasks);
       }

       #region IDisposable Members

       private bool _disposed;

       /// <summary>
       /// Performs application-defined tasks associated with freeing, releasing, or
       /// resetting unmanaged resources.
       /// </summary>
       public void Dispose()
       {
           Dispose(true);
           GC.SuppressFinalize(this);
       }

       /// <summary>
       /// Disposes the object.
       /// </summary>
       /// <param name="disposing">If <code>false</code>, cleans up native resources.
       /// If <code>true</code> cleans up both managed and native resources</param>
       protected virtual void Dispose(bool disposing)
       {
           if (_disposed == false)
           {
               // TODO: clean native resources        

               if (disposing)
               {
                   foreach (var serviceHost in _serviceHosts)
                   {
                       var disposable = serviceHost as IDisposable;
                       if (disposable != null)
                           disposable.Dispose();
                   }

               }

               _disposed = true;
           }
       }

       #endregion
                
                
   }
}

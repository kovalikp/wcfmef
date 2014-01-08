namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.ServiceModel;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// TODO: Documentation.
    /// </summary>
    public partial class SelfHostingContainer
    {
        private CompositionContainer _container;
        private string _compositionContractName;
        private List<ServiceCompositionHost> _serviceHosts;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfHostingContainer"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public SelfHostingContainer(CompositionContainer container)
            : this(container, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfHostingContainer"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="compositionContractName">Name of the composition contract.</param>
        public SelfHostingContainer(CompositionContainer container, string compositionContractName)
        {
            _container = container;
            _compositionContractName = compositionContractName;
        }

        private IEnumerable<ServiceCompositionHost> OpenableServiceHosts
        {
            get
            {
                return ServiceHosts.Where(x => x.State == CommunicationState.Created);
            }
        }

        private IEnumerable<ServiceCompositionHost> ClosableServiceHosts
        {
            get
            {
                return ServiceHosts.Where(x => x.State == CommunicationState.Opening || x.State == CommunicationState.Opened);
            }
        }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public void Open()
        {
            foreach (var serviceHost in OpenableServiceHosts)
            {
                serviceHost.Open();
            }
        }

        /// <summary>
        /// Opens the specified timeout.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        public void Open(TimeSpan timeout)
        {
            foreach (var serviceHost in OpenableServiceHosts)
            {
                serviceHost.Open(timeout);
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <exception cref="System.AggregateException">
        /// Contains exceptions thrown by <see cref="M:System.ServiceModel.Channels.CommunicationObject.Close"/>
        /// in its <see cref="P:System.AggregateException.InnerExceptions"/> collection.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
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

        /// <summary>
        /// Closes the specified timeout.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <exception cref="System.AggregateException">
        /// Contains exceptions thrown by <see cref="M:System.ServiceModel.Channels.CommunicationObject.Close(System.TimeSpan)"/>
        /// in its <see cref="P:System.AggregateException.InnerExceptions"/> collection.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void Initialize()
        {
            var exports = _container.GetExports<ISelfHostingConfiguration, Meta<TargetServices>>(_compositionContractName);
            var configurations = _container.GetExports<IServiceConfiguration, Meta<TargetServices>>(_compositionContractName);
            var serviceHosts = new List<ServiceCompositionHost>();

            foreach (var export in exports)
            {
                foreach (var serviceType in export.Metadata.View.Select(x => x.ServiceType))
                {
                    var baseAddresses = export.Value.GetBaseAddresses(serviceType);
                    var serviceHost = new ServiceCompositionHost(_container, serviceType, baseAddresses);
                    configurations.ConfigureServiceHost(serviceHost);
                    serviceHosts.Add(serviceHost);
                }
            }

            _serviceHosts = serviceHosts;
        }
    }

    /// <content>
    /// IDisposable implementation.
    /// </content>
    public partial class SelfHostingContainer : IDisposable
    {
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
        /// If <code>true</code> cleans up both managed and native resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                if (disposing)
                {
                    foreach (var serviceHost in _serviceHosts)
                    {
                        var disposable = serviceHost as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }

                _disposed = true;
            }
        }
    }
}
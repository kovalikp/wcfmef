namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.ServiceModel;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Service container for self-hosted environment.
    /// Uses implementations of <see cref="ISelfHostingConfiguration"/> interface to discover services.
    /// </summary>
    public partial class SelfHostingContainer
    {
        private CompositionContainer _container;
        private string _compositionContractName;
        private List<ServiceHost> _serviceHosts;
        private object _initializationLock = new object();

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
        /// <param name="compositionContainer">The configured composition container.</param>
        /// <param name="compositionContractName">Name of the composition contract.</param>
        public SelfHostingContainer(CompositionContainer compositionContainer, string compositionContractName)
        {
            _container = compositionContainer;
            _compositionContractName = compositionContractName;
        }

        /// <summary>
        /// Causes service hosts to open.
        /// </summary>
        /// <exception cref="System.AggregateException">
        /// Contains exceptions thrown by <see cref="M:System.ServiceModel.Channels.CommunicationObject.Open"/>
        /// in its <see cref="P:System.AggregateException.InnerExceptions"/> collection.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exceptions rethrown as aggregate expeption.")]
        public void Open()
        {
            List<Exception> exceptions = new List<Exception>();
            foreach (var serviceHost in ServiceHosts)
            {
                try
                {
                    serviceHost.Open();
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
        /// Causes service hosts to open within a specified interval of time.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <exception cref="System.AggregateException">
        /// Contains exceptions thrown by <see cref="M:System.ServiceModel.Channels.CommunicationObject.Open(System.TimeSpan)"/>
        /// in its <see cref="P:System.AggregateException.InnerExceptions"/> collection.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exceptions rethrown as aggregate expeption.")]
        public void Open(TimeSpan timeout)
        {
            List<Exception> exceptions = new List<Exception>();
            foreach (var serviceHost in ServiceHosts)
            {
                try
                {
                    serviceHost.Open(timeout);
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
        /// Causes service hosts to close.
        /// </summary>
        /// <exception cref="System.AggregateException">
        /// Contains exceptions thrown by <see cref="M:System.ServiceModel.Channels.CommunicationObject.Close"/>
        /// in its <see cref="P:System.AggregateException.InnerExceptions"/> collection.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exceptions rethrown as aggregate expeption.")]
        public void Close()
        {
            List<Exception> exceptions = new List<Exception>();
            foreach (var serviceHost in ServiceHosts)
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
        /// Causes service hosts to close within a specified interval of time.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <exception cref="System.AggregateException">
        /// Contains exceptions thrown by <see cref="M:System.ServiceModel.Channels.CommunicationObject.Close(System.TimeSpan)"/>
        /// in its <see cref="P:System.AggregateException.InnerExceptions"/> collection.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Exceptions rethrown as aggregate expeption.")]
        public void Close(TimeSpan timeout)
        {
            List<Exception> exceptions = new List<Exception>();
            foreach (var serviceHost in ServiceHosts)
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

        /// <summary>
        /// Causes service hosts to abort.
        /// </summary>
        public void Abort()
        {
            foreach (var serviceHost in ServiceHosts)
            {
                serviceHost.Abort();
            }
        }

        private void Initialize()
        {
            if (_serviceHosts == null)
            {
                lock (_initializationLock)
                {
                    if (_serviceHosts == null)
                    {
                        var exports = _container.GetExports<ISelfHostingConfiguration, Meta<TargetServices>>(_compositionContractName);
                        var configurations = _container.GetExports<IServiceConfiguration, Meta<TargetServices>>(_compositionContractName);
                        var serviceHosts = new List<ServiceHost>();

                        foreach (var export in exports)
                        {
                            foreach (var serviceType in export.Metadata.View.Select(x => x.ServiceType))
                            {
                                ServiceCompositionHost serviceHost = null;
                                try
                                {
                                    var baseAddresses = export.Value.GetBaseAddresses(serviceType);
                                    serviceHost = new ServiceCompositionHost(_container, serviceType, baseAddresses);
                                    configurations.ConfigureServiceHost(serviceHost);
                                    serviceHosts.Add(serviceHost);
                                    serviceHost = null;
                                }
                                finally
                                {
                                    var disposable = serviceHost as IDisposable;
                                    if (disposable != null)
                                    {
                                        disposable.Dispose();
                                    }
                                }
                            }
                        }

                        _serviceHosts = serviceHosts;
                    }
                }
            }
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
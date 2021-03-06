﻿namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Extend <see cref="ServiceCompositionHostFactoryBase"/> to provide customized <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer"/>
    /// for service composition and configuration.
    /// </summary>
    public abstract class ServiceCompositionHostFactoryBase : ServiceHostFactory
    {
        /// <summary>
        /// Creates a <see cref="ServiceCompositionHost"/> extension of <see cref="T:System.ServiceModel.ServiceHost" /> for a specified type of service with a specific base address.
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host.</param>
        /// <param name="baseAddresses">The <see cref="T:System.Array" /> of type <see cref="T:System.Uri" /> that contains the base addresses for the service hosted.</param>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.ServiceHost" /> for the type of service specified with a specific base address.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The <see cref="M:ServiceModel.Composition.ServiceCompositionHostFactoryBase.GetContainer"/> method returns <see langword="null" />.
        /// </exception>
        protected sealed override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var container = GetContainer();

            if (container == null)
            {
                throw new InvalidOperationException();
            }

            ServiceCompositionHost serviceHostTemp = null;
            ServiceCompositionHost serviceHost = null;

            try
            {
                var instanceContextMode = serviceType.GetInstanceContextMode();

                switch (instanceContextMode)
                {
                    case InstanceContextMode.PerSession:
                        serviceHostTemp = new ServiceCompositionHost(container, serviceType, baseAddresses);
                        break;
                    case InstanceContextMode.PerCall:
                        serviceHostTemp = new ServiceCompositionHost(container, serviceType, baseAddresses);
                        break;
                    case InstanceContextMode.Single:
                        var singleton = container.ExportService(serviceType);
                        serviceHostTemp = new ServiceCompositionHost(container, singleton, baseAddresses);
                        break;
                }

                Configure(serviceHostTemp, container);
                serviceHost = serviceHostTemp;
                serviceHostTemp = null;
            }
            finally
            {
                var disposable = serviceHostTemp as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            return serviceHost;
        }

        /// <summary>
        /// When overridden in derived class, gets <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer"/>
        /// with custom parts catalog and/or export providers.
        /// </summary>
        /// <returns>Configured <see cref="CompositionContainer"/>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Behavior may vary based on implementation.")]
        protected abstract CompositionContainer GetContainer();

        /// <summary>
        /// Configures the specified service host using composed exports marked by <see cref="ExportServiceConfigurationAttribute"/>.
        /// </summary>
        /// <param name="serviceHost">The service host.</param>
        /// <param name="container">The composition container.</param>
        protected virtual void Configure(ServiceHost serviceHost, CompositionContainer container)
        {
            if (serviceHost == null)
            {
                throw new ArgumentNullException("serviceHost");
            }

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            var exportServiceAttribute = serviceHost.Description.Behaviors.Find<ExportServiceAttribute>();
            var contractName = exportServiceAttribute != null ? exportServiceAttribute.ContractName : null;
            var configurations = container.GetExports<IServiceConfiguration, Meta<TargetServices>>(contractName)
                .Where(x => x.Metadata.MatchesExport(serviceHost.Description));
            foreach (var configuration in configurations)
            {
                configuration.Value.Configure(serviceHost);
            }
        }
    }
}
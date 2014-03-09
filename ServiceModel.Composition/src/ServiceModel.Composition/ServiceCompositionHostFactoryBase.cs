namespace ServiceModel.Composition
{
    using ServiceModel.Composition.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.ComponentModel.Composition.ReflectionModel;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;

    /// <summary>
    /// Extend <see cref="ServiceCompositionHostFactoryBase" /> to provide customized <see
    /// cref="System.ComponentModel.Composition.Hosting.CompositionContainer" /> for service composition and configuration.
    /// </summary>
    public abstract class ServiceCompositionHostFactoryBase : ServiceHostFactoryBase
    {
        /// <summary>
        /// When overridden in a derived class, creates a <see cref="T:System.ServiceModel.ServiceHostBase" /> with a
        /// specific base address using custom initiation data.
        /// </summary>
        /// <param name="constructorString">
        /// The initialization data that is passed to the <see cref="T:System.ServiceModel.ServiceHostBase" /> instance
        /// being constructed by the factory.
        /// </param>
        /// <param name="baseAddresses">
        /// An <see cref="T:System.Array" /> of type <see cref="T:System.Uri" /> that contains the base addresses of the host.
        /// </param>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.ServiceHostBase" /> object with the specified base addresses and
        /// initialized with the custom initiation data.
        /// </returns>
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            var container = GetContainer();

            var catalogs = SearchExportProviders(container);

            var exportedServiceTypes = catalogs.SelectMany(x => x.Parts)
                .Where(x => x.ExportDefinitions.Any(
                    y => y.Metadata.ContainsKey("ExportingType") &&
                        (y.Metadata["ExportingType"] as Type) == typeof(ExportServiceAttribute)))
                .Select(x => ReflectionModelServices.GetPartType(x));

            var serviceType = exportedServiceTypes.FirstOrDefault(x => x.Value.FullName == constructorString);

            if (serviceType == null)
            {
                throw new InvalidOperationException("Could not find corresponding service type.");
            }

            return CreateServiceHost(serviceType.Value, baseAddresses);
        }

        /// <summary>
        /// Creates a <see cref="ServiceCompositionHost" /> extension of <see cref="T:System.ServiceModel.ServiceHost"
        /// /> for a specified type of service with a specific base address.
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host.</param>
        /// <param name="baseAddresses">
        /// The <see cref="T:System.Array" /> of type <see cref="T:System.Uri" /> that contains the base addresses for
        /// the service hosted.
        /// </param>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.ServiceHost" /> for the type of service specified with a specific base address.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// The <see cref="M:ServiceModel.Composition.ServiceCompositionHostFactoryBase.GetContainer" /> method returns
        /// <see langword="null" />.
        /// </exception>
        protected internal virtual ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
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
                serviceHostTemp = new ServiceCompositionHost(container, serviceType, baseAddresses);
                Configure(serviceHostTemp, container);
                serviceHost = serviceHostTemp;
                serviceHostTemp = null;
            }
            finally
            {
                var disposable = serviceHostTemp as IDisposable;
                if (serviceHostTemp != null)
                {
                    disposable.Dispose();
                }
            }

            return serviceHost;
        }

        /// <summary>
        /// Configures the specified service host using composed exports marked by <see
        /// cref="ExportServiceConfigurationAttribute" />.
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

        /// <summary>
        /// When overridden in derived class, gets <see
        /// cref="System.ComponentModel.Composition.Hosting.CompositionContainer" /> with custom parts catalog and/or
        /// export providers.
        /// </summary>
        /// <returns>Configured <see cref="CompositionContainer" />.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Behavior may vary based on implementation.")]
        protected abstract CompositionContainer GetContainer();

        private IEnumerable<ComposablePartCatalog> SearchExportProviders(ExportProvider exportProvider)
        {
            var aggregateExportProvider = exportProvider as AggregateExportProvider;
            if (aggregateExportProvider != null)
            {
                var catalogs = aggregateExportProvider.Providers.SelectMany(x => SearchExportProviders(x));
                foreach (var catalog in catalogs)
                {
                    yield return catalog;
                }
            }

            var catalogExportProvider = exportProvider as CatalogExportProvider;
            if (catalogExportProvider != null)
            {
                yield return catalogExportProvider.Catalog;
            }

            var compositionContainer = exportProvider as CompositionContainer;
            if (compositionContainer != null)
            {
                yield return compositionContainer.Catalog;
                var catalogs = compositionContainer.Providers.SelectMany(x => SearchExportProviders(x));
                foreach (var catalog in catalogs)
                {
                    yield return catalog;
                }
            }

            var composablePartExportProvider = exportProvider as ComposablePartExportProvider;
            if (composablePartExportProvider != null)
            {
                yield break;
            }
        }
    }
}
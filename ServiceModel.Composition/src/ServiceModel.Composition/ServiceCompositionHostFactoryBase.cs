using ServiceModel.Composition.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
    /// <summary>
    /// Extend <see cref="ServiceCompositionHostFactoryBase"/> to provide customized <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer"/>
    /// for service composition and configuration.
    /// </summary>
    public abstract class ServiceCompositionHostFactoryBase : ServiceHostFactory
    {
        /// <summary>
        /// Creates a <see cref="ServiceHostWithDescriptionsConfiguration"/> extension of <see cref="T:System.ServiceModel.ServiceHost" /> for a specified type of service with a specific base address.
        /// </summary>
        /// <param name="serviceType">Specifies the type of service to host.</param>
        /// <param name="baseAddresses">The <see cref="T:System.Array" /> of type <see cref="T:System.Uri" /> that contains the base addresses for the service hosted.</param>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.ServiceHost" /> for the type of service specified with a specific base address.
        /// </returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var container = GetContainer();
            
            if (container == null)
                throw new InvalidOperationException();

            var descriptionsConfigurator = new CompositionConfigurator(container);
            var serviceHost = new ServiceHostWithDescriptionsConfiguration(descriptionsConfigurator, serviceType, baseAddresses);
            Configure(serviceHost, container);
            return serviceHost;
        }

        /// <summary>
        /// When overridden in derived class, creates <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer"/>
        /// with custom parts catalog and/or export providers.
        /// </summary>
        /// <returns></returns>
        protected abstract CompositionContainer GetContainer();

        /// <summary>
        /// Configures the specified service host using composed exports marked by <see cref="ExportServiceConfigurationAttribute"/>.
        /// </summary>
        /// <param name="serviceHost">The service host.</param>
        /// <param name="container">The composition container.</param>
        protected virtual void Configure(ServiceHost serviceHost, CompositionContainer container)
        {
            var configurations = container.GetExports<IServiceConfiguration, Meta<TargetServices>>()
                .Where(x => x.Metadata.MatchesExport(serviceHost.Description));
            foreach (var configuration in configurations)
            {
                configuration.Value.Configure(serviceHost);
            }
        }
    }
}

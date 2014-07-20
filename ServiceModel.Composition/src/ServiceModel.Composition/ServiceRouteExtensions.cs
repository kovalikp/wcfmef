namespace ServiceModel.Composition
{
    using ServiceModel.Composition.Internal;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.ServiceModel.Activation;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Routing;

    /// <summary>
    /// Registers <see cref="System.ServiceModel.Activation.ServiceRoute"/>..
    /// Uses implementations of <see cref="IServiceRouteConfiguration"/> interface to discover services.
    /// </summary>
    public static class ServiceRouteExtensions
    {
        /// <summary>
        /// Registers the service routes.
        /// </summary>
        /// <param name="routeCollection">The route collection.</param>
        /// <param name="compositionContainer">The composition container.</param>
        public static void RegisterServiceRoutes(
            this ICollection<RouteBase> routeCollection, 
            CompositionContainer compositionContainer)
        {
            if (routeCollection == null)
            {
                throw new ArgumentNullException("routeCollection");
            }

            if (compositionContainer == null)
            {
                throw new ArgumentNullException("compositionContainer");
            }

            routeCollection.RegisterServiceRoutes(compositionContainer, null);
        }

        /// <summary>
        /// Registers the service routes.
        /// </summary>
        /// <param name="routeCollection">The route collection.</param>
        /// <param name="compositionContainer">The composition container.</param>
        /// <param name="compositionContractName">Name of the composition contract.</param>
        public static void RegisterServiceRoutes(
            this ICollection<RouteBase> routeCollection, 
            CompositionContainer compositionContainer, 
            string compositionContractName)
        {
            if (routeCollection == null)
            {
                throw new ArgumentNullException("routeCollection");
            }

            if (compositionContainer == null)
            {
                throw new ArgumentNullException("compositionContainer");
            }

            var exports = compositionContainer.GetExports<IServiceRouteConfiguration, Meta<TargetServices>>(compositionContractName);
            foreach (var export in exports)
            {
                foreach (var serviceType in export.Metadata.View.Select(x => x.ServiceType))
                {
                    try
                    {
                        var factory = new ServiceCompositionHostFactory(compositionContainer);
                        var routePrefix = export.Value.GetRoutePrefix(serviceType);
                        var serviceRoute = new ServiceRoute(routePrefix, factory, serviceType);
                        routeCollection.Add(serviceRoute);
                    }
                    finally
                    {
                    }
                }
            }
        }
    }
}

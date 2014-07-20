namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Implements method that can provide route prefix for <see cref="System.ServiceModel.Activation.ServiceRoute" />.
    /// Mark implementing class with <see cref="ExportServiceRouteConfigurationAttribute" />.
    /// </summary>
    /// <remarks>
    /// Exported values are used by <see cref="ServiceRouteExtensions" />.
    /// </remarks>
    public interface IServiceRouteConfiguration
    {
        /// <summary>
        /// Gets the route prefix.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>Route prefix.</returns>
        string GetRoutePrefix(Type serviceType);
    }
}

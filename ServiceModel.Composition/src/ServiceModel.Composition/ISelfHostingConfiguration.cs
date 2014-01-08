namespace ServiceModel.Composition
{
    using System;

    /// <summary>
    /// Implements method that can provide base addresses for <see cref="System.ServiceModel.ServiceHost" />.
    /// Mark implementing class with <see cref="ExportSelfHostingConfigurationAttribute" />.
    /// </summary>
    /// <remarks>
    /// Exported values are used by <see cref="SelfHostingContainer" />.
    /// </remarks>
    public interface ISelfHostingConfiguration
    {
        /// <summary>
        /// Gets the base addresses.
        /// </summary>
        /// <param name="serviceType">The <see cref="System.Type"/> of the service.</param>
        /// <returns>The <see cref="T:System.Array" /> of type <see cref="T:System.Uri" /> that contains the base addresses for the service hosted.</returns>
        Uri[] GetBaseAddresses(Type serviceType);
    }
}

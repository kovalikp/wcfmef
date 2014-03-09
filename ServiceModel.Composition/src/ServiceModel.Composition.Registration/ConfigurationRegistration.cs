namespace ServiceModel.Composition.Registration
{
    using System;
    using System.ComponentModel.Composition.Registration;

    /// <summary>
    /// Rule-based configuration for service configuration.
    /// </summary>
    public static class ConfigurationRegistration
    {
        /// <summary>
        /// Exports configuration for <see cref="SelfHostingContainer"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <returns>The part builder.</returns>
        /// <exception cref="System.ArgumentNullException">partBuilder</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportSelfHostingConfiguration<T>(this PartBuilder<T> partBuilder)
           where T : ISelfHostingConfiguration
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            return partBuilder.ExportSelfHostingConfiguration(null);
        }

        /// <summary>
        /// Exports configuration for <see cref="SelfHostingContainer"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The part builder.</returns>
        /// <exception cref="System.ArgumentNullException">partBuilder</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportSelfHostingConfiguration<T>(this PartBuilder<T> partBuilder, Type serviceType)
           where T : ISelfHostingConfiguration
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            partBuilder.Export<ISelfHostingConfiguration>(x => x
                .ContractTypeIdentity<ISelfHostingConfiguration>()
                .AddMetadata("ServiceType", serviceType));

            return partBuilder;
        }

        /// <summary>
        /// Exports the service configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <returns>The part builder.</returns>
        /// <exception cref="System.ArgumentNullException">partBuilder</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportServiceConfiguration<T>(this PartBuilder<T> partBuilder)
           where T : IServiceConfiguration
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            return partBuilder.ExportServiceConfiguration(null);
        }

        /// <summary>
        /// Exports the service configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">partBuilder</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportServiceConfiguration<T>(this PartBuilder<T> partBuilder, Type serviceType)
           where T : IServiceConfiguration
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            partBuilder.Export<IServiceConfiguration>(x => x
                .ContractTypeIdentity<IServiceConfiguration>()
                .AddMetadata("ServiceType", serviceType));

            return partBuilder;
        }

        /// <summary>
        /// Exports configuration for <see cref="ServiceRouteContainer"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">partBuilder</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportServiceRouteConfiguration<T>(this PartBuilder<T> partBuilder)
           where T : IServiceRouteConfiguration
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            return partBuilder.ExportServiceRouteConfiguration(null);
        }

        /// <summary>
        /// Exports configuration for <see cref="ServiceRouteContainer"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">partBuilder</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportServiceRouteConfiguration<T>(this PartBuilder<T> partBuilder, Type serviceType)
           where T : IServiceRouteConfiguration
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            partBuilder.Export<IServiceRouteConfiguration>(x => x
                .ContractTypeIdentity<IServiceRouteConfiguration>()
                .AddMetadata("ServiceType", serviceType));

            return partBuilder;
        }
    }
}
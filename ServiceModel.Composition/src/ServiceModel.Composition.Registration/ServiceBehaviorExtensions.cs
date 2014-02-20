#if NET45
namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Registration;
    using System.Linq;
    using System.ServiceModel.Description;

    /// <summary>
    /// Rule-based configuration for service behavior.
    /// </summary>
    public static class ServiceBehaviorExtensions
    {
        /// <summary>
        /// Specifies that matching types should be exported as service behavior for any service.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        public static PartBuilder<T> ExportServiceBehavior<T>(this PartBuilder<T> partBuilder)
            where T : IServiceBehavior
        {
            return partBuilder.ExportServiceBehavior(null);
        }

        /// <summary>
        /// Specifies that matching types should be exported as service behavior for specified service type.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportServiceBehavior<T>(this PartBuilder<T> partBuilder, Type serviceType)
            where T : IServiceBehavior
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            partBuilder.Export<IServiceBehavior>(x => x
                .ContractTypeIdentity<IServiceBehavior>()
                .AddMetadata("ServiceType", serviceType));
            return partBuilder;
        }
    }
}
#endif
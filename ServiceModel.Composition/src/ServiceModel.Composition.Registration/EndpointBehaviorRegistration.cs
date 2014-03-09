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
    /// Rule-based configuration for endpoint behavior.
    /// </summary>
    public static class EndpointBehaviorRegistration
    {
        /// <summary>
        /// Specifies that matching types should be exported as endpoint behavior for any endpoint.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        public static PartBuilder<T> ExportEndpointBehavior<T>(this PartBuilder<T> partBuilder)
            where T : IEndpointBehavior
        {
            return partBuilder.ExportEndpointBehavior(null, null);
        }

        /// <summary>
        /// Specifies that matching types should be exported as endpoint behavior for specified service type.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        public static PartBuilder<T> ExportEndpointBehavior<T>(this PartBuilder<T> partBuilder, Type serviceType)
            where T : IEndpointBehavior
        {
            return partBuilder.ExportEndpointBehavior(serviceType, null);
        }

        /// <summary>
        /// Specifies that matching types should be exported as endpoint behavior for specified service type
        /// and/or more criteria.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="endpointBehaviorBuilder">The operation behavior builder.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportEndpointBehavior<T>(
            this PartBuilder<T> partBuilder,
            Type serviceType,
            Action<EndpointBehaviorBuilder> endpointBehaviorBuilder)
            where T : IEndpointBehavior
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            var endpointBehavior = BuildEndpointBehavior(endpointBehaviorBuilder);
            
            partBuilder.Export<IEndpointBehavior>(x => x
                .ContractTypeIdentity<IEndpointBehavior>()
                .AddMetadata("ServiceType", serviceType)
                .AddMetadata("BindingNames", endpointBehavior.BindingNames)
                .AddMetadata("BindingTypes", endpointBehavior.BindingTypes)
                .AddMetadata("ContractNames", endpointBehavior.ContractNames)
                .AddMetadata("ContractTypes", endpointBehavior.ContractTypes)
                .AddMetadata("EndpointNames", endpointBehavior.EndpointNames));
            return partBuilder;
        }


        internal static EndpointBehaviorBuilder BuildEndpointBehavior(
            Action<EndpointBehaviorBuilder> endpointBehaviorConfiguration)
        {
            var endpointBehaviorBuilder = new EndpointBehaviorBuilder();
            if (endpointBehaviorConfiguration != null)
            {
                endpointBehaviorConfiguration(endpointBehaviorBuilder);
            }

            return endpointBehaviorBuilder;
        }
    }
}
#endif
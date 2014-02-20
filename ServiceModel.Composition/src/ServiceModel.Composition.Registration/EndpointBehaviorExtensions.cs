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
    public static class EndpointBehaviorExtensions
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
        /// <param name="operationBehaviorBuilder">The operation behavior builder.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportEndpointBehavior<T>(
            this PartBuilder<T> partBuilder,
            Type serviceType,
            Action<OperationBehaviorBuilder> operationBehaviorBuilder)
            where T : IEndpointBehavior
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }
            
            partBuilder.Export<IEndpointBehavior>(x => x
                .ContractTypeIdentity<IEndpointBehavior>()
                .AddMetadata("ServiceType", serviceType)
                .BuildOperationBehavior(operationBehaviorBuilder));
            return partBuilder;
        }

        internal static ExportBuilder BuildOperationBehavior(
            this ExportBuilder exportBuilder,
            Action<OperationBehaviorBuilder> operationBehaviorConfiguration)
        {
            if (operationBehaviorConfiguration == null)
            {
                return exportBuilder;
            }

            var operationBehaviorBuilder = new OperationBehaviorBuilder(exportBuilder);
            operationBehaviorConfiguration(operationBehaviorBuilder);
            return exportBuilder;
        }
    }
}
#endif
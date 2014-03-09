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
    /// Rule-based configuration for contract behavior.
    /// </summary>
    public static class ContractBehaviorRegistration
    {
        /// <summary>
        /// Specifies so matching types should be exported as contract behavior for any contract.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        public static PartBuilder<T> ExportContractBehavior<T>(this PartBuilder<T> partBuilder)
            where T : IContractBehavior
        {
            return partBuilder.ExportContractBehavior(null);
        }

        /// <summary>
        /// Specifies that matching types should be exported as contract behavior for specified
        /// service contract type.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportContractBehavior<T>(this PartBuilder<T> partBuilder, Type serviceContractType)
           where T : IContractBehavior
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            partBuilder.Export<IContractBehavior>(x => x
                .ContractTypeIdentity<IContractBehavior>()
                .AddMetadata("ServiceContractType", serviceContractType));
            return partBuilder;
        }
    }
}
#endif
// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Registration;
    using System.Linq;
    using System.ServiceModel.Description;

    /// <summary>
    /// Rule-based configuration for operation behavior.
    /// </summary>
    public static class OperationBehaviorExtensions
    {
        /// <summary>
        /// Specifies that matching types should be exported as operation behavior for any contract type and operation name.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        public static PartBuilder<T> ExportOperationBehavior<T>(this PartBuilder<T> partBuilder)
            where T : IOperationBehavior
        {
            return partBuilder.ExportOperationBehavior(null, null);
        }

        /// <summary>
        /// Specifies that matching types should be exported as operation behavior for specified contract type.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        public static PartBuilder<T> ExportOperationBehavior<T>(this PartBuilder<T> partBuilder, Type serviceContractType)
            where T : IOperationBehavior
        {
            return partBuilder.ExportOperationBehavior(serviceContractType, null);
        }

        /// <summary>
        /// Specifies that matching types should be exported as operation behavior for 
        /// specified service contract type and operation names.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <param name="operationNames">The operation names.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        public static PartBuilder<T> ExportOperationBehavior<T>(
            this PartBuilder<T> partBuilder,
            Type serviceContractType,
            IEnumerable<string> operationNames)
            where T : IOperationBehavior
        {
            if (operationNames == null)
            {
                operationNames = Enumerable.Empty<string>();
            }

            return partBuilder.ExportOperationBehavior(serviceContractType, operationNames.ToArray());
        }

        /// <summary>
        /// Specifies that matching types should be exported as operation behavior for 
        /// specified service contract type and operation names.
        /// </summary>
        /// <typeparam name="T">Exported type.</typeparam>
        /// <param name="partBuilder">The part builder.</param>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <param name="operationNames">The operation names.</param>
        /// <returns>The same <see cref="PartBuilder{T}"/> instance so that multiple calls can be chained.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Derived type required to check export contract type.")]
        public static PartBuilder<T> ExportOperationBehavior<T>(
            this PartBuilder<T> partBuilder,
            Type serviceContractType,
            params string[] operationNames)
            where T : IOperationBehavior
        {
            if (partBuilder == null)
            {
                throw new ArgumentNullException("partBuilder");
            }

            partBuilder.Export<IOperationBehavior>(x => x
                .ContractTypeIdentity<IOperationBehavior>()
                .AddMetadata("ServiceContractType", serviceContractType)
                .AddMetadata("OperationNames", operationNames));
            return partBuilder;
        }
    }
}

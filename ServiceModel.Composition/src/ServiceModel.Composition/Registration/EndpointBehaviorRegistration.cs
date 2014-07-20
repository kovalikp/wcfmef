namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.ServiceModel.Description;

    /// <summary>
    /// 
    /// </summary>
    public static class EndpointBehaviorRegistration
    {
        /// <summary>
        /// Adds the export endpoint behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <returns></returns>
        public static ComposablePart AddExportEndpointBehavior(
            this CompositionBatch batch,
            IEndpointBehavior exportedValue)
        {
            return batch.AddExportEndpointBehavior(exportedValue, null, null);
        }

        /// <summary>
        /// Adds the export endpoint behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public static ComposablePart AddExportEndpointBehavior(this CompositionBatch batch,
            IEndpointBehavior exportedValue,
            Type serviceType)
        {
            return batch.AddExportEndpointBehavior(exportedValue, serviceType, null);
        }

        /// <summary>
        /// Adds the export endpoint behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="endpointBehaviorBuilder">The endpoint behavior builder.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">batch</exception>
        public static ComposablePart AddExportEndpointBehavior(
            this CompositionBatch batch,
            IEndpointBehavior exportedValue,
            Type serviceType,
            Action<EndpointBehaviorBuilder> endpointBehaviorBuilder)
        {
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }

            string contractName = AttributedModelServices.GetContractName(typeof(IContractBehavior));
            string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof(IContractBehavior));
            var endpointBehavior = BuildEndpointBehavior(endpointBehaviorBuilder);
            return batch.AddExport(new Export(contractName, new Dictionary<string, object>
            {
                { "ContractTypeIdentity", typeIdentity },
                { "ExportTypeIdentity",typeIdentity },
                { "ServiceType", serviceType },
                { "BindingNames", endpointBehavior.BindingNames },
                { "BindingTypes", endpointBehavior.BindingTypes },
                { "ContractNames", endpointBehavior.ContractNames },
                { "ContractTypes", endpointBehavior.ContractTypes},
                { "EndpointNames", endpointBehavior.EndpointNames },
            }, () => exportedValue));

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

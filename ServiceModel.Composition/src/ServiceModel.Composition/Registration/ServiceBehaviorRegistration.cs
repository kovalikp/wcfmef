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
    /// Rule-based configuration for service behavior.
    /// </summary>
    public static class ServiceBehaviorRegistration
    {
        /// <summary>
        /// Exports the service behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <returns></returns>
        public static ComposablePart ExportServiceBehavior(
            this CompositionBatch batch, 
            IServiceBehavior exportedValue)
        {
            return batch.ExportServiceBehavior(exportedValue, null);
        }

        /// <summary>
        /// Exports the service behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">batch</exception>
        public static ComposablePart ExportServiceBehavior(
            this CompositionBatch batch, 
            IServiceBehavior exportedValue, 
            Type serviceType)
        {
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }

            string contractName = AttributedModelServices.GetContractName(typeof(IServiceBehavior));
            string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof(IServiceBehavior));
            return batch.AddExport(new Export(contractName, new Dictionary<string, object>
            {
                { "ContractTypeIdentity", typeIdentity },
                { "ExportTypeIdentity",typeIdentity },
                { "ServiceType", serviceType }
            }, () => exportedValue));

        }
    }
}
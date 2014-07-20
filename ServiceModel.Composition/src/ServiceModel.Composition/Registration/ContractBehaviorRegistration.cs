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
    public static class ContractBehaviorRegistration
    {
        /// <summary>
        /// Adds the export contract behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <returns></returns>
        public static ComposablePart AddExportContractBehavior(this CompositionBatch batch, 
            IContractBehavior exportedValue)
        {
            return batch.AddExportContractBehavior(exportedValue, null);
        }

        /// <summary>
        /// Adds the export contract behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">batch</exception>
        public static ComposablePart AddExportContractBehavior(this CompositionBatch batch, 
            IContractBehavior exportedValue, 
            Type serviceContractType)
        {
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }

            string contractName = AttributedModelServices.GetContractName(typeof(IContractBehavior));
            string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof(IContractBehavior));
            return batch.AddExport(new Export(contractName, new Dictionary<string, object>
            {
                { "ContractTypeIdentity", typeIdentity },
                { "ExportTypeIdentity",typeIdentity },
                { "ServiceContractType", serviceContractType }
            }, () => exportedValue));
        }
    }
}

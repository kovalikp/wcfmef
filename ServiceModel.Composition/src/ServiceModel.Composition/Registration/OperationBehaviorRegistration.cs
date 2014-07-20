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
    public static class OperationBehaviorRegistration
    {
        /// <summary>
        /// Adds the export contract behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <returns></returns>
        public static ComposablePart AddExportContractBehavior(
            this CompositionBatch batch,
            IOperationBehavior exportedValue)
        {
            return batch.AddOperationContractBehavior(exportedValue, null, null);
        }

        /// <summary>
        /// Adds the operation contract behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <returns></returns>
        public static ComposablePart AddOperationContractBehavior(
            this CompositionBatch batch,
            IOperationBehavior exportedValue,
            Type serviceContractType)
        {
            return batch.AddOperationContractBehavior(exportedValue, serviceContractType, null);
        }

        /// <summary>
        /// Adds the operation contract behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <param name="operationNames">The operation names.</param>
        /// <returns></returns>
        public static ComposablePart AddOperationContractBehavior(
            this CompositionBatch batch,
            IOperationBehavior exportedValue,
            Type serviceContractType,
            IEnumerable<string> operationNames)
        {
            if (operationNames == null)
            {
                    operationNames = Enumerable.Empty<string>();
            }

            return batch.AddOperationContractBehavior(exportedValue, serviceContractType, operationNames.ToArray());
        }

        /// <summary>
        /// Adds the operation contract behavior.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceContractType">Type of the service contract.</param>
        /// <param name="operationNames">The operation names.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">batch</exception>
        public static ComposablePart AddOperationContractBehavior(
            this CompositionBatch batch,
            IOperationBehavior exportedValue,
            Type serviceContractType,
            string[] operationNames)
        {
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }

            string contractName = AttributedModelServices.GetContractName(typeof(IOperationBehavior));
            string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof(IOperationBehavior));
            return batch.AddExport(new Export(contractName, new Dictionary<string, object>
            {
                { "ContractTypeIdentity", typeIdentity },
                { "ExportTypeIdentity",typeIdentity },
                { "ServiceContractType", serviceContractType },
                { "OperationNames", operationNames }
            }, () => exportedValue));
        }
    }
}

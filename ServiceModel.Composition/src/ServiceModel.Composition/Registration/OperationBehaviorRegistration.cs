namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.ServiceModel.Description;

    public static class OperationBehaviorRegistration
    {
        public static ComposablePart AddExportContractBehavior(
            this CompositionBatch batch,
            IOperationBehavior exportedValue)
        {
            return batch.AddOperationContractBehavior(exportedValue, null, null);
        }

        public static ComposablePart AddOperationContractBehavior(
            this CompositionBatch batch,
            IOperationBehavior exportedValue,
            Type serviceContractType)
        {
            return batch.AddOperationContractBehavior(exportedValue, serviceContractType, null);
        }

        public static ComposablePart AddOperationContractBehavior(
            this CompositionBatch batch,
            IOperationBehavior exportedValue,
            Type serviceContractType,
            IEnumerable<string> operationNames)
        {
            return batch.AddOperationContractBehavior(exportedValue, serviceContractType, operationNames.ToArray());
        }

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

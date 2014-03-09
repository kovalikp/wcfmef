namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.ServiceModel.Description;

    public static class ContractBehaviorRegistration
    {
        public static ComposablePart AddExportContractBehavior(this CompositionBatch batch, 
            IContractBehavior exportedValue)
        {
            return batch.AddExportContractBehavior(exportedValue, null);
        }

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

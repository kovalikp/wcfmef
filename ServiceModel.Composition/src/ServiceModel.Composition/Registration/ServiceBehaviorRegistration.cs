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
        public static ComposablePart ExportServiceBehavior(
            this CompositionBatch batch, 
            IServiceBehavior exportedValue)
        {
            return batch.ExportServiceBehavior(exportedValue, null);
        }

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
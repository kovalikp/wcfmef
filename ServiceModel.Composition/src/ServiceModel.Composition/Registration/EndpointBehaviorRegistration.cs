namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.ServiceModel.Description;

    public static class EndpointBehaviorRegistration
    {
        public static ComposablePart AddExportEndpointBehavior(
            this CompositionBatch batch,
            IEndpointBehavior exportedValue)
        {
            return batch.AddExportEndpointBehavior(exportedValue, null, null);
        }

        public static ComposablePart AddExportEndpointBehavior(this CompositionBatch batch,
            IEndpointBehavior exportedValue,
            Type serviceType)
        {
            return batch.AddExportEndpointBehavior(exportedValue, serviceType, null);
        }

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

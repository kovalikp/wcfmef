namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;

    public static class ConfigurationRegistration
    {
        public static ComposablePart AddExportSelfHostingConfiguration(
            this CompositionBatch batch,
            ISelfHostingConfiguration exportedValue)
        {
            return batch.AddExportSelfHostingConfiguration(exportedValue, null);
        }

        public static ComposablePart AddExportSelfHostingConfiguration(
            this CompositionBatch batch,
            ISelfHostingConfiguration exportedValue,
            Type serviceType)
        {
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }

            string contractName = AttributedModelServices.GetContractName(typeof(ISelfHostingConfiguration));
            string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof(ISelfHostingConfiguration));
            return batch.AddExport(new Export(contractName, new Dictionary<string, object>
            {
                { "ContractTypeIdentity", typeIdentity },
                { "ExportTypeIdentity",typeIdentity },
                { "ServiceType", serviceType }
            }, () => exportedValue));
        }

        public static ComposablePart AddExportServiceConfiguration(
            this CompositionBatch batch,
            IServiceConfiguration exportedValue)
        {
            return batch.AddExportServiceConfiguration(exportedValue, null);
        }

        public static ComposablePart AddExportServiceConfiguration(
            this CompositionBatch batch,
            IServiceConfiguration exportedValue,
            Type serviceType)
        {
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }

            string contractName = AttributedModelServices.GetContractName(typeof(IServiceConfiguration));
            string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof(IServiceConfiguration));
            return batch.AddExport(new Export(contractName, new Dictionary<string, object>
            {
                { "ContractTypeIdentity", typeIdentity },
                { "ExportTypeIdentity",typeIdentity },
                { "ServiceType", serviceType }
            }, () => exportedValue));
        }

        public static ComposablePart AddExportServiceRouteConfiguration(this CompositionBatch batch,
            IServiceRouteConfiguration exportedValue)
        {
            return batch.AddExportServiceRouteConfiguration(exportedValue, null);
        }

        public static ComposablePart AddExportServiceRouteConfiguration(this CompositionBatch batch,
            IServiceRouteConfiguration exportedValue,
            Type serviceType)
        {
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }

            string contractName = AttributedModelServices.GetContractName(typeof(IServiceRouteConfiguration));
            string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof(IServiceRouteConfiguration));
            return batch.AddExport(new Export(contractName, new Dictionary<string, object>
            {
                { "ContractTypeIdentity", typeIdentity },
                { "ExportTypeIdentity",typeIdentity },
                { "ServiceType", serviceType }
            }, () => exportedValue));
        }
    }
}
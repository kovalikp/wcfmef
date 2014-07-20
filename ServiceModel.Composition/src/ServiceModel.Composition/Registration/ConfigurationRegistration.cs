namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;

    /// <summary>
    ///
    /// </summary>
    public static class ConfigurationRegistration
    {
        /// <summary>
        /// Creates a part from the specified self hosting configuration and composes it in the specified composition container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="exportedValue">The exported value.</param>
        public static void ComposeExportedSelfHostingConfiguration(
            this CompositionContainer container,
            ISelfHostingConfiguration exportedValue)
        {
            container.ComposeExportedSelfHostingConfiguration(exportedValue, null);
        }

        /// <summary>
        /// Adds the export self hosting configuration.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <exception cref="System.ArgumentNullException">container</exception>
        public static void ComposeExportedSelfHostingConfiguration(
            this CompositionContainer container,
            ISelfHostingConfiguration exportedValue,
            Type serviceType)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            var batch = new CompositionBatch();
            batch.AddExportedSelfHostingConfiguration(exportedValue, serviceType);
            container.Compose(batch);
        }

        /// <summary>
        /// Adds the export self hosting configuration.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <returns></returns>
        public static ComposablePart AddExportedSelfHostingConfiguration(
            this CompositionBatch batch,
            ISelfHostingConfiguration exportedValue)
        {
            return batch.AddExportedSelfHostingConfiguration(exportedValue, null);
        }

        /// <summary>
        /// Adds the export self hosting configuration.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">batch</exception>
        public static ComposablePart AddExportedSelfHostingConfiguration(
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

        /// <summary>
        /// Adds the export service configuration.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="exportedValue">The exported value.</param>
        public static void ComposeExportedServiceConfiguration(
            this CompositionContainer container,
            IServiceConfiguration exportedValue)
        {
            container.ComposeExportedServiceConfiguration(exportedValue, null);
        }

        /// <summary>
        /// Adds the export service configuration.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <exception cref="System.ArgumentNullException">container</exception>
        public static void ComposeExportedServiceConfiguration(
            this CompositionContainer container,
            IServiceConfiguration exportedValue,
            Type serviceType)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            var batch = new CompositionBatch();
            batch.AddExportedServiceConfiguration(exportedValue, serviceType);
            container.Compose(batch);
        }

        /// <summary>
        /// Adds the export service configuration.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">batch</exception>
        public static ComposablePart AddExportedServiceConfiguration(
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

        /// <summary>
        /// Adds the export service route configuration.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="exportedValue">The exported value.</param>
        public static void ComposeExportedServiceRouteConfiguration(
            this CompositionContainer container,
            IServiceRouteConfiguration exportedValue)
        {
            container.ComposeExportedServiceRouteConfiguration(exportedValue, null);
        }

        /// <summary>
        /// Adds the export service route configuration.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <exception cref="System.ArgumentNullException">container</exception>
        public static void ComposeExportedServiceRouteConfiguration(
            this CompositionContainer container,
            IServiceRouteConfiguration exportedValue,
            Type serviceType)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            var batch = new CompositionBatch();
            batch.AddExportedServiceRouteConfiguration(exportedValue, serviceType);
            container.Compose(batch);
        }

        /// <summary>
        /// Adds the export service route configuration.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <returns></returns>
        public static ComposablePart AddExportedServiceRouteConfiguration(
            this CompositionBatch batch,
            IServiceRouteConfiguration exportedValue)
        {
            return batch.AddExportedServiceRouteConfiguration(exportedValue, null);
        }

        /// <summary>
        /// Adds the export service route configuration.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">batch</exception>
        public static ComposablePart AddExportedServiceRouteConfiguration(
            this CompositionBatch batch,
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

        /// <summary>
        /// Adds the exported service route configuration.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="contractName">Name of the contract.</param>
        /// <param name="exportedValue">The exported value.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">batch</exception>
        public static ComposablePart AddExportedServiceRouteConfiguration(
            this CompositionBatch batch,
            string contractName,
            IServiceRouteConfiguration exportedValue,
            Type serviceType)
        {
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }
            
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
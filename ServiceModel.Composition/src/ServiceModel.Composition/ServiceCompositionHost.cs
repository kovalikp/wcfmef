namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Provides a composition host for services.
    /// </summary>
    /// <remarks>
    /// Services with <see cref="ServiceBehaviorAttribute.InstanceContextMode"/> set to <see cref="InstanceContextMode.Single"/>
    /// require default constructor. Service object will be exported on opening and set using
    /// <see cref="ServiceBehaviorAttribute.SetWellKnownSingleton(object)"/>
    /// </remarks>
    public class ServiceCompositionHost : ServiceHost
    {
        private CompositionContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCompositionHost"/> class
        /// with specified composition container, the type of service and its base addresses.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        public ServiceCompositionHost(CompositionContainer container, Type serviceType, Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this._container = container;
        }

        /// <summary>
        /// Applies exported service, contracts, endpoints and operations behaviors.
        /// If service has neither <see cref="ExportServiceAttribute"/> or <see cref="ExportContractAttribute"/>,
        /// it will attach new <see cref="ExportServiceAttribute"/>.
        /// </summary>
        protected override void OnOpening()
        {
            base.OnOpening();
            ApplyServiceBehaviors();

            ApplyContractBehaviors();

            ApplyEndpointBehaviors();

            ApplyOperationBehaviors();

            ServiceBehaviorAttribute serviceBehavior = Description.Behaviors.Find<ServiceBehaviorAttribute>();
            if (serviceBehavior != null && serviceBehavior.InstanceContextMode == InstanceContextMode.Single)
            {
                var singleton = _container.ExportService(this.Description.ServiceType);
                serviceBehavior.SetWellKnownSingleton(singleton);
            }
            else
            {
                ApplyExportBehavior();
            }
        }

        private IEnumerable<ExportContractAttribute> YieldExportServiceAttribute(KeyedCollection<Type, IContractBehavior> contractBehaviors)
        {
            if (contractBehaviors.Contains(typeof(ExportContractAttribute)))
            {
                yield return contractBehaviors[typeof(ExportContractAttribute)] as ExportContractAttribute;
            }
        }

        private void ApplyExportBehavior()
        {
            var exportServices = Description.Behaviors.FindAll<ExportServiceAttribute>();
            var exportContracts = Description.Endpoints.SelectMany(x => YieldExportServiceAttribute(x.Contract.Behaviors)).ToList();

            foreach (var exportContract in exportContracts)
            {
                if (exportContract.Container == null)
                {
                    exportContract.Container = _container;
                }
            }

            foreach (var exportService in exportServices)
            {
                if (exportService.Container == null)
                {
                    exportService.Container = _container;
                }
            }

            if (exportServices.Count + exportContracts.Count == 0)
            {
                Description.Behaviors.Add(new ExportServiceAttribute(_container, null));
            }
        }

        private void ApplyServiceBehaviors()
        {
            var behaviorExports = _container.GetExports<IServiceBehavior, Meta<TargetServices>>();

            foreach (var behavior in behaviorExports)
            {
                if (behavior.Metadata.MatchesExport(Description))
                {
                    Description.Behaviors.Add(behavior.Value);
                }
            }
        }

        private void ApplyEndpointBehaviors()
        {
            var behaviorExports = _container.GetExports<IEndpointBehavior, Meta<TargetEndpoints>>();
            foreach (var endpoint in this.Description.Endpoints)
            {
                foreach (var behavior in behaviorExports)
                {
                    if (behavior.Metadata.MatchesExport(Description, endpoint))
                    {
                        endpoint.Behaviors.Add(behavior.Value);
                    }
                }
            }
        }

        private void ApplyContractBehaviors()
        {
            var behaviorExports = _container.GetExports<IContractBehavior, Meta<TargetContracts>>();

            foreach (var behavior in behaviorExports)
            {
                foreach (var contractDescription in ImplementedContracts.Values)
                {
                    if (behavior.Metadata.MatchesExport(contractDescription))
                    {
                        contractDescription.Behaviors.Add(behavior.Value);
                    }
                }
            }
        }

        private void ApplyOperationBehaviors()
        {
            var behaviorExports = _container.GetExports<IOperationBehavior, Meta<TargetOperations>>();

            foreach (var behavior in behaviorExports)
            {
                foreach (var contractDescription in ImplementedContracts.Values)
                {
                    foreach (var operationDescription in contractDescription.Operations)
                    {
                        if (behavior.Metadata.MatchesExport(contractDescription, operationDescription))
                        {
                            operationDescription.Behaviors.Add(behavior.Value);
                        }
                    }
                }
            }
        }
    }
}
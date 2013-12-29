using ServiceModel.Composition.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
    public class CompositionConfigurator : IServiceDescriptionsConfiguration
    {
        private readonly CompositionContainer _container;
        
        public CompositionConfigurator(CompositionContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Applies exported service, contracts, endpoints and operations behaviors. 
        /// If service has neither <see cref="ExportServiceAttribute"/> or <see cref="ExportContractAttribute"/>,
        /// it will attach new <see cref="ExportServiceAttribute"/>.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="implementedContracts">The implemented contracts.</param>
        public void ApplyConfiguration(ServiceDescription serviceDescription, IEnumerable<ContractDescription> implementedContracts)
        {

            ApplyServiceBehaviors(serviceDescription);

            ApplyEndpointBehaviors(serviceDescription);

            ApplyContractBehaviors(implementedContracts);

            ApplyOperationBehaviors(implementedContracts);

            ServiceBehaviorAttribute serviceBehavior = serviceDescription.Behaviors.Find<ServiceBehaviorAttribute>();
            if (serviceBehavior != null && serviceBehavior.InstanceContextMode == InstanceContextMode.Single)
            {
                var singleton = _container.ExportService(serviceDescription.ServiceType);
                serviceBehavior.SetWellKnownSingleton(singleton);
            }
            else
            {
                ApplyExportBehavior(serviceDescription);
            }
        }

        private IEnumerable<ExportContractAttribute> YieldExportServiceAttribute(KeyedCollection<Type, IContractBehavior> contractBehaviors)
        {
            if (contractBehaviors.Contains(typeof(ExportContractAttribute)))
            {
                yield return contractBehaviors[typeof(ExportContractAttribute)] as ExportContractAttribute;
            }
        }

        private void ApplyExportBehavior(ServiceDescription serviceDescription)
        {
            var exportServices = serviceDescription.Behaviors.FindAll<ExportServiceAttribute>();
            var exportContracts = serviceDescription.Endpoints.SelectMany(x => YieldExportServiceAttribute(x.Contract.ContractBehaviors)).ToList();

            foreach (var exportContract in exportContracts)
            {
                if (exportContract.Container == null)
                    exportContract.Container = _container;
            }

            foreach (var exportService in exportServices)
            {
                if (exportService.Container == null)
                    exportService.Container = _container;
            }

            if (exportServices.Count + exportContracts.Count == 0)
                serviceDescription.Behaviors.Add(new ExportServiceAttribute(_container, null));
        }

        private void ApplyServiceBehaviors(ServiceDescription serviceDescription)
        {
            var behaviorExports = _container.GetExports<IServiceBehavior, Meta<TargetServices>>();

            foreach (var behavior in behaviorExports)
            {
                if (behavior.Metadata.MatchesExport(serviceDescription))
                    serviceDescription.Behaviors.Add(behavior.Value);
            }
        }

        private void ApplyEndpointBehaviors(ServiceDescription serviceDescription)
        {
            var behaviorExports = _container.GetExports<IEndpointBehavior, Meta<TargetEndpoints>>();
            foreach (var endpoint in serviceDescription.Endpoints)
            {
                foreach (var behavior in behaviorExports)
                {
                    if (behavior.Metadata.MatchesExport(serviceDescription, endpoint))
                        endpoint.EndpointBehaviors.Add(behavior.Value);
                }
            }
        }

        private void ApplyContractBehaviors(IEnumerable<ContractDescription> implementedContracts)
        {
            var behaviorExports = _container.GetExports<IContractBehavior, Meta<TargetContracts>>();

            foreach (var behavior in behaviorExports)
            {
                foreach (var contractDescription in implementedContracts)
                {
                    if (behavior.Metadata.MatchesExport(contractDescription))
                        contractDescription.Behaviors.Add(behavior.Value);
                }
            }
        }

        private void ApplyOperationBehaviors(IEnumerable<ContractDescription> implementedContracts)
        {
            var behaviorExports = _container.GetExports<IOperationBehavior, Meta<TargetOperations>>();

            foreach (var behavior in behaviorExports)
            {
                foreach (var contractDescription in implementedContracts)
                {
                    foreach (var operationDescription in contractDescription.Operations)
                    {
                        if (behavior.Metadata.MatchesExport(contractDescription, operationDescription))
                            operationDescription.OperationBehaviors.Add(behavior.Value);
                    }
                }
            }
        }
    }
}

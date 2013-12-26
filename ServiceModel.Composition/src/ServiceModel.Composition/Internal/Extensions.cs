using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition.Internal
{
    static class Extensions
    {
        internal static object ExportService(this CompositionContainer container, Type exportType)
        {
            return container.ExportService(AttributedModelServices.GetContractName(exportType), exportType);
        }
        
        internal static object ExportService(this CompositionContainer container, string contractName, Type exportType)
        {
            return container.ExportService(AttributedModelServices.GetContractName(exportType), AttributedModelServices.GetTypeIdentity(exportType));
        }

        internal static object ExportService(this CompositionContainer container, string contractName, string requiredTypeIdentity)
        {
            var importDefinition = new ContractBasedImportDefinition(
                contractName, requiredTypeIdentity,
                null, ImportCardinality.ZeroOrMore, true, true, CreationPolicy.Any);

            Export export = container.GetExports(importDefinition).FirstOrDefault();

            if (export == null)
                throw new InvalidOperationException();

            return export.Value;
        }

        internal static bool MatchesExport(this Meta<TargetServices> targetServices, ServiceDescription serviceDescription)
        {
            return targetServices.View.Any(x => x.MatchesExport(serviceDescription));
        }

        internal static bool MatchesExport(this ITargetServices targetServices, ServiceDescription serviceDescription)
        {
            return targetServices.ServiceTypes.IsNullOrEmptyOrContains(serviceDescription.ServiceType);
        }
        
        internal static bool MatchesExport(this Meta<TargetEndpoints> targetEnpoints, ServiceDescription serviceDescription, ServiceEndpoint serviceEndpoint)
        {
            return targetEnpoints.View.Any(x => x.MatchesExport(serviceDescription, serviceEndpoint));
        }

        internal static bool MatchesExport(this ITargetEndpoints targetEnpoints, ServiceDescription serviceDescription, ServiceEndpoint serviceEndpoint)
        {
            var matchServiceType = targetEnpoints.ServiceTypes.IsNullOrEmptyOrContains(serviceDescription.ServiceType);
            var matchEndpointName = targetEnpoints.EndpointNames.IsNullOrEmptyOrContains(serviceEndpoint.Name);

            var matchBinding = targetEnpoints.BindingNames.IsNullOrEmpty() && targetEnpoints.BindingTypes.IsNullOrEmpty();
            matchBinding = matchBinding || targetEnpoints.BindingNames.IsNotNullAndContains(serviceEndpoint.Binding.Name);
            matchBinding = matchBinding || targetEnpoints.BindingTypes.IsNotNullAndContains(serviceEndpoint.Binding.GetType());


            var matchContract = targetEnpoints.ContractNames.IsNullOrEmpty() && targetEnpoints.ContractTypes.IsNullOrEmpty();
            matchContract = matchContract || targetEnpoints.ContractNames.IsNotNullAndContains(serviceEndpoint.Contract.Name);
            matchContract = matchContract || targetEnpoints.ContractTypes.IsNotNullAndContains(serviceEndpoint.Contract.ContractType);

            return matchServiceType && matchEndpointName
                && matchBinding && matchContract;
        }

        internal static bool MatchesExport(this Meta<TargetContracts> targetContract, ContractDescription contractDescription)
        {
            return targetContract.View.Any(x => x.MatchesExport(contractDescription));
        }

        internal static bool MatchesExport(this ITargetContracts targetContract, ContractDescription contractDescription)
        {
            return targetContract.ContractTypes.IsNullOrEmptyOrContains(contractDescription.ContractType);
        }

        internal static bool MatchesExport(this Meta<TargetOperations> targetOperations, ContractDescription contractDescription, OperationDescription operationDescription)
        {
            return targetOperations.View.Any(x => x.MatchesExport(contractDescription, operationDescription));
        }

        internal static bool MatchesExport(this ITargetOperations targetOperations, ContractDescription contractDescription, OperationDescription operationDescription)
        {
            var matchContractType = targetOperations.ContractTypes.IsNullOrEmptyOrContains(contractDescription.ContractType);
            var matchOperationName = targetOperations.OperationNames.IsNullOrEmptyOrContains(operationDescription.Name);
            return matchContractType && matchOperationName;
        }

        internal static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }


        internal static bool IsNullOrEmptyOrContains<T>(this T[] array, T item)
        {
            return array == null || array.Length == 0 || array.Contains(item);
        }

        internal static bool IsNotNullAndContains<T>(this T[] array, T item)
        {
            return array != null && array.Contains(item);
        }

        internal static void ConfigureServiceHost(this IEnumerable<Lazy<IServiceConfiguration, Meta<TargetServices>>> exportedConfigurations, ServiceHost serviceHost)
        {
            var configurations = exportedConfigurations
                .Where(x => x.Metadata.MatchesExport(serviceHost.Description)); 
            foreach (var configuration in configurations)
            {
                configuration.Value.Configure(serviceHost);
            }
        }
    }
}

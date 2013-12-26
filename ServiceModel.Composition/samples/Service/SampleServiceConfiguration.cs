using ServiceModel.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [ExportServiceConfiguration(typeof(SampleService))]
    [ExportSelfHostingConfiguration(typeof(SampleService))]
    public class SampleServiceConfiguration : ISelfHostingConfiguration, IServiceConfiguration
    {
        public Uri[] GetBaseAddresses(Type serviceType)
        {
            return new[] { new Uri("http://localhost:12345/SampleService.svc") };
        }

        public void Configure(ServiceHost serviceHost)
        {
            //add default endpoint
            var binding = new WSHttpBinding();
            //binding.Security.Mode = SecurityMode.None;
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            //binding.Security.Message.EstablishSecurityContext = false;
            //ServiceEndpoint endpoint = new ServiceEndpoint(
            //    ContractDescription.GetContract(typeof(ISampleService)), binding, new EndpointAddress(""));
            //serviceHost.AddServiceEndpoint(endpoint);
            serviceHost.AddServiceEndpoint(typeof(ISampleService), binding, "");
            
            //add metadata endpoint and behavior
            var metadataBehavior = new ServiceMetadataBehavior();
            metadataBehavior.HttpGetEnabled = true;
            serviceHost.Description.Behaviors.Add(metadataBehavior);
            serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            
            //add debug behavior
            var debugBehavior = serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debugBehavior == null)
            {
                debugBehavior = new ServiceDebugBehavior();
                serviceHost.Description.Behaviors.Add(debugBehavior);
            }
            debugBehavior.IncludeExceptionDetailInFaults = true;
        }
    }
}

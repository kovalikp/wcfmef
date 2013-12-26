using ServiceModel.Composition;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    [ExportEndpointBehavior(ContractTypes = new[] { typeof(ISampleService) })]
    public class EndpointBehaviorExtension : IEndpointBehavior
    {
        private ILogger _logger;

        [ImportingConstructor]
        public EndpointBehaviorExtension(ILogger logger)
        {
            _logger = logger;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            _logger.Log("Validate ServiceEndpoint '{0}'", endpoint.Name);
        }
    }

    [ExportServiceBehavior]
    public class ServiceBehaviorExtension : IServiceBehavior
    {
        private ILogger _logger;

        [ImportingConstructor]
        public ServiceBehaviorExtension(ILogger logger)
        {
            _logger = logger;

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            _logger.Log("Validate ServiceDescription '{0}'", serviceDescription.Name);
        }
    }

    //[ExportOperationBehavior(typeof(ISampleService))]
    [ExportOperationBehavior(typeof(ISampleService), "Ping")]
    public class OperationBehaviorExtension : IOperationBehavior
    {
        private ILogger _logger;

        [ImportingConstructor]
        public OperationBehaviorExtension(ILogger logger)
        {
            _logger = logger;

        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {

        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {

        }

        public void Validate(OperationDescription operationDescription)
        {
            _logger.Log("Validate OperationDescription '{0}'", operationDescription.Name);

        }
    }

    [ExportContractBehavior]
    public class ContractBehaviorExtension : IContractBehavior
    {
        private ILogger _logger;

        [ImportingConstructor]
        public ContractBehaviorExtension(ILogger logger)
        {
            _logger = logger;

        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {

        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            _logger.Log("Validate ContractDescription '{0}'", contractDescription.Name);

        }
    }
}

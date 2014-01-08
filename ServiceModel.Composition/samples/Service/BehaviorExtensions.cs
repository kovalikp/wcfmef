namespace Service
{
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

    /// <summary>
    /// TODO: Documentation.
    /// </summary>
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

    /// <summary>
    /// TODO: Documentation.
    /// </summary>
    [ExportServiceBehavior]
    public class ServiceBehaviorExtension : IServiceBehavior
    {
        private ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBehaviorExtension"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        [ImportingConstructor]
        public ServiceBehaviorExtension(ILogger logger)
        {
            _logger = logger;

        }

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        /// <param name="endpoints">The service endpoints.</param>
        /// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The host that is currently being built.</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host that is currently being constructed.</param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            _logger.Log("Validate ServiceDescription '{0}'", serviceDescription.Name);
        }
    }

    /// <summary>
    /// TODO: Documentation.
    /// </summary>
    [ExportOperationBehavior(typeof(ISampleService), new[]{"Ping"})]
    public class OperationBehaviorExtension : IOperationBehavior
    {
        private ILogger _logger;

        [ImportingConstructor]
        public OperationBehaviorExtension(ILogger logger)
        {
            _logger = logger;

        }

        /// <summary>
        /// Implement to pass data at runtime to bindings to support custom behavior.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="bindingParameters">The collection of objects that binding elements require to support the behavior.</param>
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// Implements a modification or extension of the client across an operation.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="clientOperation">The run-time object that exposes customization properties for the operation described by <paramref name="operationDescription" />.</param>
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {

        }

        /// <summary>
        /// Implements a modification or extension of the service across an operation.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="dispatchOperation">The run-time object that exposes customization properties for the operation described by <paramref name="operationDescription" />.</param>
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {

        }

        public void Validate(OperationDescription operationDescription)
        {
            _logger.Log("Validate OperationDescription '{0}'", operationDescription.Name);

        }
    }

    /// <summary>
    /// TODO: Documentation.
    /// </summary>
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

namespace ServiceModel.Composition
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    /// <summary>
    /// Specifies that marked service type provides an export.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ExportServiceAttribute : ExportAttribute, IServiceBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceAttribute"/> class
        /// exporting the service type marked with this attribute under the default contract name.
        /// </summary>
        public ExportServiceAttribute()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceAttribute"/> class
        /// exporting the service type marked with this attribute under the specified contract name.
        /// </summary>
        /// <param name="contractName">The contract name that is used to export the type or member marked with this attribute, or null or an empty string ("") to use the default contract name.</param>
        public ExportServiceAttribute(string contractName)
            : this(null, contractName)
        {
        }

        internal ExportServiceAttribute(CompositionContainer container, string contractName)
            : base(contractName)
        {
            Container = container;
        }

        internal CompositionContainer Container { get; set; }

        /// <summary>
        /// This interface method implementation does not do anything.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        /// <param name="endpoints">The service endpoints.</param>
        /// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Sets <see cref="CompositionInstanceProvider"/> for each endpoint dispatcher <see cref="System.ServiceModel.Dispatcher.DispatchRuntime.InstanceProvider"/>.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The host that is currently being built.</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (serviceHostBase == null)
            {
                throw new ArgumentNullException("serviceHostBase");
            }
            
            var instanceProvider = new CompositionInstanceProvider(Container, ContractName);
            foreach (ChannelDispatcherBase dispatcher in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = dispatcher as ChannelDispatcher;

                if (channelDispatcher == null)
                {
                    continue;
                }

                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    endpointDispatcher.DispatchRuntime.InstanceProvider = instanceProvider;
                    endpointDispatcher.DispatchRuntime.InstanceContextInitializers.Add(new CompositionInstanceContextInitializer(Container, ContractName));
                }
            }
        }

        /// <summary>
        /// This interface method implementation does not do anything.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host that is currently being constructed.</param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
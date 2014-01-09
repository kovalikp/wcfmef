namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    /// <summary>
    /// Specifies that marked service provides an export for target contract type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExportContractAttribute : ExportAttribute, IContractBehavior, IContractBehaviorAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportContractAttribute"/> class
        /// exporting the service type marked with this attribute under the default contract name.
        /// </summary>
        public ExportContractAttribute()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportContractAttribute"/> class
        /// exporting the service type marked with this attribute under the specified contract name.
        /// </summary>
        /// <param name="contractType">A type from which to derive the contract name that is used to export the type or member marked with this attribute, or null to use the default contract name.</param>
        public ExportContractAttribute(Type contractType)
            : this(null, contractType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportContractAttribute"/> class
        /// exporting the service type marked with this attribute under the specified contract name.
        /// </summary>
        /// <param name="contractName">The contract name that is used to export the type or member marked with this attribute, or null or an empty string ("") to use the default contract name.</param>
        public ExportContractAttribute(string contractName)
            : this(contractName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportContractAttribute"/> class
        /// exporting the specified contract type marked with this attribute under the specified contract name.
        /// </summary>
        /// <param name="contractName">The contract name that is used to export the type or member marked with this attribute, or null or an empty string ("") to use the default contract name.</param>
        /// <param name="contractType">The type to export.</param>
        public ExportContractAttribute(string contractName, Type contractType)
            : base(contractName, contractType)
        {
        }

        /// <summary>
        /// Gets the type of the contract to which the contract behavior is applicable.
        /// </summary>
        /// <value>The contract to which the contract behavior is applicable.</value>
        public Type TargetContract
        {
            get
            {
                return ContractType;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether use filtered composition container catalog to enable 
        /// per service instancing behavior.
        /// </summary>
        /// <value>
        /// <c>true</c> if use per service instancing behavior; otherwise, <c>false</c>.
        /// </value>
        public bool UsePerServiceInstancing { get; set; }
        
        internal CompositionContainer Container { get; set; }

        /// <summary>
        /// This interface method implementation does not do anything.
        /// </summary>
        /// <param name="contractDescription">The contract description to modify.</param>
        /// <param name="endpoint">The endpoint to modify.</param>
        /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// This interface method implementation does not do anything.
        /// </summary>
        /// <param name="contractDescription">The contract description for which the extension is intended.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="clientRuntime">The client runtime.</param>
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        /// <summary>
        /// Sets <see cref="CompositionInstanceProvider"/> to <see cref="System.ServiceModel.Dispatcher.DispatchRuntime.InstanceProvider"/> of dispatchRuntime parameter.
        /// </summary>
        /// <param name="contractDescription">The contract description to be modified.</param>
        /// <param name="endpoint">The endpoint that exposes the contract.</param>
        /// <param name="dispatchRuntime">The dispatch runtime that controls service execution.</param>
        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            if (dispatchRuntime == null)
            {
                throw new ArgumentNullException("dispatchRuntime");
            }

            dispatchRuntime.InstanceProvider = new CompositionInstanceProvider(Container, ContractName, ContractType);
            dispatchRuntime.InstanceContextInitializers.Add(new CompositionInstanceContextInitializer(UsePerServiceInstancing));
        }

        /// <summary>
        /// This interface method implementation does not do anything.
        /// </summary>
        /// <param name="contractDescription">The contract to validate.</param>
        /// <param name="endpoint">The endpoint to validate.</param>
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}

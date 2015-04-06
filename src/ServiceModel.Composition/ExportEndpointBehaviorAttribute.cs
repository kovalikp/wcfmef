namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition;
    using System.ServiceModel.Description;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Exports implementation of <see cref="System.ServiceModel.Description.IEndpointBehavior" /> for composition.
    /// </summary>
    /// <remarks>
    /// The <see cref="P:ExportEndpointBehaviorAttribute.ServiceTypes" />, <see cref="P:ExportEndpointBehaviorAttribute.EndpointNames" />,
    /// <see cref="P:ExportEndpointBehaviorAttribute.BindingTypes" />, <see cref="P:ExportEndpointBehaviorAttribute.BindingNames" />
    /// <see cref="P:ExportEndpointBehaviorAttribute.ContractTypes" />, <see cref="P:ExportEndpointBehaviorAttribute.ContractNames" />
    /// properties will be used to match behavior to target endpoint.
    /// Empty <see cref="System.Array" /> or <see langword="null" /> values will be matched to any endpoint.
    /// Both BindingTypes and BindingNames must be empty or null to match any endpoint binding.
    /// Both ContractTypes and ContractNames must be empty or null to match any endpoint contract.
    /// You can use attribute multiple for more fine-grained control.
    /// </remarks>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExportEndpointBehaviorAttribute : ExportContractTypeIdentityAttribute, ITargetEndpoints
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportEndpointBehaviorAttribute"/> class 
        /// exporting the marked type for any endpoint of any service type.
        /// </summary>
        public ExportEndpointBehaviorAttribute() 
            : this(null) 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportEndpointBehaviorAttribute"/> class 
        /// exporting the marked type for any endpoint of specified service type.
        /// </summary>
        /// <param name="serviceType">The service types.</param>
        public ExportEndpointBehaviorAttribute(Type serviceType)
            : base(null, typeof(IEndpointBehavior))
        {
            ServiceType = serviceType;
        }

        /// <summary>
        /// Gets the service type that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public Type ServiceType { get; private set; }

        /// <summary>
        /// Gets or sets the endpoint names that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The endpoint names.
        /// </value>
        public string[] EndpointNames { get; set; }

        /// <summary>
        /// Gets or sets the binding names that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The binding names.
        /// </value>
        public string[] BindingNames { get; set; }

        /// <summary>
        /// Gets or sets the binding types that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The binding types.
        /// </value>
        public Type[] BindingTypes { get; set; }

        /// <summary>
        /// Gets or sets the service contract names that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The contract names.
        /// </value>
        public string[] ContractNames { get; set; }

        /// <summary>
        /// Gets or sets the service contract types that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The contract types.
        /// </value>
        public Type[] ContractTypes { get; set; }
    }
}

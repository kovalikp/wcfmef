using ServiceModel.Composition.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
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
            : this(serviceTypes: null, endpointNames: null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportEndpointBehaviorAttribute"/> class 
        /// exporting the marked type for any endpoint of specified service types.
        /// </summary>
        /// <param name="serviceTypes">The service types.</param>
        public ExportEndpointBehaviorAttribute(params Type[] serviceTypes)
            : this(serviceTypes, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportEndpointBehaviorAttribute"/> class 
        /// exporting the marked type for specified endpoints of specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="endpointNames">The endpoint names.</param>
        public ExportEndpointBehaviorAttribute(Type serviceType, params string[] endpointNames)
            : this(new[] { serviceType }, endpointNames)
        {
        }

        private ExportEndpointBehaviorAttribute(Type[] serviceTypes, string[] endpointNames)
            : base(null, typeof(IEndpointBehavior))
        {
            ServiceTypes = serviceTypes;
            EndpointNames = endpointNames;
        }

        /// <summary>
        /// Gets the service types that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public Type[] ServiceTypes { get; private set; }

        /// <summary>
        /// Gets or sets the endpoint names that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The endpoint names.
        /// </value>
        public string[] EndpointNames { get; set; }

        /// <summary>
        /// Gets or sets the binding names that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The binding names.
        /// </value>
        public string[] BindingNames { get; set; }

        /// <summary>
        /// Gets or sets the binding types that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The binding types.
        /// </value>
        public Type[] BindingTypes { get; set; }

        /// <summary>
        /// Gets or sets the service contract names that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The contract names.
        /// </value>
        public string[] ContractNames { get; set; }

        /// <summary>
        /// Gets or sets the service contract types that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The contract types.
        /// </value>
        public Type[] ContractTypes { get; set; }
    }
}

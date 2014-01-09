namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition;
    using System.ServiceModel.Description;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Exports implementation of <see cref="System.ServiceModel.Description.IContractBehavior"/> for composition.
    /// </summary>
    /// <remarks>
    /// The <see cref="P:ExportContractBehaviorAttribute.ContractTypes"/> property will be used to match behavior to target contract.
    /// Empty <see cref="System.Array"/> or <see langword="null" /> values will be matched to any contract.
    /// </remarks>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExportContractBehaviorAttribute : ExportContractTypeIdentityAttribute, ITargetContracts
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportContractBehaviorAttribute"/> class 
        /// exporting the marked type for any service contract type.
        /// </summary>
        public ExportContractBehaviorAttribute()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportContractBehaviorAttribute"/> class 
        /// exporting the marked type for specified service contract type.
        /// </summary>
        /// <param name="serviceContractType">The service contract types.</param>
        public ExportContractBehaviorAttribute(Type serviceContractType)
            : base(null, typeof(IContractBehavior))
        {
            ServiceContractType = serviceContractType;
        }

        /// <summary>
        /// Gets the contract types that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The contract types.
        /// </value>
        public Type ServiceContractType { get; private set; }
    }
}

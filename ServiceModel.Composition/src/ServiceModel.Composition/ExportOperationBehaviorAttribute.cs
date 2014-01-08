namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition;
    using System.ServiceModel.Description;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Exports implementation of <see cref="System.ServiceModel.Description.IOperationBehavior"/> for composition.
    /// </summary>
    /// <remarks>
    /// The <see cref="ExportOperationBehaviorAttribute.ContractType"/>, <see cref="ExportOperationBehaviorAttribute.OperationNames"/>
    /// properties will be used to match behavior to target endpoint.
    /// Empty <see cref="System.Array"/> or <see langword="null" /> values will be matched to any endpoint.
    /// You can use attribute multiple times for more fine-grained control.
    /// </remarks>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExportOperationBehaviorAttribute : ExportContractTypeIdentityAttribute, ITargetOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportOperationBehaviorAttribute"/> class
        /// exporting the marked type for any operation of any contract type.
        /// </summary>
        public ExportOperationBehaviorAttribute()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportOperationBehaviorAttribute"/> class
        /// exporting the marked type for any operation of specified contract types.
        /// </summary>
        /// <param name="serviceContractType">The contract types.</param>
        public ExportOperationBehaviorAttribute(Type serviceContractType)
            : this(serviceContractType, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportOperationBehaviorAttribute"/> class
        /// exporting the marked type for specified operations of specified service contract type.
        /// </summary>
        /// <param name="contractType">Type of the contract.</param>
        /// <param name="operationNames">The operation names.</param>
        public ExportOperationBehaviorAttribute(Type contractType, string[] operationNames)
            : base(null, typeof(IOperationBehavior))
        {
            ServiceContractType = contractType;
            OperationNames = operationNames;
        }

        /// <summary>
        /// Gets the operation names that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The operation names.
        /// </value>
        public string[] OperationNames { get; private set; }

        /// <summary>
        /// Gets the service contract type that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The contract types.
        /// </value>
        public Type ServiceContractType { get; private set; }
    }
}
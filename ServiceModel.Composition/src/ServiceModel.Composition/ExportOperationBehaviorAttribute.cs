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
    /// Exports implementation of <see cref="System.ServiceModel.Description.IOperationBehavior"/> for composition.
    /// </summary>
    /// <remarks>
    /// The <see cref="ExportOperationBehaviorAttribute.ContractTypes"/>, <see cref="ExportOperationBehaviorAttribute.OperationNames"/> 
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
            : this(contractTypes: null, operationNames: null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportOperationBehaviorAttribute"/> class 
        /// exporting the marked type for any operation of specified contract types.
        /// </summary>
        /// <param name="contractTypes">The contract types.</param>
        public ExportOperationBehaviorAttribute(params Type[] contractTypes)
            : this(contractTypes, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportOperationBehaviorAttribute"/> class 
        /// exporting the marked type for specified operations of specified service contract type.
        /// </summary>
        /// <param name="contractType">Type of the contract.</param>
        /// <param name="operationNames">The operation names.</param>
        public ExportOperationBehaviorAttribute(Type contractType, params string[] operationNames)
            : this(new[] { contractType }, operationNames)
        {

        }

        private ExportOperationBehaviorAttribute(Type[] contractTypes, string[] operationNames)
            : base(null, typeof(IOperationBehavior))
        {
            ContractTypes = contractTypes;
            OperationNames = operationNames;
        }


        /// <summary>
        /// Gets the service contract types that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The contract types.
        /// </value>
        public Type[] ContractTypes { get; private set; }

        /// <summary>
        /// Gets or sets the operation names that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The operation names.
        /// </value>
        public string[] OperationNames { get; set; }
    }
}

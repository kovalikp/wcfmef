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
        /// exporting the marked type for spedified service contract types.
        /// </summary>
        /// <param name="contractTypes">The service contract types.</param>
        public ExportContractBehaviorAttribute(params Type[] contractTypes)
            : base(null, typeof(IContractBehavior))
        {
            ContractTypes = contractTypes;
        }

        /// <summary>
        /// Gets the contract types that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The contract types.
        /// </value>
        public Type[] ContractTypes { get; private set; }
    }
}

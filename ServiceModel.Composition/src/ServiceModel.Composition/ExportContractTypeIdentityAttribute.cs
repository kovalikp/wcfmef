using ServiceModel.Composition.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
    /// <summary>
    /// Provides composition contract type identity metadata for exported type.
    /// </summary>
    public abstract class ExportContractTypeIdentityAttribute : ExportAttribute, IContractTypeIdentity
    {
        private string _contractTypeIdentity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportContractTypeIdentityAttribute"/> class.
        /// </summary>
        /// <param name="contractName">The contract name that is used to export the type or member marked with this attribute, or null or an empty string ("") to use the default contract name.</param>
        /// <param name="contractType">The type to export.</param>
        protected ExportContractTypeIdentityAttribute(string contractName, Type contractType)
            :base(contractName, contractType)
        {
            if (contractType == null) throw new ArgumentNullException("contractType");
            _contractTypeIdentity = AttributedModelServices.GetTypeIdentity(contractType);
        }

        /// <summary>
        /// Gets the composition contract type identity.
        /// </summary>
        /// <remarks>
        /// Uses <see cref="AttributedModelServices.GetTypeIdentity(Type)"/> to determine the identity.
        /// </remarks>
        /// <value>
        /// The contract type identity.
        /// </value>
        public string ContractTypeIdentity
        {
            get { return _contractTypeIdentity; }
        }
    }
}

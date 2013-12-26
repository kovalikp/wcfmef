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
    /// Exports implementation of <see cref="ISelfHostingConfiguration"/> for target services.
    /// Target services must be specified to make them discoverable by <see cref="SelfHostingContainer"/>.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ExportSelfHostingConfigurationAttribute : ExportContractTypeIdentityAttribute, ITargetServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportSelfHostingConfigurationAttribute"/> class
        /// for target service types exporting the type marked with this attribute under the default contract name.
        /// </summary>
        /// <param name="serviceTypes">The service types.</param>
        public ExportSelfHostingConfigurationAttribute(params Type[] serviceTypes)
            : this(null, serviceTypes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportSelfHostingConfigurationAttribute"/> class
        /// for target service types exporting the type marked with this attribute under the specified contract name.
        /// </summary>
        /// <param name="contractName">
        /// The contract name that is used to export the type or member marked with this
        /// attribute, or null or an empty string ("") to use the default contract name
        /// </param>
        /// <param name="serviceTypes">The service types.</param>
        public ExportSelfHostingConfigurationAttribute(string contractName, params Type[] serviceTypes)
            : base(contractName, typeof(ISelfHostingConfiguration))
        {
            ServiceTypes = serviceTypes;
        }

        /// <summary>
        /// Gets the service types that will be exported by <see cref="SelfHostingContainer"/>.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public Type[] ServiceTypes { get; private set; }
    }
}

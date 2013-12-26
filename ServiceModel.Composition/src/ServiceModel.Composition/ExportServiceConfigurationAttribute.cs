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
    /// Exports implementation of <see cref="System.ServiceModel.Description.IServiceBehavior"/> for composition.
    /// </summary>
    /// <remarks>
    /// The <see cref="ExportServiceConfigurationAttribute.ServiceTypes"/>
    /// property will be used to match behavior to target service.
    /// Empty <see cref="System.Array"/> or <see langword="null" /> values will be matched to any service.
    /// You can use attribute multiple times. 
    /// </remarks>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ExportServiceConfigurationAttribute : ExportContractTypeIdentityAttribute, ITargetServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceConfigurationAttribute"/> class
        /// for target service types exporting the type marked with this attribute under the default composition contract name.
        /// </summary>
        /// <param name="serviceTypes">The service types.</param>
        public ExportServiceConfigurationAttribute(params Type[] serviceTypes)
            : this(null, serviceTypes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceConfigurationAttribute"/> class
        /// for target service types exporting the type marked with this attribute under the specified composition contract name.
        /// </summary>
        /// <param name="contractName">
        /// The contract name that is used to export the type marked with this
        /// attribute, or null or an empty string ("") to use the default contract name
        /// </param>
        /// <param name="serviceTypes">The service types.</param>
        public ExportServiceConfigurationAttribute(string contractName, params Type[] serviceTypes)
            : base(contractName, typeof(IServiceConfiguration))
        {
            ServiceTypes = serviceTypes;
        }

        /// <summary>
        /// Gets the service types that configurtion should be attached to.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public Type[] ServiceTypes { get; private set; }
    }
}



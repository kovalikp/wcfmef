namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Exports implementation of <see cref="IServiceRouteConfiguration"/> for target services.
    /// Target services must be specified to make them discoverable by <see cref="ServiceRouteExtensions"/>.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ExportServiceRouteConfigurationAttribute : ExportContractTypeIdentityAttribute, ITargetServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceRouteConfigurationAttribute"/> class
        /// for target service types exporting the type marked with this attribute under default contract name.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        public ExportServiceRouteConfigurationAttribute(Type serviceType)
            : this(null, serviceType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceRouteConfigurationAttribute"/> class
        /// for target service types exporting the type marked with this attribute under the specified contract name.
        /// </summary>
        /// <param name="contractName">
        /// The contract name that is used to export the type or member marked with this
        /// attribute, or null or an empty string ("") to use the default contract name.
        /// </param>
        /// <param name="serviceType">The service types.</param>
        public ExportServiceRouteConfigurationAttribute(string contractName, Type serviceType)
            : base(contractName, typeof(ISelfHostingConfiguration))
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            ServiceType = serviceType;
        }

        /// <summary>
        /// Gets the service types that will be exported by <see cref="ServiceRouteExtensions"/>.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public Type ServiceType { get; private set; }
    }
}

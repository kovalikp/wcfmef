namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition;
    using ServiceModel.Composition.Internal;

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
        /// <param name="serviceType">The service types.</param>
        public ExportSelfHostingConfigurationAttribute(Type serviceType)
            : this(null, serviceType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportSelfHostingConfigurationAttribute"/> class
        /// for target service types exporting the type marked with this attribute under the specified contract name.
        /// </summary>
        /// <param name="contractName">
        /// The contract name that is used to export the type or member marked with this
        /// attribute, or null or an empty string ("") to use the default contract name.
        /// </param>
        /// <param name="serviceType">The service types.</param>
        public ExportSelfHostingConfigurationAttribute(string contractName, Type serviceType)
            : base(contractName, typeof(ISelfHostingConfiguration))
        {
            ServiceType = serviceType;
        }
        
        /// <summary>
        /// Gets the service types that will be exported by <see cref="SelfHostingContainer"/>.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public Type ServiceType { get; private set; }
    }
}

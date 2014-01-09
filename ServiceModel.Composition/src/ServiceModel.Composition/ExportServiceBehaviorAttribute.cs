namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition;
    using System.ServiceModel.Description;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Exports implementation of <see cref="System.ServiceModel.Description.IServiceBehavior"/> for composition.
    /// </summary>
    /// <remarks>
    /// The <see cref="P:ExportServiceBehaviorAttribute.ServiceTypes"/> property will be used to match behavior to target service.
    /// Empty <see cref="System.Array"/> or <see langword="null" /> values will be matched to any service.
    /// </remarks>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExportServiceBehaviorAttribute : ExportContractTypeIdentityAttribute, ITargetServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceBehaviorAttribute"/> class
        /// exporting the marked type for any service type.
        /// </summary>
        public ExportServiceBehaviorAttribute()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceBehaviorAttribute"/> class
        /// exporting the marked type for target service type.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        public ExportServiceBehaviorAttribute(Type serviceType)
            : base(null, typeof(IServiceBehavior))
        {
            ServiceType = serviceType;
        }

        /// <summary>
        /// Gets the service type that behavior should be attached to.
        /// </summary>
        /// <value>
        /// The service type.
        /// </value>
        public Type ServiceType { get; private set; }
    }
}

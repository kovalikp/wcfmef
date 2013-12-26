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
        /// exporting the marked type for target service types.
        /// </summary>
        /// <param name="serviceTypes">The service types.</param>
        public ExportServiceBehaviorAttribute(Type[] serviceTypes)
            : base(null, typeof(IServiceBehavior))
        {
            ServiceTypes = serviceTypes;
        }

        /// <summary>
        /// Gets the service types that behaviour should be attached to.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public Type[] ServiceTypes { get; private set; }
    }

}

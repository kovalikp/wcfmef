// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Exports implementation of <see cref="System.ServiceModel.Description.IServiceBehavior"/> for composition.
    /// </summary>
    /// <remarks>
    /// The <see cref="ExportServiceConfigurationAttribute.ServiceType"/>
    /// property will be used to match behavior to target service.
    /// Empty <see cref="System.Array"/> or <see langword="null" /> values will be matched to any service.
    /// You can use attribute multiple times.
    /// </remarks>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ExportServiceConfigurationAttribute : ExportContractTypeIdentityAttribute, ITargetServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceConfigurationAttribute"/> class.
        /// Exports the type marked with this attribute for any service type. Uses the default composition contract name.
        /// </summary>
        public ExportServiceConfigurationAttribute()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceConfigurationAttribute"/> class.
        /// Exports the type marked with this attribute for specified service type. Uses the default composition contract name.
        /// </summary>
        /// <param name="serviceType">The service types.</param>
        public ExportServiceConfigurationAttribute(Type serviceType)
            : this(null, serviceType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportServiceConfigurationAttribute"/> class.
        /// Exports the type marked with this attribute for specified service type. Uses the specified composition contract name.
        /// </summary>
        /// <param name="contractName">
        /// The contract name that is used to export the type marked with this
        /// attribute, or null or an empty string ("") to use the default contract name.
        /// </param>
        /// <param name="serviceType">The service types.</param>
        public ExportServiceConfigurationAttribute(string contractName, Type serviceType)
            : base(contractName, typeof(IServiceConfiguration))
        {
            ServiceType = serviceType;
        }

        /// <summary>
        /// Gets the service types that configuration should be attached to.
        /// </summary>
        /// <value>
        /// The service types.
        /// </value>
        public Type ServiceType { get; private set; }
    }
}

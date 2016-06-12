// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition
{
    using System.ServiceModel;

    /// <summary>
    /// Implements method that can be used to configure <see cref="System.ServiceModel.ServiceHost"/>.
    /// Mark implementation class with<see cref="ExportServiceConfigurationAttribute"/>.
    /// </summary>
    /// <remarks>
    /// Exported values are used by <see cref="ServiceCompositionHostFactoryBase" /> and <see cref="SelfHostingContainer" />.
    /// </remarks>
    public interface IServiceConfiguration
    {
        /// <summary>
        /// Configures the specified service host.
        /// </summary>
        /// <param name="serviceHost">The service host.</param>
        void Configure(ServiceHost serviceHost);
    }
}

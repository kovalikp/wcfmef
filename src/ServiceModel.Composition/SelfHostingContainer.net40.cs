// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition
{
#if NET40
    using System.Collections.Generic;
    using System.ServiceModel;

    /// <content>
    /// Expose ServiceHosts as IEnumerable.
    /// </content>
    public partial class SelfHostingContainer
    {
        /// <summary>
        /// Gets the service hosts.
        /// </summary>
        /// <value>
        /// The service hosts.
        /// </value>
        public IEnumerable<ServiceHost> ServiceHosts
        {
            get
            {
                Initialize();
                return _serviceHosts;
            }
        }
    }
#endif
}

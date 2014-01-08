#if NET40

namespace ServiceModel.Composition
{
    using System.Collections.Generic;

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
        public IEnumerable<ServiceCompositionHost> ServiceHosts
        {
            get
            {
                if (_serviceHosts == null)
                {
                    Initialize();
                }

                return _serviceHosts;
            }
        }
    }
}

#endif
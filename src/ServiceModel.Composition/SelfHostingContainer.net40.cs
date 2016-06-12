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

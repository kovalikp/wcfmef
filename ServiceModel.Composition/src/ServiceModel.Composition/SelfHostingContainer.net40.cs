#if NET40
namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
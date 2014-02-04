namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Service container for web application. 
    /// Uses implementations of <see cref="IServiceRouteConfiguration"/> interface to discover services.
    /// </summary>
    public class ServiceRouteContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRouteContainer"/> class.
        /// </summary>
        public ServiceRouteContainer()
        {
        }
    }
}

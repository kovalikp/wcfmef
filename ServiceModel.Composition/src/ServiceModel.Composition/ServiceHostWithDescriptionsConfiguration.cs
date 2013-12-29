using ServiceModel.Composition.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
    /// <summary>
    /// Provides a composition host for services.
    /// </summary>
    /// <remarks>
    /// Services with <see cref="ServiceBehaviorAttribute.InstanceContextMode" /> set to <see cref="InstanceContextMode.Single" />
    /// require default constructor. Service object will be exported on opening and set using
    /// <see cref="ServiceBehaviorAttribute.SetWellKnownSingleton(object)" />
    /// </remarks>
    public class ServiceHostWithDescriptionsConfiguration : ServiceHost
    {
        private IServiceDescriptionsConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHostWithDescriptionsConfiguration"/> class
        /// with configuration applied during the transition into the opening state, the type of service and its base addresses.
        /// </summary>
        /// <param name="configuration">The on opening configurator.</param>
        /// <param name="serviceType">The type of hosted service.</param>
        /// <param name="baseAddresses">An <see cref="System.Array"/> of type <see cref="System.Uri"/> that contains the base addresses for the
        /// hosted service.</param>
        public ServiceHostWithDescriptionsConfiguration(IServiceDescriptionsConfiguration configuration, Type serviceType, Uri[] baseAddresses)
            //: base(serviceType, baseAddresses)
        {
            _configuration = configuration;
            InitializeDescription(serviceType, new UriSchemeKeyedCollection(baseAddresses));
        }

        protected override void ApplyConfiguration()
        {
            base.ApplyConfiguration();
            _configuration.ApplyConfiguration(Description, ImplementedContracts.Values);
        }
    }
}

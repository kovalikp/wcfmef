// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Manages composition of service instances.
    /// </summary>
    public class CompositionInstanceProvider : IInstanceProvider
    {
        private readonly string _exportContractName;
        private readonly CompositionContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionInstanceProvider"/> class.
        /// </summary>
        /// <param name="container">The composition container.</param>
        /// <param name="exportContractName">Contract name that is used to export the service.</param>
        public CompositionInstanceProvider(CompositionContainer container, string exportContractName)
        {
            _container = container;
            _exportContractName = exportContractName;
        }

        /// <summary>
        /// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext" /> object.
        /// </summary>
        /// <param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext" /> object.</param>
        /// <param name="message">The message that triggered the creation of a service object.</param>
        /// <returns>
        /// The service object.
        /// </returns>
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            if (instanceContext == null)
            {
                throw new ArgumentNullException("instanceContext");
            }

            var serviceType = instanceContext.Host.Description.ServiceType;
            var extension = instanceContext.Extensions.Find<CompositionInstanceContextExtension>();
            return extension.ExportInstance(_exportContractName, serviceType);
        }

        /// <summary>
        /// Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext" /> object.
        /// </summary>
        /// <param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext" /> object.</param>
        /// <returns>
        /// A user-defined service object.
        /// </returns>
        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        /// <summary>
        /// Called when an <see cref="T:System.ServiceModel.InstanceContext" /> object recycles a service object.
        /// </summary>
        /// <param name="instanceContext">The service's instance context.</param>
        /// <param name="instance">The service object to be recycled.</param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            if (instanceContext == null)
            {
                throw new ArgumentNullException("instanceContext");
            }

            var extension = instanceContext.Extensions.Find<CompositionInstanceContextExtension>();
            extension.ReleaseInstance();
        }
    }
}

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
        private readonly string _contractName;
        private readonly Type _contractType;
        private readonly CompositionContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionInstanceProvider"/> class.
        /// </summary>
        /// <param name="container">The composition container.</param>
        /// <param name="contractName">Contract name that is used to export the service.</param>
        /// <param name="contractType">Contract type that is used to export the service.</param>
        public CompositionInstanceProvider(CompositionContainer container, string contractName, Type contractType)
        {
            _container = container;
            _contractName = contractName;
            _contractType = contractType;
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

            var extension = instanceContext.Extensions.Find<CompositionInstanceContextExtension>();
            var container = extension.GetInsanceContextContainer(_container);

            var contractType = _contractType ?? instanceContext.Host.Description.ServiceType;
            return _contractName == null
                ? container.ExportService(contractType)
                : container.ExportService(_contractName, contractType);
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
            if (extension != null)
            {
                extension.DisposeInstanceContextContainer();
            }
            else
            {
                var disposable = instance as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}

namespace ServiceModel.Composition
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;

    internal class CompositionInstanceContextInitializer : IInstanceContextInitializer
    {
        private bool _filterCatalog;

        public CompositionInstanceContextInitializer(bool filterCatalog)
        {
            _filterCatalog = filterCatalog;
        }

        public void Initialize(InstanceContext instanceContext, Message message)
        {
            if (instanceContext == null)
            {
                throw new ArgumentNullException("instanceContext");
            }

            instanceContext.Extensions.Add(new CompositionInstanceContextExtension(_filterCatalog));
        }
    }
}
namespace ServiceModel.Composition
{
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
            instanceContext.Extensions.Add(new CompositionInstanceContextExtension(_filterCatalog));
        }
    }
}
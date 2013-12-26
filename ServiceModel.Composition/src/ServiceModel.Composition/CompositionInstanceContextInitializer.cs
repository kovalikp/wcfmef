using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
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

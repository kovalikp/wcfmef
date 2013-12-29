using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
    public interface IServiceDescriptionsConfiguration
    {
        void ApplyConfiguration(ServiceDescription serviceDescription, IEnumerable<ContractDescription> implementedContracts);
    }
}

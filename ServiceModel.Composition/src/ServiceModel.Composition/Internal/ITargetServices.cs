using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition.Internal
{
    /// <summary>
    /// Metadata information for <see cref="ExportServiceBehaviorAttribute"/>.
    /// </summary>
    internal interface ITargetServices : IContractTypeIdentity
    {
        Type[] ServiceTypes { get; }
    }

    internal class TargetServices : ITargetServices
    {
        public Type[] ServiceTypes { get; set; }

        public string ContractTypeIdentity { get; set; }
    }
}

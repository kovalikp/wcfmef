using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition.Internal
{
    /// <summary>
    /// Metadata information for <see cref="ExportContractBehaviorAttribute"/>.
    /// </summary>
    internal interface ITargetContracts : IContractTypeIdentity
    {
        Type[] ContractTypes { get; }
    }

    internal class TargetContracts : ITargetContracts
    {

        public Type[] ContractTypes { get; set; }

        public string ContractTypeIdentity { get; set; }
    }
}

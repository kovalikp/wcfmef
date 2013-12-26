using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition.Internal
{
    /// <summary>
    /// Metadata information for <see cref="ExportOperationBehaviorAttribute"/>.
    /// </summary>
    internal interface ITargetOperations : IContractTypeIdentity
    {
        Type[] ContractTypes { get; }

        string[] OperationNames { get; }
    }

    internal class TargetOperations : ITargetOperations
    {
        public Type[] ContractTypes { get; set; }

        public string[] OperationNames { get; set; }

        public string ContractTypeIdentity { get; set; }
    }
}

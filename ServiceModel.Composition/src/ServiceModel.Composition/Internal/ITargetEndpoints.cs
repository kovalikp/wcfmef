using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition.Internal
{
    /// <summary>
    /// Metadata information for <see cref="ExportEndpointBehaviorAttribute"/>.
    /// </summary>
    internal interface ITargetEndpoints : IContractTypeIdentity
    {
        Type[] ServiceTypes { get; }

        string[] EndpointNames { get; }

        string[] ContractNames { get; }

        Type[] ContractTypes { get; }

        string[] BindingNames { get; }

        Type[] BindingTypes { get; }
    }

    internal class TargetEndpoints : ITargetEndpoints
    {
        public Type[] ServiceTypes { get; set; }

        public string[] EndpointNames { get; set; }

        public string[] ContractNames { get; set; }

        public Type[] ContractTypes { get; set; }

        public string[] BindingNames { get; set; }

        public Type[] BindingTypes { get; set; }

        public string ContractTypeIdentity { get; set; }
    }
}

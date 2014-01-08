namespace ServiceModel.Composition.Internal
{
    using System;

    /// <summary>
    /// Metadata information for <see cref="ExportContractBehaviorAttribute"/>.
    /// </summary>
    internal interface ITargetContracts : IContractTypeIdentity
    {
        Type ServiceContractType { get; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Used by Meta class in exprepression tree.")]
    internal class TargetContracts : ITargetContracts
    {
        public Type ServiceContractType { get; set; }

        public string ContractTypeIdentity { get; set; }
    }
}

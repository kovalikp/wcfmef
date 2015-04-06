namespace ServiceModel.Composition.Internal
{
    using System;

    /// <summary>
    /// Metadata information for <see cref="ExportOperationBehaviorAttribute"/>.
    /// </summary>
    internal interface ITargetOperations : IContractTypeIdentity
    {
        Type ServiceContractType { get; }

        string[] OperationNames { get; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Used by Meta class in exprepression tree.")]
    internal class TargetOperations : ITargetOperations
    {
        public Type ServiceContractType { get; set; }

        public string[] OperationNames { get; set; }

        public string ContractTypeIdentity { get; set; }
    }
}
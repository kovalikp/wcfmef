﻿namespace ServiceModel.Composition.Internal
{
    using System;

    /// <summary>
    /// Metadata information for <see cref="ExportServiceBehaviorAttribute"/>.
    /// </summary>
    internal interface ITargetServices : IContractTypeIdentity
    {
        Type ServiceType { get; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Used by Meta class in exprepression tree.")]
    internal class TargetServices : ITargetServices
    {
        public Type ServiceType { get; set; }

        public string ContractTypeIdentity { get; set; }
    }
}
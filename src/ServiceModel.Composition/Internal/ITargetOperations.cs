// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition.Internal
{
    using System;

    /// <summary>
    /// Metadata information for <see cref="ExportOperationBehaviorAttribute"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Internal.")]
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
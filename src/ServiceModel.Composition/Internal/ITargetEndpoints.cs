﻿// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition.Internal
{
    using System;

    /// <summary>
    /// Metadata information for <see cref="ExportEndpointBehaviorAttribute"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Internal.")]
    internal interface ITargetEndpoints : IContractTypeIdentity
    {
        Type ServiceType { get; }

        string[] EndpointNames { get; }

        string[] ContractNames { get; }

        Type[] ContractTypes { get; }

        string[] BindingNames { get; }

        Type[] BindingTypes { get; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Used by Meta class in exprepression tree.")]
    internal class TargetEndpoints : ITargetEndpoints
    {
        public Type ServiceType { get; set; }

        public string[] EndpointNames { get; set; }

        public string[] ContractNames { get; set; }

        public Type[] ContractTypes { get; set; }

        public string[] BindingNames { get; set; }

        public Type[] BindingTypes { get; set; }

        public string ContractTypeIdentity { get; set; }
    }
}
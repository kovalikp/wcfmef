// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Registration;
    using System.Linq;
    using System.ServiceModel.Description;

    internal static class BehaviorExtensions
    {
        internal static ExportBuilder ContractTypeIdentity<T>(this ExportBuilder exportBuilder)
        {
            return exportBuilder.AddMetadata("ContractTypeIdentity", AttributedModelServices.GetTypeIdentity(typeof(T)));
        }
    }
}
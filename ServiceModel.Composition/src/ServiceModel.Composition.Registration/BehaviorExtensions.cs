#if NET45
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
#endif
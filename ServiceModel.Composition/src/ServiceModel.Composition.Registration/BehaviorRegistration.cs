#if NET45

namespace ServiceModel.Composition.Registration
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Registration;

    internal static class BehaviorRegistration
    {
        internal static ExportBuilder ContractTypeIdentity<T>(this ExportBuilder exportBuilder)
        {
            return exportBuilder.AddMetadata("ContractTypeIdentity", AttributedModelServices.GetTypeIdentity(typeof(T)));
        }
    }
}

#endif          
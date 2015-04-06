using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: SatelliteContractVersion("1.0.0.0")]
[assembly: AssemblyCompany("Pavol Kovalik")]
[assembly: AssemblyCopyright("© Pavol Kovalik.  All rights reserved.")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyProduct("ServiceModel.Composition")]
[assembly: AssemblyMetadata("Serviceable", "True")]
#if NET40

namespace System.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    internal sealed class AssemblyMetadataAttribute : Attribute
    {
        public AssemblyMetadataAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}

#endif
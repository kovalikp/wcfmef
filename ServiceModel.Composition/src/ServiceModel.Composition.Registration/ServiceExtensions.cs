using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition.Registration
{
    public static class ServiceExtensions
    {
        public static ExportBuilder UsePerServiceInstancing(this ExportBuilder exportBuilder)
        {
            Contract.Requires<ArgumentNullException>(exportBuilder != null, "exportBuilder");
            exportBuilder.AddMetadata("UsePerServiceInstancing", true);
            return exportBuilder;
        }

        //public static PartBuilder ExportSelfHostingConfiguration(this PartBuilder export, Type serviceType)
        //{
            
        //    return export;
        //}
    }
}

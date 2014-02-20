#if NET45
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Composition.Registration
{
    /// <summary>
    /// Rule-based configuration for services and their dependencies.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Specifies that matching types should be exported per service intance.
        /// </summary>
        /// <param name="exportBuilder">The export builder.</param>
        /// <returns></returns>
        public static ExportBuilder UsePerServiceInstancing(this ExportBuilder exportBuilder)
        {
            Contract.Requires<ArgumentNullException>(exportBuilder != null, "exportBuilder");
            if (exportBuilder == null)
            {
                throw new ArgumentNullException("exportBuilder");
            }

            exportBuilder.AddMetadata("UsePerServiceInstancing", true);
            return exportBuilder;
        }

        //public static PartBuilder ExportSelfHostingConfiguration(this PartBuilder export, Type serviceType)
        //{

        //    return export;
        //}
    }
}
#endif
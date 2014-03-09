#if NET45
namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Registration;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Rule-based configuration for services and their dependencies.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Specifies that matching types should be exported per service intance.
        /// </summary>
        /// <param name="exportBuilder">The export builder.</param>
        /// <returns>The export builder.</returns>
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

        /// <summary>
        /// Exports the service.
        /// </summary>
        /// <param name="exportBuilder">The export builder.</param>
        /// <returns>The export builder.</returns>
        /// <exception cref="System.ArgumentNullException">exportBuilder</exception>
        public static ExportBuilder ExportService(this ExportBuilder exportBuilder)
        {
            if (exportBuilder == null)
            {
                throw new ArgumentNullException("exportBuilder");
            }
            exportBuilder.AddMetadata("ExportingType", typeof(ExportServiceAttribute));
            return exportBuilder;
        }

        /// <summary>
        /// Exports the service.
        /// </summary>
        /// <param name="exportBuilder">The export builder.</param>
        /// <param name="usePerServiceInstancing">if set to <c>true</c> to use per service instancing behavior.</param>
        /// <returns>The export builder.</returns>
        /// <exception cref="System.ArgumentNullException">exportBuilder</exception>
        public static ExportBuilder ExportService(this ExportBuilder exportBuilder, bool usePerServiceInstancing)
        {
            if (exportBuilder == null)
            {
                throw new ArgumentNullException("exportBuilder");
            }
            exportBuilder.AddMetadata("ExportingType", typeof(ExportServiceAttribute));
            exportBuilder.AddMetadata("UsePerServiceInstancing", usePerServiceInstancing);
            return exportBuilder;
        }
    }
}
#endif
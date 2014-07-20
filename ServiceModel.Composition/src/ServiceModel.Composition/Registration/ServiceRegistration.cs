namespace ServiceModel.Composition.Registration
{
    using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
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
        /// Exports the service.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">batch</exception>
        public static CompositionBatch AddExportedService<TServiceType>(this CompositionBatch batch)
            where TServiceType : new()
        {
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }
            string contractName = AttributedModelServices.GetContractName(typeof(TServiceType));
            string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof(TServiceType));
            
            batch.AddExport(new Export(contractName, new Dictionary<string, object>
            {
                { "ContractTypeIdentity", typeIdentity },
                { "ExportTypeIdentity",typeIdentity }
            }, () => new TServiceType()));
            return batch;
        }

        ///// <summary>
        ///// Exports the service.
        ///// </summary>
        ///// <param name="batch">The export builder.</param>
        ///// <param name="usePerServiceInstancing">if set to <c>true</c> to use per service instancing behavior.</param>
        ///// <returns>The export builder.</returns>
        ///// <exception cref="System.ArgumentNullException">CompositionBatch</exception>
        //public static CompositionBatch ExportService(this CompositionBatch batch, bool usePerServiceInstancing)
        //{
        //    if (batch == null)
        //    {
        //        throw new ArgumentNullException("batch");
        //    }
        //    //CompositionBatch.AddMetadata("ExportingType", typeof(ExportServiceAttribute));
        //    //CompositionBatch.AddMetadata("UsePerServiceInstancing", usePerServiceInstancing);
        //    return batch;
        //}
    }
}

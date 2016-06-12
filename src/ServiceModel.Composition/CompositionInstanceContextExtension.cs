namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.ServiceModel;
    using ServiceModel.Composition.Internal;

    /// <summary>
    /// Provides methods to satisfy imports on an existing part instance, using composition specific to service instance context.
    /// </summary>
    public sealed class CompositionInstanceContextExtension : IExtension<InstanceContext>, ICompositionService
    {
        private readonly CompositionContainer _container;
        private Export _export;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionInstanceContextExtension"/> class.
        /// </summary>
        public CompositionInstanceContextExtension(CompositionContainer container, string exportContractName)
        {
            _container = container;
            ExportContractName = exportContractName;
        }

        public string ExportContractName { get; private set; }

        public IDictionary<string, object> ExportMetadata
        {
            get
            {
                if (_export == null)
                {
                    throw new InvalidOperationException("");
                }

                return _export.Metadata;
            }
        }

        /// <summary>
        /// Composes the specified part, with recomposition and validation disabled.
        /// </summary>
        /// <param name="part">The part to compose.</param>
        /// <exception cref="InvalidOperationException">
        /// Instance context composition container is not available.
        /// The container is not initialized yet or it is already disposed.
        /// </exception>
        public void SatisfyImportsOnce(ComposablePart part)
        {
            _container.SatisfyImportsOnce(part);
        }

        internal object ExportInstance(string contractName, Type serviceType)
        {
            _export = _container.ExportService(contractName, serviceType);
            return _export.Value;
        }

        internal void ReleaseInstance()
        {
            _container.ReleaseExport(_export);
            _export = null;
        }

        void IExtension<InstanceContext>.Attach(InstanceContext owner)
        {
            // pass
        }

        void IExtension<InstanceContext>.Detach(InstanceContext owner)
        {
            // pass
        }
    }
}
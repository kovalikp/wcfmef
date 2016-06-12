// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        /// Initializes a new instance of the <see cref="CompositionInstanceContextExtension" /> class.
        /// </summary>
        /// <param name="container">The composition container.</param>
        /// <param name="exportContractName">Name of the export contract.</param>
        public CompositionInstanceContextExtension(CompositionContainer container, string exportContractName)
        {
            _container = container;
            ExportContractName = exportContractName;
        }

        /// <summary>
        /// Gets contract name of the exported value.
        /// </summary>
        /// <value>
        /// The name of the export contract.
        /// </value>
        public string ExportContractName { get; private set; }

        /// <summary>
        /// Gets metadata of the exported value.
        /// </summary>
        /// <value>
        /// The export metadata.
        /// </value>
        /// <exception cref="System.InvalidOperationException">Value has not been exported.</exception>
        public IDictionary<string, object> ExportMetadata
        {
            get
            {
                if (_export == null)
                {
                    throw new InvalidOperationException("Value has not been exported.");
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

        /// <summary>
        /// Enables an extension object to find out when it has been aggregated. Called when
        /// the extension is added to the <see cref="IExtensibleObject{T}.Extensions"/> property.
        /// </summary>
        /// <param name="owner">The extensible object that aggregates this extension.</param>
        void IExtension<InstanceContext>.Attach(InstanceContext owner)
        {
            // pass
        }

        /// <summary>
        /// Enables an object to find out when it is no longer aggregated. Called when an
        /// extension is removed from the <see cref="IExtensibleObject{T}.Extensions"/> property.
        /// </summary>
        /// <param name="owner">The extensible object that aggregates this extension.</param>
        void IExtension<InstanceContext>.Detach(InstanceContext owner)
        {
            // pass
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
    }
}
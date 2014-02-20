namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading;

    /// <summary>
    /// Provides methods to satisfy imports on an existing part instance, using composition specific to service instance context.
    /// </summary>
    public sealed class CompositionInstanceContextExtension : IExtension<InstanceContext>, ICompositionService
    {
        private CompositionContainer _container;

        private bool _containerInitialized;

        private object _containerLock = new object();

        private bool _filterCatalog;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionInstanceContextExtension" /> class.
        /// </summary>
        /// <param name="filterCatalog">If set to <c>true</c> filter composition container catalog .</param>
        public CompositionInstanceContextExtension(bool filterCatalog)
        {
            _filterCatalog = filterCatalog;
        }

        /// <summary>
        /// Composes the specified part, with recomposition and validation disabled.
        /// </summary>
        /// <param name="part">The part to compose.</param>
        /// <exception cref="InvalidOperationException">
        /// Instance context composition container is not available. The container is not initialized yet or it is
        /// already disposed.
        /// </exception>
        public void SatisfyImportsOnce(ComposablePart part)
        {
            var container = _container;
            if (container == null)
            {
                throw new InvalidOperationException("Instance context composition container is not available.");
            }

            container.SatisfyImportsOnce(part);
        }

        void IExtension<InstanceContext>.Attach(InstanceContext owner)
        {
            // pass
        }

        void IExtension<InstanceContext>.Detach(InstanceContext owner)
        {
            // pass
        }

        internal CompositionContainer GetInsanceContextContainer(CompositionContainer container)
        {
            CompositionContainer childContainer = null;
            childContainer = LazyInitializer.EnsureInitialized(
                ref _container,
                ref _containerInitialized,
                ref _containerLock,
                () =>
                {
                    if (_filterCatalog && container.Catalog != null)
                    {
                        return CreateContainer(FilterCatalog(container), container);
                    }

                    return new CompositionContainer(container);
                });
            if (childContainer == null)
            {
                throw new InvalidOperationException("Instance context composition container is not available.");
            }

            return childContainer;
        }

        internal void DisposeInstanceContextContainer()
        {
            lock (_containerLock)
            {
                if (_container != null)
                {
                    _container.Dispose();
                    _container = null;
                }
            }
        }

        private static FilteredCatalog FilterCatalog(CompositionContainer container)
        {
            return new FilteredCatalog(container.Catalog, x => x.ExportDefinitions.Any(ed => UsePerServiceInstancing(ed.Metadata)));
        }

        private static CompositionContainer CreateContainer(FilteredCatalog catalog, CompositionContainer container)
        {
            return new CompositionContainer(catalog, container);
        }

        private static bool UsePerServiceInstancing(IDictionary<string, object> metadata)
        {
            object value;
            if (metadata.TryGetValue("UsePerServiceInstancing", out value))
            {
                return (value is bool) && (bool)value;
            }

            return false;
        }
    }
}
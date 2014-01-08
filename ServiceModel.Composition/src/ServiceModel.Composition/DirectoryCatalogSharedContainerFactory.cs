namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Threading;

    /// <summary>
    /// Provides shared (static) thread safe composition container.
    /// Catalog loads parts from application directory and private binaries path.
    /// </summary>
    public class DirectoryCatalogSharedContainerFactory : ServiceCompositionHostFactoryBase
    {
        private static CompositionContainer _container;

        private static bool _containerInitialized;

        private static object _containerLock = new object();

        /// <summary>
        /// Gets shared (static) <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer" />
        /// with aggregate parts catalog of directory catalogs.
        /// Probes <see cref="System.AppDomainSetup.ApplicationBase"/> and <see cref="System.AppDomainSetup.PrivateBinPath"/>
        /// of <see cref="System.AppDomain.CurrentDomain"/>.
        /// </summary>
        /// <value>
        /// The shared composition container.
        /// </value>
        public static CompositionContainer SharedContainer
        {
            get
            {
                return LazyInitializer.EnsureInitialized(ref _container, ref _containerInitialized, ref _containerLock, ContainerFactory);
            }
        }

        /// <summary>
        /// Creates <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer" />
        /// with aggregate parts catalog of directory catalogs.
        /// Probes <see cref="System.AppDomainSetup.ApplicationBase"/> and <see cref="System.AppDomainSetup.PrivateBinPath"/>
        /// of <see cref="System.AppDomain.CurrentDomain"/>.
        /// </summary>
        /// <returns>Composition container.</returns>
        protected override CompositionContainer CreateContainer()
        {
            return SharedContainer;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private static CompositionContainer ContainerFactory()
        {
            AggregateCatalog catalog = null;
            AggregateCatalog tmpCatalog = null;
            CompositionContainer container = null;
            try
            {
                tmpCatalog = CreateCatalog();
                container = new CompositionContainer(catalog, true);
                catalog = tmpCatalog;
                tmpCatalog = null;
            }
            finally
            {
                if (tmpCatalog != null)
                {
                    tmpCatalog.Dispose();
                }
            }

            return container;
        }

        private static AggregateCatalog CreateCatalog()
        {
            var catalogs = new List<ComposablePartCatalog>();
            var setup = AppDomain.CurrentDomain.SetupInformation;

            try
            {
                if (setup != null)
                {
                    catalogs.Add(new DirectoryCatalog(setup.ApplicationBase));

                    if (!string.IsNullOrEmpty(setup.PrivateBinPath))
                    {
                        catalogs.Add(new DirectoryCatalog(setup.PrivateBinPath));
                    }
                }
            }
            finally
            {
                foreach (var x in catalogs)
                {
                    x.Dispose();
                }
            }

            return new AggregateCatalog(catalogs);
        }
    }
}
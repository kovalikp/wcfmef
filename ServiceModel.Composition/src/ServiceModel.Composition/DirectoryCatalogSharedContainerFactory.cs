using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceModel.Composition
{
    /// <summary>
    /// Provides shared (static) thread safe composition contanier.
    /// Catalog loads parts from application directory and private binaries path.
    /// </summary>
    public class DirectoryCatalogSharedContainerFactory : ServiceCompositionHostFactoryBase
    {
        private static CompositionContainer _container;

        private static bool _containerInitialized;

        private static object _containerLock = new object();

        /// <summary>
        /// Creates <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer" />
        /// with aggregate parts catalog of directory catalogs. 
        /// Probes <see cref="System.AppDomainSetup.ApplicationBase"/> and <see cref="System.AppDomainSetup.PrivateBinPath"/>
        /// of <see cref="System.AppDomain.CurrentDomain"/>.
        /// </summary>
        /// <returns>Composition container.</returns>
        protected override CompositionContainer GetContainer()
        {
            return SharedContainer;
        }

        /// <summary>
        /// Get shared (static) <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer" />
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
                return LazyInitializer.EnsureInitialized(ref _container, ref _containerInitialized, ref _containerLock, CreateContainer);    
            }
        }

        private static CompositionContainer CreateContainer()
        {
            var catalogs = new List<ComposablePartCatalog>();
            var setup = AppDomain.CurrentDomain.SetupInformation;

            if (setup != null)
            {
                catalogs.Add(new DirectoryCatalog(setup.ApplicationBase));

                if (!string.IsNullOrEmpty(setup.PrivateBinPath))
                    catalogs.Add(new DirectoryCatalog(setup.PrivateBinPath));
            }

            var catalog = new AggregateCatalog(catalogs);

            return new CompositionContainer(catalog, true);
        }
    }
}

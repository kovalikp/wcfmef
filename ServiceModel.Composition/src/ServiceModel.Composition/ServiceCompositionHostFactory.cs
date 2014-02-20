﻿namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Simple implementation of <see cref="ServiceCompositionHostFactoryBase" /> that accepts <see
    /// cref="CompositionContainer" /> in constructor.
    /// </summary>
    public class ServiceCompositionHostFactory : ServiceCompositionHostFactoryBase
    {
        private CompositionContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCompositionHostFactory" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ServiceCompositionHostFactory(CompositionContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        /// <summary>
        /// When overridden in derived class, gets <see
        /// cref="System.ComponentModel.Composition.Hosting.CompositionContainer" /> with custom parts catalog and/or
        /// export providers.
        /// </summary>
        /// <returns>Configured <see cref="CompositionContainer" />.</returns>
        protected override CompositionContainer GetContainer()
        {
            return _container;
        }
    }
}
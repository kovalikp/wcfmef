// Copyright (c) Pavol Kovalik. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ServiceModel.Composition
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;

    internal class CompositionInstanceContextInitializer : IInstanceContextInitializer
    {
        private readonly CompositionContainer _container;
        private readonly string _exportContractName;

        public CompositionInstanceContextInitializer(CompositionContainer container, string exportContractName)
        {
            _container = container;
            _exportContractName = exportContractName;
        }

        public void Initialize(InstanceContext instanceContext, Message message)
        {
            if (instanceContext == null)
            {
                throw new ArgumentNullException("instanceContext");
            }

            instanceContext.Extensions.Add(new CompositionInstanceContextExtension(_container, _exportContractName));
        }
    }
}
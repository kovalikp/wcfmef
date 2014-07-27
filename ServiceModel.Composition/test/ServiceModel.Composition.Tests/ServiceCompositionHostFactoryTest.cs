using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ServiceModel.Composition.Registration;

namespace ServiceModel.Composition.Tests
{
    public class ServiceCompositionHostFactoryTest
    {
        
        [Fact]
        public void CreateServiceHost()
        {
            var catalog = new TypeCatalog(typeof(FooService));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(FooService).FullName, new Uri[] { });
            Assert.NotNull(result);
            Assert.IsType<ServiceCompositionHost>(result);
            Assert.Equal(result.Description.ServiceType, typeof(FooService));
        }
        
        [Fact]
        public void CreateServiceHostWithContractName()
        {
            var catalog = new TypeCatalog(typeof(BarService));
            var container = new CompositionContainer(catalog);
            //container.ComposeExportedValue()
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(BarService).FullName, new Uri[] { });
            Assert.NotNull(result);
            Assert.IsType<ServiceCompositionHost>(result);
            Assert.Equal(result.Description.ServiceType, typeof(BarService));
        }
    }
}

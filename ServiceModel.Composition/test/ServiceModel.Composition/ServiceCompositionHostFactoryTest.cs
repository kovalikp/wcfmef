using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceModel.Composition
{
    public class ServiceCompositionHostFactoryTest
    {
        [Fact]
        public void Test()
        {
            var container = new CompositionContainer();
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost("ServiceModel.Composition.ServiceCompositionHostFactoryTests.Service", new Uri[] { });
            Assert.NotNull(result);
        }
    }
}

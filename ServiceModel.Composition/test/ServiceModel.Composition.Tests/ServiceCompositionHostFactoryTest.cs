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
        public void Test()
        {
            var catalog = new TypeCatalog(typeof(TestService));
            var container = new CompositionContainer(catalog);
            //container.ComposeExportedValue()
            CompositionBatch batch = new CompositionBatch();
            var foo = container.GetExports<TestService>();
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost("ServiceModel.Composition.TestService", new Uri[] { });
            Assert.NotNull(result);
        }
    }
}

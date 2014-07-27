using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceModel.Composition.Tests
{
    public class ExportServiceBehaviorAttributeTest
    {

        [Fact]
        public void ExportServiceBehavior_ForService()
        {
            var catalog = new TypeCatalog(typeof(FooService), typeof(ServiceBehaviorForAll), typeof(ServiceBehaviorForService));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(FooService).FullName, new Uri[] { });
            result.AddServiceEndpoint(
                typeof(IFooContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportServiceBehavior_ForService");


            result.Open();
            Assert.True(result.Description.Behaviors.OfType<ServiceBehaviorForAll>().Any());
            Assert.True(result.Description.Behaviors.OfType<ServiceBehaviorForService>().Any());
        }

        [Fact]
        public void ExportServiceBehavior_NotForService()
        {
            var catalog = new TypeCatalog(typeof(BarService), typeof(ServiceBehaviorForAll), typeof(ServiceBehaviorForService));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(BarService).FullName, new Uri[] { });
            result.AddServiceEndpoint(
                typeof(IBarContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportServiceBehavior_NotForService");
            
            result.Open();
            
            Assert.True(result.Description.Behaviors.OfType<ServiceBehaviorForAll>().Any());
            Assert.False(result.Description.Behaviors.OfType<ServiceBehaviorForService>().Any());
        }
    }
}

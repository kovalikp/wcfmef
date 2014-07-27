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
    public class ExportEndpointBehaviorAttributeTest
    {
        [Fact]
        public void ExportEndpointBehavior_ForAll()
        {
            var catalog = new TypeCatalog(typeof(FooService), typeof(EndpointBehaviorForAll), typeof(EndpointBehaviorForService));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(FooService).FullName, new Uri[] { });
            var endpoint = result.AddServiceEndpoint(
                typeof(IFooContract).FullName, 
                new BasicHttpBinding(), 
                "http://localhost:12345/ExportEndpointBehavior_ForAll");
            
            result.Open();
            
            AssertPlus.AnyOfType<EndpointBehaviorForAll>(endpoint.EndpointBehaviors);
            AssertPlus.AnyOfType<EndpointBehaviorForService>(endpoint.EndpointBehaviors);

        }

        [Fact]
        public void ExportEndpointBehavior_NotForService()
        {
            var catalog = new TypeCatalog(typeof(BarService), typeof(EndpointBehaviorForAll), typeof(EndpointBehaviorForService));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(BarService).FullName, new Uri[] { });
            var endpoint = result.AddServiceEndpoint(
                typeof(IBarContract).FullName, 
                new BasicHttpBinding(), 
                "http://localhost:12345/ExportEndpointBehavior_NotForService");
            
            result.Open();

            AssertPlus.AnyOfType<EndpointBehaviorForAll>(endpoint.EndpointBehaviors);
            AssertPlus.NoneOfType<EndpointBehaviorForService>(endpoint.EndpointBehaviors);
        }

        [Fact]
        public void ExportEndpointBehavior_ForIFooContract()
        {
            var catalog = new TypeCatalog(typeof(FooBarService), typeof(EndpointBehaviorForIFooContract));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(FooBarService).FullName, new Uri[] { });
            var fooContractEndpoint = result.AddServiceEndpoint(
                typeof(IFooContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportEndpointBehavior_ForIFooContract_1");
            var barContractEndpoint = result.AddServiceEndpoint(
                typeof(IBarContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportEndpointBehavior_ForIFooContract_2");
            result.Open();

            AssertPlus.AnyOfType<EndpointBehaviorForIFooContract>(fooContractEndpoint.EndpointBehaviors);
            AssertPlus.NoneOfType<EndpointBehaviorForIFooContract>(barContractEndpoint.EndpointBehaviors);
        }

        [Fact]
        public void ExportEndpointBehavior_ForBasicHttpBinding()
        {
            var catalog = new TypeCatalog(typeof(FooBarService), typeof(EndpointBehaviorForBasicHttpBinding));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(FooBarService).FullName, new Uri[] { });
            var basicHttpBindingEndpoint = result.AddServiceEndpoint(
                typeof(IFooContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportEndpointBehavior_ForBasicHttpBinding_1");
            var wsHttpBindingEndpoint = result.AddServiceEndpoint(
                typeof(IBarContract).FullName,
                new WSHttpBinding(),
                "http://localhost:12345/ExportEndpointBehavior_ForBasicHttpBinding_2");
            result.Open();

            AssertPlus.AnyOfType<EndpointBehaviorForBasicHttpBinding>(basicHttpBindingEndpoint.EndpointBehaviors);
            AssertPlus.NoneOfType<EndpointBehaviorForBasicHttpBinding>(wsHttpBindingEndpoint.EndpointBehaviors);
        }

        [Fact]
        public void ExportEndpointBehavior_ForNamedEnpoint()
        {
            var catalog = new TypeCatalog(typeof(FooBarService), typeof(EndpointBehaviorForNamedEndpoint));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(FooBarService).FullName, new Uri[] { });
            var namedEndpoint = result.AddServiceEndpoint(
                typeof(IFooContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportEndpointBehavior_ForNamedEnpoint_1");
            namedEndpoint.Name = EndpointBehaviorForNamedEndpoint.EndpointName;

            var defaultNamedEndpoint = result.AddServiceEndpoint(
                typeof(IBarContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportEndpointBehavior_ForNamedEnpoint_2");
            result.Open();

            AssertPlus.AnyOfType<EndpointBehaviorForNamedEndpoint>(namedEndpoint.EndpointBehaviors);
            AssertPlus.NoneOfType<EndpointBehaviorForNamedEndpoint>(defaultNamedEndpoint.EndpointBehaviors);
        }
    }
}

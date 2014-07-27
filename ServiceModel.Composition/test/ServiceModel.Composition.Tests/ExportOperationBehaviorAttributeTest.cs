using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceModel.Composition.Tests
{
    
    public class ExportOperationBehaviorAttributeTest
    {
        [Fact]
        public void ExportOperationBehavior_ForFooContract()
        {
            var catalog = new TypeCatalog(typeof(FooService), typeof(OperationBehaviorForAll), typeof(OperationBehaviorForBarContract));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(FooService).FullName, new Uri[] { });
            var endpoint = result.AddServiceEndpoint(
                typeof(IFooContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportOperationBehavior_ForFooContract");


            result.Open();
            AssertPlus.AnyOfType<OperationBehaviorForAll>(endpoint.Contract.Operations[0].OperationBehaviors);
            AssertPlus.NoneOfType<OperationBehaviorForBarContract>(endpoint.Contract.Operations[0].OperationBehaviors);
        }

        [Fact]
        public void ExportOperationBehavior_ForBarContract()
        {
            var catalog = new TypeCatalog(typeof(BarService), typeof(OperationBehaviorForAll), typeof(OperationBehaviorForBarContract));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(BarService).FullName, new Uri[] { });
            var endpoint = result.AddServiceEndpoint(
                typeof(IBarContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportOperationBehavior_ForBarContract");

            result.Open();

            AssertPlus.AnyOfType<OperationBehaviorForAll>(endpoint.Contract.Operations[0].OperationBehaviors);
            AssertPlus.AnyOfType<OperationBehaviorForBarContract>(endpoint.Contract.Operations[0].OperationBehaviors);
            AssertPlus.AnyOfType<OperationBehaviorForAll>(endpoint.Contract.Operations[1].OperationBehaviors);
            AssertPlus.AnyOfType<OperationBehaviorForBarContract>(endpoint.Contract.Operations[1].OperationBehaviors);
        }

        [Fact]
        public void ExportOperationBehavior_ForBazOperation()
        {
            var catalog = new TypeCatalog(typeof(BarService), typeof(OperationBehaviorForBazOperation));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(BarService).FullName, new Uri[] { });
            var endpoint = result.AddServiceEndpoint(
                typeof(IBarContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportOperationBehavior_ForBazOperation");

            result.Open();

            var barOperationDescription = endpoint.Contract.Operations.First(x => x.Name == "Bar");
            var bazOperationDescription = endpoint.Contract.Operations.First(x => x.Name == "Baz");

            AssertPlus.NoneOfType<OperationBehaviorForBazOperation>(barOperationDescription.OperationBehaviors);
            AssertPlus.AnyOfType<OperationBehaviorForBazOperation>(bazOperationDescription.OperationBehaviors);
        }
    }
}

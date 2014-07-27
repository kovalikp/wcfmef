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
    public class ExportContractBehaviorAttributeTest
    {
        [Fact]
        public void ExportContractBehavior_ForFooContract()
        {
            var catalog = new TypeCatalog(typeof(FooService), typeof(ContractBehaviorForAll), typeof(ContractBehaviorForFooContract));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(FooService).FullName, new Uri[] { });
            var endpoint = result.AddServiceEndpoint(
                typeof(IFooContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportContractBehavior_ForFooContract");


            result.Open();
            AssertPlus.AnyOfType<ContractBehaviorForAll>(endpoint.Contract.ContractBehaviors);
            AssertPlus.AnyOfType<ContractBehaviorForFooContract>(endpoint.Contract.ContractBehaviors);
        }

        [Fact]
        public void ExportServiceBehavior_ForBarContract()
        {
            var catalog = new TypeCatalog(typeof(BarService), typeof(ContractBehaviorForAll), typeof(ContractBehaviorForFooContract));
            var container = new CompositionContainer(catalog);
            var factory = new ServiceCompositionHostFactory(container);
            var result = factory.CreateServiceHost(typeof(BarService).FullName, new Uri[] { });
            var endpoint = result.AddServiceEndpoint(
                typeof(IBarContract).FullName,
                new BasicHttpBinding(),
                "http://localhost:12345/ExportServiceBehavior_ForBarContract");

            result.Open();

            AssertPlus.AnyOfType<ContractBehaviorForAll>(endpoint.Contract.ContractBehaviors);
            AssertPlus.NoneOfType<ContractBehaviorForFooContract>(endpoint.Contract.ContractBehaviors);
        }
    }
}

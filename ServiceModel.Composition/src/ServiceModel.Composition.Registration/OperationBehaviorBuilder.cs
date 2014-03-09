//#if NET45
//namespace ServiceModel.Composition.Registration
//{
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel.Composition.Registration;
//    using System.Linq;
//    using System.Text;
//    using System.Threading.Tasks;

//    /// <summary>
//    /// Metadata information for exported operation behavior.
//    /// </summary>
//    public sealed class OperationBehaviorBuilder
//    {
//        private readonly ExportBuilder _exportBuilder;

//        internal EndpointBehaviorBuilder(ExportBuilder exportBuilder)
//        {
//            _exportBuilder = exportBuilder;
//        }

//        /// <summary>
//        /// Adds the endpoint names metadata.
//        /// </summary>
//        /// <param name="endpointNames">The endpoint names.</param>
//        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
//        public EndpointBehaviorBuilder AddEndpointNames(string[] endpointNames)
//        {
//            _exportBuilder.AddMetadata("EndpointNames", endpointNames);
//            return this;
//        }

//        /// <summary>
//        /// Adds the binding names metadata.
//        /// </summary>
//        /// <param name="bindingNames">The binding names.</param>
//        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
//        public EndpointBehaviorBuilder AddBindingNames(string[] bindingNames)
//        {
//            _exportBuilder.AddMetadata("BindingNames", bindingNames);
//            return this;
//        }

//        /// <summary>
//        /// Adds the binding types metadata.
//        /// </summary>
//        /// <param name="bindingTypes">The binding types.</param>
//        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
//        public EndpointBehaviorBuilder AddBindingTypes(Type[] bindingTypes)
//        {
//            _exportBuilder.AddMetadata("BindingTypes", bindingTypes);
//            return this;
//        }

//        /// <summary>
//        /// Adds the contract names metadata.
//        /// </summary>
//        /// <param name="contractNames">The contract names.</param>
//        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
//        public EndpointBehaviorBuilder AddContractNames(string[] contractNames)
//        {
//            _exportBuilder.AddMetadata("ContractNames", contractNames);
//            return this;
//        }

//        /// <summary>
//        /// Adds the contract types metadata.
//        /// </summary>
//        /// <param name="contractTypes">The contract types.</param>
//        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
//        public EndpointBehaviorBuilder AddContractTypes(Type[] contractTypes)
//        {
//            _exportBuilder.AddMetadata("ContractTypes", contractTypes);
//            return this;
//        }
//    }
//}
//#endif
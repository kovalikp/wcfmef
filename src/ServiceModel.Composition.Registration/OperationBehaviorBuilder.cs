namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Registration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Metadata information for exported operation behavior.
    /// </summary>
    public sealed class OperationBehaviorBuilder
    {
        private readonly ExportBuilder _exportBuilder;

        internal OperationBehaviorBuilder(ExportBuilder exportBuilder)
        {
            _exportBuilder = exportBuilder;
        }

        /// <summary>
        /// Adds the endpoint names metadata.
        /// </summary>
        /// <param name="endpointNames">The endpoint names.</param>
        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
        public OperationBehaviorBuilder AddEndpointNames(string[] endpointNames)
        {
            _exportBuilder.AddMetadata("EndpointNames", endpointNames);
            return this;
        }

        /// <summary>
        /// Adds the binding names metadata.
        /// </summary>
        /// <param name="bindingNames">The binding names.</param>
        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
        public OperationBehaviorBuilder AddBindingNames(string[] bindingNames)
        {
            _exportBuilder.AddMetadata("BindingNames", bindingNames);
            return this;
        }

        /// <summary>
        /// Adds the binding types metadata.
        /// </summary>
        /// <param name="bindingTypes">The binding types.</param>
        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
        public OperationBehaviorBuilder AddBindingTypes(Type[] bindingTypes)
        {
            _exportBuilder.AddMetadata("BindingTypes", bindingTypes);
            return this;
        }

        /// <summary>
        /// Adds the contract names metadata.
        /// </summary>
        /// <param name="contractNames">The contract names.</param>
        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
        public OperationBehaviorBuilder AddContractNames(string[] contractNames)
        {
            _exportBuilder.AddMetadata("ContractNames", contractNames);
            return this;
        }

        /// <summary>
        /// Adds the contract types metadata.
        /// </summary>
        /// <param name="contractTypes">The contract types.</param>
        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
        public OperationBehaviorBuilder AddContractTypes(Type[] contractTypes)
        {
            _exportBuilder.AddMetadata("ContractTypes", contractTypes);
            return this;
        }
    }
}

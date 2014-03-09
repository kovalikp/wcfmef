namespace ServiceModel.Composition.Registration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Metadata information for exported operation behavior.
    /// </summary>
    public sealed class EndpointBehaviorBuilder
    {
        public EndpointBehaviorBuilder()
        {
        }

        public string[] EndpointNames { get; set; }

        public string[] BindingNames { get; set; }

        public Type[] BindingTypes { get; set; }

        public string[] ContractNames { get; set; }

        public Type[] ContractTypes { get; set; }

        public EndpointBehaviorBuilder AddEndpointNames(string[] endpointNames)
        {
            EndpointNames= endpointNames;
            return this;
        }

        /// <summary>
        /// Adds the binding names metadata.
        /// </summary>
        /// <param name="bindingNames">The binding names.</param>
        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
        public EndpointBehaviorBuilder AddBindingNames(string[] bindingNames)
        {
            BindingNames = bindingNames;
            return this;
        }

        /// <summary>
        /// Adds the binding types metadata.
        /// </summary>
        /// <param name="bindingTypes">The binding types.</param>
        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
        public EndpointBehaviorBuilder AddBindingTypes(Type[] bindingTypes)
        {
            BindingTypes = bindingTypes;
            return this;
        }

        /// <summary>
        /// Adds the contract names metadata.
        /// </summary>
        /// <param name="contractNames">The contract names.</param>
        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
        public EndpointBehaviorBuilder AddContractNames(string[] contractNames)
        {
            ContractNames = contractNames;
            return this;
        }

        /// <summary>
        /// Adds the contract types metadata.
        /// </summary>
        /// <param name="contractTypes">The contract types.</param>
        /// <returns>The same OperationBehaviorBuilder instance so that multiple calls can be chained.</returns>
        public EndpointBehaviorBuilder AddContractTypes(Type[] contractTypes)
        {
            ContractTypes = contractTypes;
            return this;
        }
    }
}

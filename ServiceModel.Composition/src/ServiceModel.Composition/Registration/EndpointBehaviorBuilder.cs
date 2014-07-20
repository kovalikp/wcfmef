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
        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointBehaviorBuilder"/> class.
        /// </summary>
        public EndpointBehaviorBuilder()
        {
        }

        /// <summary>
        /// Gets or sets the endpoint names.
        /// </summary>
        /// <value>
        /// The endpoint names.
        /// </value>
        public string[] EndpointNames { get; set; }

        /// <summary>
        /// Gets or sets the binding names.
        /// </summary>
        /// <value>
        /// The binding names.
        /// </value>
        public string[] BindingNames { get; set; }

        /// <summary>
        /// Gets or sets the binding types.
        /// </summary>
        /// <value>
        /// The binding types.
        /// </value>
        public Type[] BindingTypes { get; set; }

        /// <summary>
        /// Gets or sets the contract names.
        /// </summary>
        /// <value>
        /// The contract names.
        /// </value>
        public string[] ContractNames { get; set; }

        /// <summary>
        /// Gets or sets the contract types.
        /// </summary>
        /// <value>
        /// The contract types.
        /// </value>
        public Type[] ContractTypes { get; set; }

        /// <summary>
        /// Adds the endpoint names.
        /// </summary>
        /// <param name="endpointNames">The endpoint names.</param>
        /// <returns></returns>
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

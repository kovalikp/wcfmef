#if NET45

namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading.Tasks;

    /// <content>
    /// Task based asynchronous methods.
    /// </content>
    public partial class SelfHostingContainer
    {
        /// <summary>
        /// Gets the service hosts.
        /// </summary>
        /// <value>
        /// The service hosts.
        /// </value>
        public IReadOnlyCollection<ServiceCompositionHost> ServiceHosts
        {
            get
            {
                if (_serviceHosts == null)
                {
                    Initialize();
                }

                return _serviceHosts;
            }
        }

        /// <summary>
        /// Opens the asynchronous.
        /// </summary>
        /// <returns>Return <see cref="Task"/>.</returns>
        public async Task OpenAsync()
        {
            var serviceHosts = _serviceHosts.Where(x => x.State == CommunicationState.Created);
            var tasks = new List<Task>();
            foreach (var serviceHost in OpenableServiceHosts)
            {
                var task = Task.Factory.FromAsync(serviceHost.BeginOpen, serviceHost.EndOpen, new { });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Opens the asynchronous.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>Return <see cref="Task"/>.</returns>
        public async Task OpenAsync(TimeSpan timeout)
        {
            var tasks = new List<Task>();
            foreach (var serviceHost in _serviceHosts)
            {
                var task = Task.Factory.FromAsync(serviceHost.BeginOpen, serviceHost.EndOpen, timeout, new { });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Closes the asynchronous.
        /// </summary>
        /// <returns>Return <see cref="Task"/>.</returns>
        public async Task CloseAsync()
        {
            var tasks = new List<Task>();
            foreach (var serviceHost in ClosableServiceHosts)
            {
                var task = Task.Factory.FromAsync(serviceHost.BeginClose, serviceHost.EndClose, new { });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Closes the asynchronous.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns>Return <see cref="Task"/>.</returns>
        public async Task CloseAsync(TimeSpan timeout)
        {
            var tasks = new List<Task>();
            foreach (var serviceHost in OpenableServiceHosts)
            {
                var task = Task.Factory.FromAsync(serviceHost.BeginClose, serviceHost.EndClose, timeout, new { });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }
    }
}

#endif
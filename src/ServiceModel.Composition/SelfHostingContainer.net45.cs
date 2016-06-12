#if NET45

namespace ServiceModel.Composition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading;
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
        public IReadOnlyCollection<ServiceHost> ServiceHosts
        {
            get
            {
                Initialize();
                return _serviceHosts;
            }
        }

        /// <summary>
        /// Causes service hosts to open asynchronously.
        /// </summary>
        /// <returns>Return <see cref="Task"/>.</returns>
        public async Task OpenAsync()
        {
            await OpenAsync(CancellationToken.None);
        }

        /// <summary>
        /// Causes service hosts to open asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Return <see cref="Task"/>.</returns>
        public async Task OpenAsync(CancellationToken cancellationToken)
        {
            await FromAsync(serviceHost => serviceHost.BeginOpen, serviceHost => serviceHost.EndOpen, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Causes service hosts to close asynchronously.
        /// </summary>
        /// <returns>Return <see cref="Task"/>.</returns>
        public async Task CloseAsync()
        {
            await CloseAsync(CancellationToken.None);
        }

        /// <summary>
        /// Causes service hosts to close asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Return <see cref="Task"/>.</returns>
        public async Task CloseAsync(CancellationToken cancellationToken)
        {
            await FromAsync(serviceHost => serviceHost.BeginClose, serviceHost => serviceHost.EndClose, cancellationToken).ConfigureAwait(false);
        }

        private async Task FromAsync(
            Func<ServiceHost, Func<AsyncCallback, object, IAsyncResult>> beginMethod,
            Func<ServiceHost, Action<IAsyncResult>> endMethod,
            CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            var countdown = new CountdownEvent(ServiceHosts.Count);
            var countdownTask = Task.Factory.StartNew(() => countdown.Wait(cancellationToken), cancellationToken);

            foreach (var serviceHost in ServiceHosts)
            {
                var asyncTask = Task.Factory.FromAsync(beginMethod(serviceHost), endMethod(serviceHost), new { });

                var continuation = asyncTask.ContinueWith(
                    antecedent =>
                    {
                        // signal async operation completed
                        countdown.Signal();

                        // treat CommunicationObjectAbortedException as OperationCancelledException, if operation was cancelled
                        if (antecedent.Exception != null)
                        {
                            if (antecedent.Exception.InnerExceptions.Count == 1 &&
                                antecedent.Exception.InnerExceptions[0] is CommunicationObjectAbortedException)
                            {
                                cancellationToken.ThrowIfCancellationRequested();
                            }

                            throw antecedent.Exception;
                        }
                    },
                    TaskContinuationOptions.ExecuteSynchronously);
                tasks.Add(continuation);
            }

            var abortTask = countdownTask.ContinueWith(
                antecedent => this.Abort(),
                TaskContinuationOptions.OnlyOnCanceled);

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}

#endif
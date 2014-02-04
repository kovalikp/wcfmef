namespace Service
{
    using ServiceModel.Composition;
    using System.ComponentModel.Composition;
    using System.ServiceModel;

    [ExportService(UsePerServiceInstancing = true)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SampleService : ISampleService
    {
        private ILogger _logger;

        private static int _serviceCounter = 0;

        private int _serviceId;

        public SampleService()
        {
            _serviceCounter++;
            _serviceId = _serviceCounter;
        }

        [ImportingConstructor]
        public SampleService(ILogger logger)
            : this()
        {
            _logger = logger;
        }

        public PingResponse Ping(PingRequest pingRequest)
        {
            var part = new object();
            var compositionExtension = OperationContext.Current.Extensions.Find<CompositionInstanceContextExtension>();
            compositionExtension.SatisfyImportsOnce(part);
            
            return new PingResponse
            {
                ClientId = pingRequest.ClientId,
                ServiceId = _serviceId,
                ManagedThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId
            };
        }
    }
}
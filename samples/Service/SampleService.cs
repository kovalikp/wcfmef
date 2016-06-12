namespace Service
{
    using ServiceModel.Composition;
    using System.ComponentModel.Composition;
    using System.ServiceModel;

    [ExportService()]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SampleService : ISampleService
    {
        private ILogger _logger;

        private static int _serviceCounter = 0;

        private int _serviceId;

        private int _pingCounter;

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
            _pingCounter++;
            return new PingResponse
            {
                ClientId = pingRequest.ClientId,
                ServiceId = _serviceId,
                ManagedThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId,
                Count = _pingCounter
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ServiceModel.Composition;
using System.ComponentModel.Composition;

namespace Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ExportService]
    //[ExportContract(typeof(ISampleService))]
    //[ExportContract()]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SampleService : ISampleService
    {
        ILogger _logger;

        static int _serviceCounter = 0;

        int _serviceId;

        public SampleService()
        {
            _serviceCounter++;
            _serviceId = _serviceCounter;
        }

        [ImportingConstructor]
        public SampleService(ILogger logger) : this()
        {
            _logger = logger;
        }


        public PingResponse Ping(PingRequest pingRequest)
        {
            return new PingResponse
            {
                ClientId = pingRequest.ClientId,
                ServiceId = _serviceId,
                ManagedThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId
            };
        }
    }
}

namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;

    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ISampleService
    {
        [OperationContract]
        PingResponse Ping(PingRequest pingRequest);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class PingRequest
    {
        [DataMember(Order = 0)]
        public int ClientId { get; set; }
    }

    [DataContract]
    public class PingResponse
    {
        [DataMember(Order = 0)]
        public int ClientId { get; set; }

        [DataMember(Order = 1)]
        public int ServiceId { get; set; }

        [DataMember(Order = 2)]
        public int ManagedThreadId { get; set; }

        public override string ToString()
        {
            return String.Format("Client {0}, Service {1}, Managed thread {2}", ClientId, ServiceId, ManagedThreadId);
        }
    }
}

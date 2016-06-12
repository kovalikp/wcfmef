namespace Client.Sample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class PingResponse
    {
        public override string ToString()
        {
            return $"Client {ClientId}, Service {ServiceId}, Managed thread {ManagedThreadId}, Count {Count}";
        }
    }
}

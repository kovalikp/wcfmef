using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Sample
{
    public partial class PingResponse
    {
        public override string ToString()
        {
            return String.Format("Client {0}, Service {1}, Managed thread {2}", ClientId, ServiceId, ManagedThreadId);
        }
    }
}

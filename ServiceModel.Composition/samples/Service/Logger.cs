using ServiceModel.Composition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ILogger
    {
        void Log(string message);
        
        void Log(string format, params object[] args);
    }
    
    //[PerServiceInstance]
    [Export(typeof(ILogger))]
    public class DebugLogger : ILogger
    {
        Guid _id = Guid.NewGuid();

        public void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Log(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(format, args);
        }
    }
}

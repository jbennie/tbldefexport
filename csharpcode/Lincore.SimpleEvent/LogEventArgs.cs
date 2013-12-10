using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincore.SimpleEvent
{
    public class LogEventArgs : EventArgs
    {
        public object[] logs;

        public LogEventArgs(object[] l)
            : base()
        {
            logs = l; 
        }

    }
}
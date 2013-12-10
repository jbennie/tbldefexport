using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandApp
{
    public class WorkArgs
    {
        public WorkArgs() 
        {
            KeyPairArgs = new Dictionary<string, object>(); 
        }
        public Dictionary<string, object> KeyPairArgs;  
    }
}

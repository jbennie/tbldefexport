using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LandSync
{
    interface ISyncTarget
    {
        Lincore.DataTools.IDatabaseConfig WriteChangesTo();        
    }
}

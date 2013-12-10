using System;
using System.Collections.Generic;
using System.Text;
using Lincore.DataTools.DBHandler; 

namespace Lincore.DataTools
{
    public interface IDatabaseConfig
    {
        string GetConnetionString(); 
        IDBHandler GetHandler();
    }

}

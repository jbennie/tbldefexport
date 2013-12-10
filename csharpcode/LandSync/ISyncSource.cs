using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Land.LegacyDb;

namespace LandSync
{
    public interface ISyncSource
    {
        Lincore.DataTools.IDatabaseConfig ReadChangesFrom(); 
    }
}

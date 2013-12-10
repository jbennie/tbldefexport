using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandSync
{
    public class LegacyDatabaseDef: ISyncSource
    {

        private Lincore.DataTools.DBConfig.OLEDB.DatabaseConfig DBConfig; 
                
        public LegacyDatabaseDef(string path) 
        {
            DBConfig = new Lincore.DataTools.DBConfig.OLEDB.DatabaseConfig(path);             
        }
         

        public Lincore.DataTools.IDatabaseConfig ReadChangesFrom()
        {
            return DBConfig; 
        }
    }
}

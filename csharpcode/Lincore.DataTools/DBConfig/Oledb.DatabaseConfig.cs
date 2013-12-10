using System;
using System.Collections.Generic;
using System.Text;
using Lincore.DataTools; 
using Lincore.DataTools.DBHandler;

namespace Lincore.DataTools.DBConfig.OLEDB
{
    public class DatabaseConfig : IDatabaseConfig
    {

        public DatabaseConfig(string path) 
        {
            OLEDB_Path = path; 
        }

        private DBHandler.OLEDB.DBHandler _dbHandle;

        private string _OLEDB_Path = @"c:\database.mbd";
        
        public string OLEDB_Path
        {
            get
            {
                return _OLEDB_Path;
            }
            set
            {
                if (System.IO.File.Exists(value))
                {
                    _OLEDB_Path = value;
                }
                else 
                {
                    throw new Exception("Invalid Jet 4 mdb path, path does not exist: " + value); 
                }
            }
        }

        public string GetConnetionString()
        {
            // Server = $Serveraddress; Port=3306; Database = $dbname ; Uid=$user ; Pwd=$pass
            // Encrypt = true; | Encryption = true; 
       //     return "Server=" + DBSERVER_HOST + "; Port=" + DBSERVER_Port + "; Database=" + DBSERVER_DB + "; Uid=" + DBSERVER_UID + "; Pwd=" + DBSERVER_Pwd + ";";
            return @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + OLEDB_Path + ";";       
        }
      
        /// <summary>
        /// all Subsequent calls
        /// </summary>
        /// <returns></returns>
        public IDBHandler GetHandler()
        {
          //  if (Lincore.DataTools.DBHandler.OLEDB.DBHandler.DatabaseConfig == null)
          //      Lincore.DataTools.DBHandler.OLEDB.DBHandler.DatabaseConfig = this;

            if (_dbHandle == null)
            {
                _dbHandle = Lincore.DataTools.DBHandler.OLEDB.DBHandler.GetHandler(this);
            }

            return _dbHandle; 
        }
    }
}

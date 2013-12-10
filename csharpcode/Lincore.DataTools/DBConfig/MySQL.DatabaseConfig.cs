using System;
using System.Collections.Generic;
using System.Text;
using Lincore.DataTools;
using Lincore.DataTools.DBHandler;

namespace Lincore.DataTools.DBConfig.MySQL
{

    public class DatabaseConfig : IDatabaseConfig
    {

        private DBHandler.MYSQL.DBHandler _dbHandle;

        public DatabaseConfig(string host, string db, string user, string pwd, string port) 
        {
            DBSERVER_DB = db;
            DBSERVER_HOST = host;
            DBSERVER_Port = port;
            DBSERVER_Pwd = pwd;
            DBSERVER_UID = user;
        }
        
        private string DBSERVER_HOST = "127.0.0.1";
        private string DBSERVER_DB = "databasename";
        private string DBSERVER_UID = "admin";
        private string DBSERVER_Pwd = "password";
        private string DBSERVER_Port = "3306";

        public string GetConnetionString()
        {
            // Server = $Serveraddress; Port=3306; Database = $dbname ; Uid=$user ; Pwd=$pass
            // Encrypt = true; | Encryption = true; 
            return "Server=" + DBSERVER_HOST + "; Port=" + DBSERVER_Port + "; Database=" + DBSERVER_DB + "; Uid=" + DBSERVER_UID + "; Pwd=" + DBSERVER_Pwd + ";";
        }

        public IDBHandler GetHandler()
        {

          //  if (Lincore.DataTools.DBHandler.MYSQL.DBHandler.DatabaseConfig == null)
          //      Lincore.DataTools.DBHandler.MYSQL.DBHandler.DatabaseConfig = this;
        
            if (_dbHandle == null)
            {
                _dbHandle = Lincore.DataTools.DBHandler.MYSQL.DBHandler.GetHandler(this);
            }
            return _dbHandle;
        }
    }
}

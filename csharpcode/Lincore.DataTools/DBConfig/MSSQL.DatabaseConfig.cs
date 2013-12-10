using System;
using Lincore.DataTools;
using Lincore.DataTools.DBHandler;

namespace Lincore.DataTools.DBConfig.MSSQL
{

    public class DatabaseConfig : IDatabaseConfig
    {

        private Lincore.DataTools.DBHandler.MSSQL.DBHandler _dbHandle;

        public DatabaseConfig(string host, string db, string user, string pwd) 
        {
            DBSERVER_DB = db;
            DBSERVER_HOST = host;
        //    DBSERVER_Port = port;
            DBSERVER_Pwd = pwd;
            DBSERVER_UID = user;
        }
        
        private string DBSERVER_HOST = "127.0.0.1";
        private string DBSERVER_DB = "test";
        private static string DBSERVER_UID = "admin";
        private static string DBSERVER_Pwd = "password";
        private string PacketSize = "4028"; 
       // private static string DBSERVER_Port = "3306";

        public string GetConnetionString()
        {
            // Server = $Serveraddress; Port=3306; Database = $dbname ; Uid=$user ; Pwd=$pass
            // Encrypt = true; | Encryption = true; 
       //     return "Server=" + DBSERVER_HOST + "; Port=" + DBSERVER_Port + "; Database=" + DBSERVER_DB + "; Uid=" + DBSERVER_UID + "; Pwd=" + DBSERVER_Pwd + ";";
            return @"data source=" + DBSERVER_HOST + ";initial catalog=" + DBSERVER_DB + ";integrated security=SSPI;Packet Size=" + PacketSize;

            //compmgrdev.acml.com,15026
        }

        public IDBHandler GetHandler()
        {
          //  if (Lincore.DataTools.DBHandler.MSSQL.DBHandler.DatabaseConfig == null)
          //      Lincore.DataTools.DBHandler.MSSQL.DBHandler.DatabaseConfig = this;

            if (_dbHandle == null)
            {
                _dbHandle = Lincore.DataTools.DBHandler.MSSQL.DBHandler.GetHandler(this);
            }
            return _dbHandle;
        }
    }
}
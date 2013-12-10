using System;
using System.Data; 
using System.Data.Common; 
using MySql; 
using MySql.Data; 
using MySql.Data.MySqlClient;
using MySql.Data.Types; 

namespace Lincore.DataTools.DBHandler.MYSQL
{
    public class DBHandler: IDBHandler
    {
        public DBConfig.MySQL.DatabaseConfig DatabaseConfig;


        private static DBHandler _self;
        public static DBHandler GetHandler(DBConfig.MySQL.DatabaseConfig DBconfig)
        {
            if (_self == null)
            {
                _self = new DBHandler(DBconfig);
            }
            return _self;
        }
   
        private DBHandler(DBConfig.MySQL.DatabaseConfig DBconfig)
        {
            DatabaseConfig = DBconfig; 
        }

        #region IDBHandler Members

        public System.Data.IDbCommand getDataCommand(string cmdtext)
        {
            return new MySql.Data.MySqlClient.MySqlCommand(cmdtext);
        }

        public System.Data.IDbCommand getDataCommand(string cmdtext, IDbConnection aCon)
        {
            return new MySql.Data.MySqlClient.MySqlCommand(cmdtext, (MySqlConnection)aCon);
        }

        public System.Data.IDbCommand getDataCommand(string cmdtext, IDbConnection aCon, IDbTransaction aTrn)
        {
            return new MySql.Data.MySqlClient.MySqlCommand(cmdtext, (MySqlConnection)aCon, (MySqlTransaction)aTrn); 
        }

        public IDbDataAdapter getDataAdapter(string cmdtext, IDbConnection aCon)
        {
            return new MySql.Data.MySqlClient.MySqlDataAdapter(cmdtext, (MySqlConnection)aCon);
        }

        public IDbDataAdapter getDataAdapter(IDbCommand aSelectCommand)
        {
            return new MySql.Data.MySqlClient.MySqlDataAdapter((MySqlCommand)aSelectCommand); 
        }

        public System.Data.IDbConnection getDataConnection()
        {
            return new MySql.Data.MySqlClient.MySqlConnection(DatabaseConfig.GetConnetionString());           
        }

        public IDataParameter getDataParameter() 
        {
            return new MySql.Data.MySqlClient.MySqlParameter(); 
        }

        public IDataParameter getDataParameter(string aParameterName, object aObj)
        {
            return new MySql.Data.MySqlClient.MySqlParameter(aParameterName, aObj); 
        }


        public char getFieldBeginDelimiter() { return '`'; }
        public char getFieldEndDelimiter() { return '`'; }

        #endregion
    }
}

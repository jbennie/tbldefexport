using System;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes; 

namespace Lincore.DataTools.DBHandler.MSSQL
{
    public class DBHandler : IDBHandler
    {

        private DBConfig.MSSQL.DatabaseConfig DatabaseConfig;

        private static DBHandler _self;
        public static DBHandler GetHandler(DBConfig.MSSQL.DatabaseConfig DBconfig)
        {
            if (_self == null)
            {
                _self = new DBHandler(DBconfig);
            }
            return _self;
        }
  
        private DBHandler(DBConfig.MSSQL.DatabaseConfig DBconfig)
        {
            DatabaseConfig = DBconfig; 
        }

        #region IDBHandler Members

        public System.Data.IDbCommand getDataCommand(string cmdtext)
        {
            return new SqlCommand(cmdtext);
        }

        public System.Data.IDbCommand getDataCommand(string cmdtext, IDbConnection aCon)
        {
            return new SqlCommand(cmdtext, (SqlConnection)aCon);
        }

        public System.Data.IDbCommand getDataCommand(string cmdtext, IDbConnection aCon, IDbTransaction aTrn)
        {
            return new SqlCommand(cmdtext, (SqlConnection)aCon, (SqlTransaction)aTrn);
        }

        public IDbDataAdapter getDataAdapter(string cmdtext, IDbConnection aCon)
        {
            return new SqlDataAdapter(cmdtext, (SqlConnection)aCon);
        }

        public IDbDataAdapter getDataAdapter(IDbCommand aSelectCommand)
        {
            return new SqlDataAdapter((SqlCommand)aSelectCommand);
        }

        public System.Data.IDbConnection getDataConnection()
        {
            return new SqlConnection(DatabaseConfig.GetConnetionString());
        }

        public IDataParameter getDataParameter()
        {
            return new SqlParameter();
        }

        public IDataParameter getDataParameter(string aParameterName, object aObj)
        {
            return new SqlParameter(aParameterName, aObj);
        }

        public char getFieldBeginDelimiter(){return '[';}
        public char getFieldEndDelimiter( ){return ']';} 

        #endregion
    }
}

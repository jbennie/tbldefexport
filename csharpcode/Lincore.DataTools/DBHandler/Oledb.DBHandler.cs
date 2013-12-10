using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace Lincore.DataTools.DBHandler.OLEDB
{
    public class DBHandler : IDBHandler
    {
        public DBConfig.OLEDB.DatabaseConfig DatabaseConfig;

        private static DBHandler _self;
        public static DBHandler GetHandler(DBConfig.OLEDB.DatabaseConfig DBconfig)
        {
            if (_self == null)
            {
                _self = new DBHandler(DBconfig);
            }
            return _self; 
        }

        private DBHandler(DBConfig.OLEDB.DatabaseConfig DBconfig)
        {
            DatabaseConfig = DBconfig; 
        }

        #region IDBHandler Members


        public System.Data.IDbCommand getDataCommand(string cmdtext)
        {
            return new OleDbCommand(cmdtext);
        }

        public System.Data.IDbCommand getDataCommand(string cmdtext, IDbConnection aCon)
        {
            return new OleDbCommand(cmdtext, (OleDbConnection)aCon);
        }

        public System.Data.IDbCommand getDataCommand(string cmdtext, IDbConnection aCon, IDbTransaction aTrn)
        {
            return new OleDbCommand(cmdtext, (OleDbConnection)aCon, (OleDbTransaction)aTrn);
        }

        public IDbDataAdapter getDataAdapter(string cmdtext, IDbConnection aCon)
        {
            return new OleDbDataAdapter(cmdtext, (OleDbConnection)aCon);
        }

        public IDbDataAdapter getDataAdapter(IDbCommand aSelectCommand)
        {
            return new OleDbDataAdapter((OleDbCommand)aSelectCommand);
        }

        public System.Data.IDbConnection getDataConnection()
        {
            return new OleDbConnection(DatabaseConfig.GetConnetionString());
        }

        public IDataParameter getDataParameter()
        {
            return new OleDbParameter();
        }

        public IDataParameter getDataParameter(string aParameterName, object aObj)
        {
            return new OleDbParameter(aParameterName, aObj);
        }

        public char getFieldBeginDelimiter() { return '['; }
        public char getFieldEndDelimiter() { return ']'; }

        #endregion
    }
}
using System;
using System.Data; 
using System.Data.Common; 

namespace Lincore.DataTools.DBHandler
{
    public interface IDBHandler
    {
        IDbCommand getDataCommand(string cmdtext);
        IDbCommand getDataCommand(string cmdtext, IDbConnection aCon);
        IDbCommand getDataCommand(string cmdtext, IDbConnection aCon, IDbTransaction aTrn);

        IDbDataAdapter getDataAdapter(string cmdtext, IDbConnection aCon);
        IDbDataAdapter getDataAdapter(IDbCommand aSelectCommand);
        IDbConnection getDataConnection();

        IDataParameter getDataParameter();
        IDataParameter getDataParameter(string aParameterName, object aObj);

        char getFieldBeginDelimiter();
        char getFieldEndDelimiter(); 
    }
}

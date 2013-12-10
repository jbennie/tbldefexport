using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common; 
using Lincore.DataTools;
using Lincore.DataTools.vtable_common;

namespace Land.LegacyDb.Tables
{
    public class VTable_DeveloperCountyRequirements : VTable_Base
    {
        public const string VTableName = "[Developer County Requirements]";  // DEVREQ

        public VTable_DeveloperCountyRequirements(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
        //   [Developer County Requirements]


            TBL_Fields.Add(new VTable_FieldDef("[Developer Id]", "System.Int32", "developerid", false, 1 )); 	
            TBL_Fields.Add(new VTable_FieldDef("[Developer County Required]", "System.String", "developercountyrequired", false, 2)); 

                    //reset col ID
            int col = 0;
            foreach (VTable_FieldDef d in TBL_Fields)
            {
                d.CSVPosition = col;
                col++;
                d.FName = d.FName.Replace("[", "").Replace("]", "");
            }
   
        }
/*
        public void Fill(Int32 BenchID)
        {
            IDbCommand cmd = _DatabaseConfig.GetHandler().getDataCommand("Select " + this.GetSQLFields() + " from " + this.Table.TableName + " where BD_BenchID = @BenchID and VC_State = 'Approved'", this._DBConn);
            cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@BenchID", Type.GetType("System.Int32")));
            ((IDbDataParameter)cmd.Parameters["@BenchID"]).Value = BenchID;
            this.Fill(cmd);
        }
 */ 

    }
}
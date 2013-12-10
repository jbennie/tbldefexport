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
    public class VTable_Agents : VTable_Base
    {
        public const string VTableName = "[Agents]";

        public VTable_Agents(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {          
            TBL_Fields.Add(new VTable_FieldDef("[Agent]", "System.String", "organisationname", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Agent Address 1]", "System.String", "addressline1", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Agent Address 2]", "System.String", "addressline2", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Agent Address 3]", "System.String", "addressline3", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Agent Town]", "System.String", "town", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Agent County]", "System.String", "county", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Agent Post Code]", "System.String", "postcode", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Agent Tel No]", "System.String", "workphone", false, 8));

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

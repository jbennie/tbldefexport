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
    public class VTable_Greensheet_AdditionalDetails : VTable_Base
    {
        public const string VTableName = "[Greensheet Additional Details]"; // GSMOREDETAIL

        public VTable_Greensheet_AdditionalDetails(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            TBL_Fields.Add(new VTable_FieldDef("[Greensheet ID]", "System.Int32", "greensheetid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Greensheet ID Old]", "System.Int32", "greensheetidold", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Site ID]", "System.Int32", "siteid", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Contact Name]", "System.String", "contactname", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Contact ID]", "System.Int32", "contactid", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Company]", "System.String", "company", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Additional Details Sent]", "System.String", "additionaldetailssent", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Date Sent]", "System.DateTime", "datesent", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Comments]", "System.String", "comments", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Additional Details Comments]", "System.String", "additionaldetailscomments", false, 10)); 	

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
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
    public class VTable_Greensheet_Data : VTable_Base
    {
        public const string VTableName = "[GreensheetData]"; // GSDATA

        public VTable_Greensheet_Data(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {

            TBL_Fields.Add(new VTable_FieldDef("[Import ID]", "System.Int32", "importid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Site ID]", "System.Int32", "siteid", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Name]", "System.String", "name", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Company]", "System.String", "company", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Item Sent]", "System.String", "itemsent", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Date Sent]", "System.DateTime", "datesent", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Interest Yes/No]", "System.Boolean", "interestyes_no", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Comments]", "System.String", "comments", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Additional Details Sent]", "System.String", "additionaldetailssent", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Add Detaisl Sent Date]", "System.DateTime", "adddetaislsentdate", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Additional Details comments]", "System.String", "additionaldetailscomments", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[Offer Amount]", "System.String", "offeramount", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[Offer Date]", "System.DateTime", "offerdate", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[Offer accepted]", "System.String", "offeraccepted", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[Acceptance Date]", "System.String", "acceptancedate", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[Comments Date]", "System.DateTime", "commentsdate", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[Viewing Date]", "System.String", "viewingdate", false, 17));
            TBL_Fields.Add(new VTable_FieldDef("[Follow Up]", "System.String", "followup", false, 18));
            TBL_Fields.Add(new VTable_FieldDef("[Follow up Date]", "System.DateTime", "followupdate", false, 19));

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
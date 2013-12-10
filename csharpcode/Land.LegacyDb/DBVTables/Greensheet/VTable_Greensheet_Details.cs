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
    public class VTable_Greensheet_Details : VTable_Base
    {
        public const string VTableName = "[Greensheet details]"; //GSDetails

        public VTable_Greensheet_Details(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            TBL_Fields.Add(new VTable_FieldDef("[Greensheet ID]", "System.Int32", "greensheetid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Greensheet ID Old]", "System.Int32", "greensheetidold", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Company Name]", "System.String", "companyname", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Site ID]", "System.Int32", "siteid", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Contact ID]", "System.Int32", "contactid", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Contact Name]", "System.String", "contactname", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Enclosure Items Sent]", "System.String", "enclosureitemssent", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Date Letter 1 Sent]", "System.DateTime", "dateletter1sent", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Letter 1 ID]", "System.Int32", "letter1id", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Date Letter 2 Sent]", "System.DateTime", "dateletter2sent", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Date Letter 3 Sent]", "System.DateTime", "dateletter3sent", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[Interest]", "System.String", "interest", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[Comments]", "System.String", "comments", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[Comments Date]", "System.DateTime", "commentsdate", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[Offer]", "System.Double", "offer", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[Date of Offer]", "System.DateTime", "dateofoffer", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[Offer Accepted]", "System.Boolean", "offeraccepted", false, 17));
            TBL_Fields.Add(new VTable_FieldDef("[Offer Acceptance Date]", "System.DateTime", "offeracceptancedate", false, 18));
            TBL_Fields.Add(new VTable_FieldDef("[Follow-up Type]", "System.String", "followuptype", false, 19));
            TBL_Fields.Add(new VTable_FieldDef("[Follow-up Date]", "System.DateTime", "followupdate", false, 20));
            TBL_Fields.Add(new VTable_FieldDef("[Viewing Date]", "System.DateTime", "viewingdate", false, 21)); 	


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

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
    public class VTable_Offer_Details : VTable_Base
    {
        public const string VTableName = "[Offer Details]";

        public VTable_Offer_Details(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            TBL_Fields.Add(new VTable_FieldDef("[ID]", "System.Int32", "referenceid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Site ID]", "System.Int32", "siteid", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Date]", "System.DateTime", "offerdate", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Address 1]", "System.String", "address1", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Offer Amount]", "System.Double", "offeramount", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Number of Units]", "System.Double", "numberofunits", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Special Circumstances regarding sale]", "System.String", "notes", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Site ID Details2_ID]", "System.Int32", "siteiddetails2_id", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Town Details2_ID]", "System.Int32", "towndetails2_id", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Medium Details2_ID]", "System.Int32", "mediumdetails2_id", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Company Details2_ID]", "System.Int32", "companydetails2_id", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[Rep ID]", "System.Int32", "repid", false, 12)); // mapps to Refernce id of Contacts 
            TBL_Fields.Add(new VTable_FieldDef("[Rep's Details2_ID]", "System.Int32", "rep'sdetails2_id", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[Subject to Details2_ID]", "System.Int32", "subjecttodetails2_id", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[Con/Uncond Details2_ID]", "System.Int32", "conditionalid", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[Option Details2_ID]", "System.Int32", "optiondetails2_id", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[Acceptance Date]", "System.DateTime", "acceptancedate", false, 17));

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
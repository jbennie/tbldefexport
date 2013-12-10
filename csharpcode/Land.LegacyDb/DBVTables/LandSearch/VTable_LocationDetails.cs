using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common; 
using Lincore.DataTools;
using Lincore.DataTools.vtable_common;

/*
used by Lookups in CurrentFormar & Originator Company 
 */

namespace Land.LegacyDb.Tables
{
    public class VTable_LocationDetails : VTable_Base
    {
        public const string VTableName = "[Location Details]";

        public VTable_LocationDetails(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            // SQLfield , SQLType, importfield, isKey 
            TBL_Fields.Add(new VTable_FieldDef("ID", "System.Int32", "ID", false, 0));
            TBL_Fields.Add(new VTable_FieldDef("[Date received]", "System.String", "datereceived", 1));
            TBL_Fields.Add(new VTable_FieldDef("[Address1]", "System.String", "address1", 2));
            TBL_Fields.Add(new VTable_FieldDef("[Address2]", "System.String", "address2", 3));
            TBL_Fields.Add(new VTable_FieldDef("[Address3]", "System.String", "address3", 4)); 
            TBL_Fields.Add(new VTable_FieldDef("[Town]", "System.String", "town", 5));
            TBL_Fields.Add(new VTable_FieldDef("[County]", "System.String", "country", 6));
            TBL_Fields.Add(new VTable_FieldDef("[Postcode]", "System.String", "postcode", 7));
            TBL_Fields.Add(new VTable_FieldDef("[Owner Name]", "System.String", "ownername", 8));
            TBL_Fields.Add(new VTable_FieldDef("[Planning Number]", "System.String", "planningnumber", 9));
            TBL_Fields.Add(new VTable_FieldDef("[Number of Plots]", "System.Int32", "plotcount", 10));
            TBL_Fields.Add(new VTable_FieldDef("[Acreage (Hectares)]", "System.String", "sitesize", 11));
            TBL_Fields.Add(new VTable_FieldDef("[House, Flats etc]", "System.String", "propertytype", 12));
            TBL_Fields.Add(new VTable_FieldDef("[Approx Price]", "System.String", "approxprice", 13));
            TBL_Fields.Add(new VTable_FieldDef("[Offers by]", "System.String", "offersby", 14));
            TBL_Fields.Add(new VTable_FieldDef("[Current/Former Use_ID]", "System.Int32", "CFU_ID", 15));
            TBL_Fields.Add(new VTable_FieldDef("[Originator Company_ID]", "System.Int32", "ORGC_ID", 16));

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
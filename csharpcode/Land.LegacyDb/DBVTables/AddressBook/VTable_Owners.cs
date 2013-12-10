using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common; 
using Lincore.DataTools;
using Lincore.DataTools.vtable_common;


    /* Queries
     * Case Study 10 
     * Contacts Main and Sites
     * Site Flyleaf
     */

    /*forms
     * Owners
     */

    /*Reports
     *Owner Mailing Label
     */

namespace Land.LegacyDb.Tables
{
    public class VTable_Owners : VTable_Base
    {
        public const string VTableName = "[Owners]";

        public VTable_Owners(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            // SQLfield , SQLType, importfield, isKey 
            TBL_Fields.Add(new VTable_FieldDef("[OwnerID]", "System.Int32", "referenceid", false, 0));
            TBL_Fields.Add(new VTable_FieldDef("[Prefix]", "System.String", "prefix", 1));
            TBL_Fields.Add(new VTable_FieldDef("[Initial]", "System.String", "initial", 2));
            TBL_Fields.Add(new VTable_FieldDef("[Firstname]", "System.String", "firstname", 3));
            TBL_Fields.Add(new VTable_FieldDef("[Middlename]", "System.String", "middlename", 4));
            TBL_Fields.Add(new VTable_FieldDef("[Lastname]", "System.String", "lastname", 5));
            TBL_Fields.Add(new VTable_FieldDef("[Suffix]", "System.String", "suffix",6));
            TBL_Fields.Add(new VTable_FieldDef("[CompanyName]", "System.String", "organisationname", 7));
            TBL_Fields.Add(new VTable_FieldDef("[Address1]", "System.String", "addressline1", 8));
            TBL_Fields.Add(new VTable_FieldDef("[Address2]", "System.String", "addressline2", 9));
            TBL_Fields.Add(new VTable_FieldDef("[Address3]", "System.String", "addressline3", 10));
            TBL_Fields.Add(new VTable_FieldDef("[Town]", "System.String", "town", 11));
            TBL_Fields.Add(new VTable_FieldDef("[County]", "System.String", "county", 12));
            TBL_Fields.Add(new VTable_FieldDef("[Postcode]", "System.String", "postcode", 13));
            TBL_Fields.Add(new VTable_FieldDef("[HomePhone]", "System.String", "homephone", 14));
            TBL_Fields.Add(new VTable_FieldDef("[WorkPhone]", "System.String", "workphone", 15));
            TBL_Fields.Add(new VTable_FieldDef("[MobilePhone]", "System.String", "mobilephone", 16));
            TBL_Fields.Add(new VTable_FieldDef("[FaxNumber]", "System.String", "faxnumber", 17));

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

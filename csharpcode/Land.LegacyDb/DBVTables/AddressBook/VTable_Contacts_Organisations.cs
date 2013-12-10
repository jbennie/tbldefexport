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
    public class VTable_ContactsOrganisations : VTable_Base
    {
        public const string VTableName = "[Contacts - Organisations]"; // CONTACTORG

        public VTable_ContactsOrganisations(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            TBL_Fields.Add(new VTable_FieldDef("[Organisation ID]", "System.Int32", "referenceid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Contact ID]", "System.Int32", "contactid", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Original Entry Date]", "System.DateTime", "originalentrydate", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Developer Organisation ID]", "System.Int32", "developerorganisationid", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[OrganisationName]", "System.String", "organisationname", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Type]", "System.String", "type", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Region]", "System.String", "region", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Address 1]", "System.String", "addressline1", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Address 2]", "System.String", "addressline2", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Address 3]", "System.String", "addressline3", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Address 4]", "System.String", "addressline4", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[Town]", "System.String", "town", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[County]", "System.String", "county", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[PostalCode]", "System.String", "postcode", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[WorkPhone]", "System.String", "workphone", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[FaxNumber]", "System.String", "faxnumber", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[AlternativePhone]", "System.String", "homephone", false, 17));
            TBL_Fields.Add(new VTable_FieldDef("[Switchboard]", "System.String", "switchboard", false, 18));
            TBL_Fields.Add(new VTable_FieldDef("[DX number]", "System.String", "dxnumber", false, 19));
            TBL_Fields.Add(new VTable_FieldDef("[Company Email Address]", "System.String", "defaultemail", false, 20));
            TBL_Fields.Add(new VTable_FieldDef("[Website]", "System.String", "website", false, 21));
            TBL_Fields.Add(new VTable_FieldDef("[Date of Update]", "System.DateTime", "dateofupdate", false, 22));
            TBL_Fields.Add(new VTable_FieldDef("[No Contact Available]", "System.Boolean", "nocontactavailable", false, 23));

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
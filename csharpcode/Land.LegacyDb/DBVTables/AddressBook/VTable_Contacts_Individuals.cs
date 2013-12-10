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
    public class VTable_ContactsIndividuals : VTable_Base
    {
        public const string VTableName = @"[Contacts List - Individuals]"; // CONTACT

        public VTable_ContactsIndividuals(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            TBL_Fields.Add(new VTable_FieldDef("[Contact ID]", "System.Int32", "referenceid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Organisation ID]", "System.Int32", "organisationid", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Original Entry Date]", "System.DateTime", "originalentrydate", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Prefix]", "System.String", "prefix", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Initial]", "System.String", "initial", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[FirstName]", "System.String", "firstname", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[MiddleName]", "System.String", "middlename", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[LastName]", "System.String", "lastname", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Suffix]", "System.String", "suffix", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Title]", "System.String", "title", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Strategic]", "System.Boolean", "isstrategic", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[HomePhone]", "System.String", "homephone", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[MobilePhone]", "System.String", "mobilephone", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[AlternativePhone]", "System.String", "ddiphone", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[Work Phone]", "System.String", "workphone", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[EmailAddress1]", "System.String", "defaultemail", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[Fax Number]", "System.String", "faxnumber", false, 17));
            TBL_Fields.Add(new VTable_FieldDef("[Departmental Fax Number]", "System.String", "faxnumber", false, 18));
            TBL_Fields.Add(new VTable_FieldDef("[Date of Update]", "System.DateTime", "dateofupdate", false, 19));
            TBL_Fields.Add(new VTable_FieldDef("[Receive Case Study]", "System.String", "receivecasestudy", false, 20));
            TBL_Fields.Add(new VTable_FieldDef("[Frequency]", "System.String", "frequency", false, 21));
            TBL_Fields.Add(new VTable_FieldDef("[Exclude from Mailshot]", "System.Boolean", "excludefrommailshot", false, 22));
            TBL_Fields.Add(new VTable_FieldDef("[Left Company?]", "System.Boolean", "leftcompany?", false, 23));

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
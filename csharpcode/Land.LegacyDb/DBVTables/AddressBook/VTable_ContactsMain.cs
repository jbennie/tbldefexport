using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common; 
using Lincore.DataTools;
using Lincore.DataTools.vtable_common;

//Look like it is just the dump from outlook 

namespace Land.LegacyDb.Tables
{
    public class VTable_ContactsMain : VTable_Base
    {
        public const string VTableName = "[Contacts List - Main]"; // CONTACTSFROMOUTLOOK

        public VTable_ContactsMain(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        // B=Builder,C=Consultancy,D=Developer,NHA=New Homes Agency,O = Owner, S=Solicitor,TP=Town Planner
        // VALUE LIST => "Advertising Contact";"Ag Req";"Architect";"Builder";"Developer";"District Council";"Estate Agency";"Financial Advisors";"Owner";"Sales/Marketing";"System.Int32 Plot Req";"Solicitor";"Surveyor";"Supplier";"Town Planner";"Supplier/Advertising Contact"
         

        public override void SetTBLFields()
        {
            TBL_Fields.Add(new VTable_FieldDef("[Contact ID]", "System.Int32", "referenceid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Original Entry Date]", "System.DateTime", "originalentrydate", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Developer Organisation ID]", "System.Int32", "developerorganisationid", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Prefix]", "System.String", "prefix", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Initial]", "System.String", "initial", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[FirstName]", "System.String", "firstname", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[MiddleName]", "System.String", "middlename", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[LastName]", "System.String", "lastname", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Suffix]", "System.String", "suffix", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Title]", "System.String", "title", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Strategic]", "System.String", "isstrategic", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[OrganisationName]", "System.String", "organisationname", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[Type]", "System.String", "type", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[Region]", "System.String", "region", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[Address 1]", "System.String", "addressline1", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[Address 2]", "System.String", "addressline2", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[Address 3]", "System.String", "addressline3", false, 17));
            TBL_Fields.Add(new VTable_FieldDef("[Address 4]", "System.String", "addressline4", false, 18));
            TBL_Fields.Add(new VTable_FieldDef("[Town]", "System.String", "town", false, 19));
            TBL_Fields.Add(new VTable_FieldDef("[County]", "System.String", "county", false, 20));
            TBL_Fields.Add(new VTable_FieldDef("[PostalCode]", "System.String", "postcode", false, 21));
            TBL_Fields.Add(new VTable_FieldDef("[HomePhone]", "System.String", "homephone", false, 22));
            TBL_Fields.Add(new VTable_FieldDef("[WorkPhone]", "System.String", "workphone", false, 23));
            TBL_Fields.Add(new VTable_FieldDef("[MobilePhone]", "System.String", "mobilephone", false, 24));
            TBL_Fields.Add(new VTable_FieldDef("[FaxNumber]", "System.String", "faxnumber", false, 25));
            TBL_Fields.Add(new VTable_FieldDef("[AlternativePhone]", "System.String", "ddiphone", false, 26));
            TBL_Fields.Add(new VTable_FieldDef("[Switchboard]", "System.String", "switchboard", false, 27));
            TBL_Fields.Add(new VTable_FieldDef("[DX number]", "System.String", "dxnumber", false, 28));
            TBL_Fields.Add(new VTable_FieldDef("[EmailAddress1]", "System.String", "defaultemail", false, 29));
            TBL_Fields.Add(new VTable_FieldDef("[Company Email Address]", "System.String", "altemail", false, 30));
            TBL_Fields.Add(new VTable_FieldDef("[CompuServeID]", "System.String", "compuserveid", false, 31));
            TBL_Fields.Add(new VTable_FieldDef("[Website]", "System.String", "website", false, 32));
            TBL_Fields.Add(new VTable_FieldDef("[Account Number]", "System.String", "accountnumber", false, 33));
            TBL_Fields.Add(new VTable_FieldDef("[Checked]", "System.String", "checked", false, 34));
            TBL_Fields.Add(new VTable_FieldDef("[Date of Update]", "System.DateTime", "dateofupdate", false, 35));
            TBL_Fields.Add(new VTable_FieldDef("[Receive Case Study]", "System.String", "receivecasestudy", false, 36));
            TBL_Fields.Add(new VTable_FieldDef("[Frequency]", "System.String", "frequency", false, 37));
            TBL_Fields.Add(new VTable_FieldDef("[Medium]", "System.String", "medium", false, 38));

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

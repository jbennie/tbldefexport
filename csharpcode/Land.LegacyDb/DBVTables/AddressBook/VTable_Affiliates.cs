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
    public class VTable_Affiliates : VTable_Base
    {
        public const string VTableName = "[Affiliates]";

        public VTable_Affiliates(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {


            TBL_Fields.Add(new VTable_FieldDef("[ID]", "System.Int32", "id", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Category]", "System.String", "category", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Application Date]", "System.String", "applicationdate", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Name]", "System.String", "name", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Surname]", "System.String", "surname", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Job Code]", "System.String", "jobcode", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Job Description]", "System.String", "jobdescription", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Direct Line Tel]", "System.String", "directlinetel", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Fax No]", "System.String", "faxno", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Email]", "System.String", "email", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Department]", "System.String", "department", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[Authoriser/Influencer/Buyer]", "System.String", "authoriser_influencer_buyer", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[Area of business]", "System.String", "areaofbusiness", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[Company Code]", "System.String", "companycode", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[Group Indicator]", "System.String", "groupindicator", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[Parent Company]", "System.String", "parentcompany", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[Full Company Name]", "System.String", "fullcompanyname", false, 17));
            TBL_Fields.Add(new VTable_FieldDef("[Short Company Name]", "System.String", "shortcompanyname", false, 18));
            TBL_Fields.Add(new VTable_FieldDef("[Description]", "System.String", "description", false, 19));
            TBL_Fields.Add(new VTable_FieldDef("[Address 1]", "System.String", "address1", false, 20));
            TBL_Fields.Add(new VTable_FieldDef("[Address 2]", "System.String", "address2", false, 21));
            TBL_Fields.Add(new VTable_FieldDef("[Address 3]", "System.String", "address3", false, 22));
            TBL_Fields.Add(new VTable_FieldDef("[Town]", "System.String", "town", false, 23));
            TBL_Fields.Add(new VTable_FieldDef("[County]", "System.String", "county", false, 24));
            TBL_Fields.Add(new VTable_FieldDef("[Country]", "System.String", "country", false, 25));
            TBL_Fields.Add(new VTable_FieldDef("[Postcode]", "System.String", "postcode", false, 26));
            TBL_Fields.Add(new VTable_FieldDef("[Phone]", "System.String", "phone", false, 27));
            TBL_Fields.Add(new VTable_FieldDef("[Fax]", "System.String", "fax", false, 28));
            TBL_Fields.Add(new VTable_FieldDef("[E-mail]", "System.String", "email", false, 29));
            TBL_Fields.Add(new VTable_FieldDef("[Website]", "System.String", "website", false, 30));
            TBL_Fields.Add(new VTable_FieldDef("[Region Code]", "System.String", "regioncode", false, 31));
            TBL_Fields.Add(new VTable_FieldDef("[SOC]", "System.String", "soc", false, 32));
            TBL_Fields.Add(new VTable_FieldDef("[Principal product types]", "System.String", "principalproducttypes", false, 33));
            TBL_Fields.Add(new VTable_FieldDef("[Importer/Exporter]", "System.String", "importer_exporter", false, 34));
            TBL_Fields.Add(new VTable_FieldDef("[Employees]", "System.String", "employees", false, 35));
            TBL_Fields.Add(new VTable_FieldDef("[Turnover]", "System.String", "turnover", false, 36));
            TBL_Fields.Add(new VTable_FieldDef("[Account Record Key]", "System.String", "accountrecordkey", false, 37));
            TBL_Fields.Add(new VTable_FieldDef("[Reference number]", "System.String", "referencenumber", false, 38));
            TBL_Fields.Add(new VTable_FieldDef("[Account Type]", "System.String", "accounttype", false, 39));
            TBL_Fields.Add(new VTable_FieldDef("[Check Website]", "System.String", "checkwebsite", false, 40));
            TBL_Fields.Add(new VTable_FieldDef("[Check Companies House]", "System.String", "checkcompanieshouse", false, 41));
            TBL_Fields.Add(new VTable_FieldDef("[Check Yell Co UK]", "System.String", "checkyellcouk", false, 42));
            TBL_Fields.Add(new VTable_FieldDef("[Check MSN]", "System.String", "checkmsn", false, 43));
            TBL_Fields.Add(new VTable_FieldDef("[Check Google]", "System.String", "checkgoogle", false, 44));
            TBL_Fields.Add(new VTable_FieldDef("[Accepted/Rejected]", "System.String", "accepted_rejected", false, 45));
            TBL_Fields.Add(new VTable_FieldDef("[Start date]", "System.String", "startdate", false, 46));
            TBL_Fields.Add(new VTable_FieldDef("[Renewal date]", "System.String", "renewaldate", false, 47));
            TBL_Fields.Add(new VTable_FieldDef("[Last used date]", "System.String", "lastuseddate", false, 48));
            TBL_Fields.Add(new VTable_FieldDef("[Average balance]", "System.String", "averagebalance", false, 49));
            TBL_Fields.Add(new VTable_FieldDef("[Average worth]", "System.String", "averageworth", false, 50));
            TBL_Fields.Add(new VTable_FieldDef("[Accounting codes]", "System.String", "accountingcodes", false, 51));
            TBL_Fields.Add(new VTable_FieldDef("[Activity record key]", "System.String", "activityrecordkey", false, 52));
            TBL_Fields.Add(new VTable_FieldDef("[Activity code]", "System.String", "activitycode", false, 53));
            TBL_Fields.Add(new VTable_FieldDef("[Activity date]", "System.String", "activitydate", false, 54));
            TBL_Fields.Add(new VTable_FieldDef("[Response type]", "System.String", "responsetype", false, 55));
            TBL_Fields.Add(new VTable_FieldDef("[Recency of last response]", "System.String", "recencyoflastresponse", false, 56));
            TBL_Fields.Add(new VTable_FieldDef("[Frequency of response]", "System.String", "frequencyofresponse", false, 57));
            TBL_Fields.Add(new VTable_FieldDef("[Value of response]", "System.String", "valueofresponse", false, 58));

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

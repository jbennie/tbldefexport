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
    public class VTable_ContactsFromOutlook : VTable_Base
    {
        public const string VTableName = "[Contacts from Outlook]";

        public VTable_ContactsFromOutlook(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {

            TBL_Fields.Add(new VTable_FieldDef("[Link ID]", "System.Int32", "referenceid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Title]", "System.String", "title", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[FirstName]", "System.String", "firstname", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[MiddleName]", "System.String", "middlename", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[LastName]", "System.String", "lastname", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Suffix]", "System.String", "suffix", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Company]", "System.String", "company", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Department]", "System.String", "department", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[JobTitle]", "System.String", "jobtitle", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessStreet]", "System.String", "addressline1", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessStreet2]", "System.String", "addressline2", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessStreet3]", "System.String", "addressline3", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessCity]", "System.String", "town", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessState]", "System.String", "county", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessPostalCode]", "System.String", "postcode", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessCountry]", "System.String", "country", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[HomeStreet]", "System.String", "homestreet", false, 17));
            TBL_Fields.Add(new VTable_FieldDef("[HomeStreet2]", "System.String", "homestreet2", false, 18));
            TBL_Fields.Add(new VTable_FieldDef("[HomeStreet3]", "System.String", "homestreet3", false, 19));
            TBL_Fields.Add(new VTable_FieldDef("[HomeCity]", "System.String", "homecity", false, 20));
            TBL_Fields.Add(new VTable_FieldDef("[HomeState]", "System.String", "homestate", false, 21));
            TBL_Fields.Add(new VTable_FieldDef("[HomePostalCode]", "System.String", "homepostalcode", false, 22));
            TBL_Fields.Add(new VTable_FieldDef("[HomeCountry]", "System.String", "homecountry", false, 23));
            TBL_Fields.Add(new VTable_FieldDef("[OtherStreet]", "System.String", "otherstreet", false, 24));
            TBL_Fields.Add(new VTable_FieldDef("[OtherStreet2]", "System.String", "otherstreet2", false, 25));
            TBL_Fields.Add(new VTable_FieldDef("[OtherStreet3]", "System.String", "otherstreet3", false, 26));
            TBL_Fields.Add(new VTable_FieldDef("[OtherCity]", "System.String", "othercity", false, 27));
            TBL_Fields.Add(new VTable_FieldDef("[OtherState]", "System.String", "otherstate", false, 28));
            TBL_Fields.Add(new VTable_FieldDef("[OtherPostalCode]", "System.String", "otherpostalcode", false, 29));
            TBL_Fields.Add(new VTable_FieldDef("[OtherCountry]", "System.String", "othercountry", false, 30));
            TBL_Fields.Add(new VTable_FieldDef("[AssistantsPhone]", "System.String", "assistantsphone", false, 31));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessFax]", "System.String", "faxnumber", false, 32));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessPhone]", "System.String", "workphone", false, 33));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessPhone2]", "System.String", "homephone", false, 34));
            TBL_Fields.Add(new VTable_FieldDef("[Callback]", "System.String", "callback", false, 35));
            TBL_Fields.Add(new VTable_FieldDef("[CarPhone]", "System.String", "mobilephone", false, 36));
            TBL_Fields.Add(new VTable_FieldDef("[CompanyMainPhone]", "System.String", "switchboard", false, 37));
            TBL_Fields.Add(new VTable_FieldDef("[HomeFax]", "System.String", "homefax", false, 38));
            TBL_Fields.Add(new VTable_FieldDef("[HomePhone]", "System.String", "homephone", false, 39));
            TBL_Fields.Add(new VTable_FieldDef("[HomePhone2]", "System.String", "homephone2", false, 40));
            TBL_Fields.Add(new VTable_FieldDef("[ISDN]", "System.String", "isdn", false, 41));
            TBL_Fields.Add(new VTable_FieldDef("[MobilePhone]", "System.String", "mobilephone", false, 42));
            TBL_Fields.Add(new VTable_FieldDef("[OtherFax]", "System.String", "otherfax", false, 43));
            TBL_Fields.Add(new VTable_FieldDef("[OtherPhone]", "System.String", "otherphone", false, 44));
            TBL_Fields.Add(new VTable_FieldDef("[Pager]", "System.String", "pager", false, 45));
            TBL_Fields.Add(new VTable_FieldDef("[PrimaryPhone]", "System.String", "primaryphone", false, 46));
            TBL_Fields.Add(new VTable_FieldDef("[RadioPhone]", "System.String", "radiophone", false, 47));
            TBL_Fields.Add(new VTable_FieldDef("[TTYTDDPhone]", "System.String", "ttytddphone", false, 48));
            TBL_Fields.Add(new VTable_FieldDef("[Telex]", "System.String", "telex", false, 49));
            TBL_Fields.Add(new VTable_FieldDef("[Account]", "System.String", "account", false, 50));
            TBL_Fields.Add(new VTable_FieldDef("[Anniversary]", "System.String", "anniversary", false, 51));
            TBL_Fields.Add(new VTable_FieldDef("[AssistantsName]", "System.String", "assistantsname", false, 52));
            TBL_Fields.Add(new VTable_FieldDef("[BillingInformation]", "System.String", "billinginformation", false, 53));
            TBL_Fields.Add(new VTable_FieldDef("[Birthday]", "System.DateTime", "birthday", false, 54));
            TBL_Fields.Add(new VTable_FieldDef("[BusinessAddressPOBox]", "System.String", "businessaddresspobox", false, 55));
            TBL_Fields.Add(new VTable_FieldDef("[Categories]", "System.String", "categories", false, 56));
            TBL_Fields.Add(new VTable_FieldDef("[Children]", "System.String", "children", false, 57));
            TBL_Fields.Add(new VTable_FieldDef("[DirectoryServer]", "System.String", "directoryserver", false, 58));
            TBL_Fields.Add(new VTable_FieldDef("[EmailAddress]", "System.String", "defaultemail", false, 59));
            TBL_Fields.Add(new VTable_FieldDef("[EmailType]", "System.String", "emailtype", false, 60));
            TBL_Fields.Add(new VTable_FieldDef("[EmailDisplayName]", "System.String", "emaildisplayname", false, 61));
            TBL_Fields.Add(new VTable_FieldDef("[Email2Address]", "System.String", "email2address", false, 62));
            TBL_Fields.Add(new VTable_FieldDef("[Email2Type]", "System.String", "email2type", false, 63));
            TBL_Fields.Add(new VTable_FieldDef("[Email2DisplayName]", "System.String", "email2displayname", false, 64));
            TBL_Fields.Add(new VTable_FieldDef("[Email3Address]", "System.String", "email3address", false, 65));
            TBL_Fields.Add(new VTable_FieldDef("[Email3Type]", "System.String", "email3type", false, 66));
            TBL_Fields.Add(new VTable_FieldDef("[Email3DisplayName]", "System.String", "email3displayname", false, 67));
            TBL_Fields.Add(new VTable_FieldDef("[Gender]", "System.String", "gender", false, 68));
            TBL_Fields.Add(new VTable_FieldDef("[GovernmentIDNumber]", "System.String", "governmentidnumber", false, 69));
            TBL_Fields.Add(new VTable_FieldDef("[Hobby]", "System.String", "hobby", false, 70));
            TBL_Fields.Add(new VTable_FieldDef("[HomeAddressPOBox]", "System.String", "homeaddresspobox", false, 71));
            TBL_Fields.Add(new VTable_FieldDef("[Initials]", "System.String", "initial", false, 72));
            TBL_Fields.Add(new VTable_FieldDef("[InternetFreeBusy]", "System.String", "internetfreebusy", false, 73));
            TBL_Fields.Add(new VTable_FieldDef("[Keywords]", "System.String", "keywords", false, 74));
            TBL_Fields.Add(new VTable_FieldDef("[Language1]", "System.String", "language1", false, 75));
            TBL_Fields.Add(new VTable_FieldDef("[Location]", "System.String", "location", false, 76));
            TBL_Fields.Add(new VTable_FieldDef("[ManagersName]", "System.String", "managersname", false, 77));
            TBL_Fields.Add(new VTable_FieldDef("[Mileage]", "System.String", "mileage", false, 78));
            TBL_Fields.Add(new VTable_FieldDef("[Notes]", "System.String", "notes", false, 79));
            TBL_Fields.Add(new VTable_FieldDef("[OfficeLocation]", "System.String", "officelocation", false, 80));
            TBL_Fields.Add(new VTable_FieldDef("[OrganizationalIDNumber]", "System.String", "organizationalidnumber", false, 81));
            TBL_Fields.Add(new VTable_FieldDef("[OtherAddressPOBox]", "System.String", "otheraddresspobox", false, 82));
            TBL_Fields.Add(new VTable_FieldDef("[Priority]", "System.String", "priority", false, 83));
            TBL_Fields.Add(new VTable_FieldDef("[Private]", "System.Boolean", "private", false, 84));
            TBL_Fields.Add(new VTable_FieldDef("[Profession]", "System.String", "profession", false, 85));
            TBL_Fields.Add(new VTable_FieldDef("[ReferredBy]", "System.String", "referredby", false, 86));
            TBL_Fields.Add(new VTable_FieldDef("[Sensitivity]", "System.String", "sensitivity", false, 87));
            TBL_Fields.Add(new VTable_FieldDef("[Spouse]", "System.String", "spouse", false, 88));
            TBL_Fields.Add(new VTable_FieldDef("[User1]", "System.String", "user1", false, 89));
            TBL_Fields.Add(new VTable_FieldDef("[User2]", "System.String", "user2", false, 90));
            TBL_Fields.Add(new VTable_FieldDef("[User3]", "System.String", "user3", false, 91));
            TBL_Fields.Add(new VTable_FieldDef("[User4]", "System.String", "user4", false, 92));
            TBL_Fields.Add(new VTable_FieldDef("[WebPage]", "System.String", "website", false, 93));

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

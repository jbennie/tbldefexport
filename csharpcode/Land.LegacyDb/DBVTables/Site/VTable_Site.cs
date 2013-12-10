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
    public class VTable_Site : VTable_Base
    {
        public const string VTableName = "Sites";

        public VTable_Site(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        /*
         [Site]
            TBL_Fields.Add(new VTable_FieldDef("[Field2]", "System.Double", "field2", true, 1 )); 	
            TBL_Fields.Add(new VTable_FieldDef("[Town]", "System.String", "town", false, 2)); 	
            TBL_Fields.Add(new VTable_FieldDef("[Site Name / Address]", "System.String", "sitename_address", false, 3)); 	
            TBL_Fields.Add(new VTable_FieldDef("[ID]", "System.Int32", "id", false, 4));
         */

        public override void SetTBLFields()
        {
            
            TBL_Fields.Add(new VTable_FieldDef("[FormID]", "System.Int32", "referenceid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[DateReceived]", "System.DateTime", "datereceived", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Origin]", "System.String", "origin", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Origin Individual]", "System.String", "originindividual", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Buyer RWH]", "System.String", "buyerrwh", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Commission Payable]", "System.Int32", "commissionpayable", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Field1]", "System.String", "country", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Site Address1]", "System.String", "siteaddress1", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Address2]", "System.String", "address2", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Address 3]", "System.String", "address3", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Town]", "System.String", "town", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[County]", "System.String", "county", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[Postcode]", "System.String", "postcode", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[TypeOfSite]", "System.String", "typeofsite", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[Summary Details]", "System.String", "summarydetails", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[Brown/Greenfield]", "System.String", "brown_greenfield", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[CurrentUseOfLand]", "System.String", "currentuseofland", false, 17));
            TBL_Fields.Add(new VTable_FieldDef("[OwnerID]", "System.Double", "ownerid", false, 18));
            TBL_Fields.Add(new VTable_FieldDef("[OwnerName]", "System.String", "ownername", false, 19));
            TBL_Fields.Add(new VTable_FieldDef("[Hectares]", "System.Int32", "hectares", false, 20));
            TBL_Fields.Add(new VTable_FieldDef("[Acreage]", "System.Int32", "acreage", false, 21));
            TBL_Fields.Add(new VTable_FieldDef("[Sq Metres]", "System.Int32", "sqmetres", false, 22));
            TBL_Fields.Add(new VTable_FieldDef("[Sq Feet]", "System.Int32", "sqfeet", false, 23));
            TBL_Fields.Add(new VTable_FieldDef("[Developable]", "System.Double", "developable", false, 24));
            TBL_Fields.Add(new VTable_FieldDef("[Plots]", "System.Int32", "plots", false, 25));
            TBL_Fields.Add(new VTable_FieldDef("[Projected Predominant Unit]", "System.String", "projectedpredominantunit", false, 26));
            TBL_Fields.Add(new VTable_FieldDef("[Bedsit/Studio]", "System.Int32", "bedsit_studio", false, 27));
            TBL_Fields.Add(new VTable_FieldDef("[B/SGarage]", "System.Int32", "b_sgarage", false, 28));
            TBL_Fields.Add(new VTable_FieldDef("[B/SParking]", "System.Int32", "b_sparking", false, 29));
            TBL_Fields.Add(new VTable_FieldDef("[Flats]", "System.Int32", "flats", false, 30));
            TBL_Fields.Add(new VTable_FieldDef("[FlatsGarage]", "System.Int32", "flatsgarage", false, 31));
            TBL_Fields.Add(new VTable_FieldDef("[FlatsParking]", "System.Int32", "flatsparking", false, 32));
            TBL_Fields.Add(new VTable_FieldDef("[TerrHouse]", "System.Int32", "terrhouse", false, 33));
            TBL_Fields.Add(new VTable_FieldDef("[TerrHouseGarage]", "System.Int32", "terrhousegarage", false, 34));
            TBL_Fields.Add(new VTable_FieldDef("[TerrHouseParking]", "System.Int32", "terrhouseparking", false, 35));
            TBL_Fields.Add(new VTable_FieldDef("[SemiDet]", "System.Int32", "semidet", false, 36));
            TBL_Fields.Add(new VTable_FieldDef("[SemiDetGarage]", "System.Int32", "semidetgarage", false, 37));
            TBL_Fields.Add(new VTable_FieldDef("[SemiDetParking]", "System.Int32", "semidetparking", false, 38));
            TBL_Fields.Add(new VTable_FieldDef("[Det]", "System.Int32", "det", false, 39));
            TBL_Fields.Add(new VTable_FieldDef("[DetatchedGarage]", "System.Int32", "detatchedgarage", false, 40));
            TBL_Fields.Add(new VTable_FieldDef("[DetatchedParking]", "System.Int32", "detatchedparking", false, 41));
            TBL_Fields.Add(new VTable_FieldDef("[SemiDetBungalows]", "System.Int32", "semidetbungalows", false, 42));
            TBL_Fields.Add(new VTable_FieldDef("[SemiDetBungalowsGarage]", "System.Int32", "semidetbungalowsgarage", false, 43));
            TBL_Fields.Add(new VTable_FieldDef("[DetachedBungalows]", "System.Int32", "detachedbungalows", false, 44));
            TBL_Fields.Add(new VTable_FieldDef("[DetBungalowsGarages]", "System.Int32", "detbungalowsgarages", false, 45));

            //Valuation
            TBL_Fields.Add(new VTable_FieldDef("[Projected GDV]", "System.Double", "projectedgdv", false, 46));
            TBL_Fields.Add(new VTable_FieldDef("[Guide Price]", "System.Double", "guideprice", false, 47));
            TBL_Fields.Add(new VTable_FieldDef("[Final Pur Price]", "System.Double", "finalpurprice", false, 48));
            
            //Planning
            TBL_Fields.Add(new VTable_FieldDef("[Has Planning?]", "System.String", "hasplanning?", false, 49));
            TBL_Fields.Add(new VTable_FieldDef("[Planning Nr]", "System.String", "planningnr", false, 50));
            TBL_Fields.Add(new VTable_FieldDef("[App Submission Date]", "System.String", "appsubmissiondate", false, 51));
            TBL_Fields.Add(new VTable_FieldDef("[Officer Recommendation]", "System.Boolean", "officerrecommendation", false, 52));
            TBL_Fields.Add(new VTable_FieldDef("[Committee Date]", "System.String", "committeedate", false, 53));
            TBL_Fields.Add(new VTable_FieldDef("[Type of Planning]", "System.String", "typeofplanning", false, 54));
            TBL_Fields.Add(new VTable_FieldDef("[Planning Approved]", "System.String", "planningapproved", false, 55));
            TBL_Fields.Add(new VTable_FieldDef("[Planning App Date]", "System.DateTime", "planningappdate", false, 56));
            TBL_Fields.Add(new VTable_FieldDef("[Contract Extension]", "System.String", "contractextension", false, 57));
            TBL_Fields.Add(new VTable_FieldDef("[Appeal Date]", "System.DateTime", "appealdate", false, 58));
            TBL_Fields.Add(new VTable_FieldDef("[Full Approval Date]", "System.String", "fullapprovaldate", false, 59));
            
            // Sale
            TBL_Fields.Add(new VTable_FieldDef("[Status]", "System.String", "status", false, 60));
            TBL_Fields.Add(new VTable_FieldDef("[Status Desc]", "System.String", "statusdesc", false, 61));
            TBL_Fields.Add(new VTable_FieldDef("[Sale Date Agreed/STC]", "System.DateTime", "saledateagreed_stc", false, 62));
            
            TBL_Fields.Add(new VTable_FieldDef("[STC ID]", "System.Int32", "stcid", false, 63));
            TBL_Fields.Add(new VTable_FieldDef("[Confirmation of Inst]", "System.Boolean", "confirmationofinst", false, 64));
            TBL_Fields.Add(new VTable_FieldDef("[Confirmation of Inst Date]", "System.DateTime", "confirmationofinstdate", false, 65));
            TBL_Fields.Add(new VTable_FieldDef("[Draft Contract Submitted]", "System.DateTime", "draftcontractsubmitted", false, 66));
            TBL_Fields.Add(new VTable_FieldDef("[Searches Submitted]", "System.DateTime", "searchessubmitted", false, 67));
            TBL_Fields.Add(new VTable_FieldDef("[Preliminary Enquiries]", "System.DateTime", "preliminaryenquiries", false, 68));
            TBL_Fields.Add(new VTable_FieldDef("[ProjEx]", "System.DateTime", "projex", false, 69));
            TBL_Fields.Add(new VTable_FieldDef("[ProjCompl]", "System.DateTime", "projcompl", false, 70));
            TBL_Fields.Add(new VTable_FieldDef("[Exch Date]", "System.DateTime", "exchdate", false, 71));
            TBL_Fields.Add(new VTable_FieldDef("[Compl Date]", "System.DateTime", "compldate", false, 72));
            TBL_Fields.Add(new VTable_FieldDef("[Conditions]", "System.String", "conditions", false, 73));

            TBL_Fields.Add(new VTable_FieldDef("[Option Expiry Date]", "System.String", "optionexpirydate", false, 74));
            TBL_Fields.Add(new VTable_FieldDef("[Length of Option]", "System.String", "lengthofoption", false, 75));
            TBL_Fields.Add(new VTable_FieldDef("[Option Premium]", "System.Double", "optionpremium", false, 76));
            TBL_Fields.Add(new VTable_FieldDef("[OMV %]", "System.Int32", "omvpercent", false, 77));
            TBL_Fields.Add(new VTable_FieldDef("[Option Fee]", "System.Int32", "optionfee", false, 78));
            TBL_Fields.Add(new VTable_FieldDef("[Option % Commission]", "System.Int32", "optionpercentcommission", false, 79));
            TBL_Fields.Add(new VTable_FieldDef("[Option Invoice #]", "System.Int32", "optioninvoicenumber", false, 80));
            TBL_Fields.Add(new VTable_FieldDef("[Option Invoice Date]", "System.DateTime", "optioninvoicedate", false, 81));
            
            TBL_Fields.Add(new VTable_FieldDef("[Purchaser/Company]", "System.String", "purchasername", false, 82));
            TBL_Fields.Add(new VTable_FieldDef("[Purchaser Co ID]", "System.Int32", "purchasercoid", false, 83));
            
            TBL_Fields.Add(new VTable_FieldDef("[Land Buyer]", "System.String", "landbuyer", false, 84));
            
            TBL_Fields.Add(new VTable_FieldDef("[Development Name]", "System.String", "developmentname", false, 85));
            TBL_Fields.Add(new VTable_FieldDef("[Right to Market]", "System.Boolean", "righttomarket", false, 86));
            TBL_Fields.Add(new VTable_FieldDef("[RTM %]", "System.Int32", "rtmpercent", false, 87));
            TBL_Fields.Add(new VTable_FieldDef("[RTM Commission]", "System.Double", "rtmcommission", false, 88));
            
            TBL_Fields.Add(new VTable_FieldDef("[Demolition]", "System.String", "demolition", false, 89));
            TBL_Fields.Add(new VTable_FieldDef("[Foundations]", "System.String", "foundations", false, 90));
            TBL_Fields.Add(new VTable_FieldDef("[Construction]", "System.String", "construction", false, 91));
            
            TBL_Fields.Add(new VTable_FieldDef("[Sales Start Date]", "System.String", "salesstartdate", false, 92));
            TBL_Fields.Add(new VTable_FieldDef("[Nett Fee %]", "System.Int32", "nettfeepercent", false, 93));
            TBL_Fields.Add(new VTable_FieldDef("[Commission]", "System.Double", "commission", false, 94));
            TBL_Fields.Add(new VTable_FieldDef("[Invoice No]", "System.String", "invoiceno", false, 95));
            TBL_Fields.Add(new VTable_FieldDef("[Invoice Date]", "System.DateTime", "invoicedate", false, 96));

            TBL_Fields.Add(new VTable_FieldDef("[Vendor Solicitor]", "System.Int32", "vendorsolicitorid", false, 97));
            TBL_Fields.Add(new VTable_FieldDef("[Vendor's Solicitors]", "System.String", "vendorsolicitorname", false, 98));
            TBL_Fields.Add(new VTable_FieldDef("[Vendor's Partner Name]", "System.String", "vendorpartnername", false, 99));
            TBL_Fields.Add(new VTable_FieldDef("[Purchaser Solicitor]", "System.Int32", "purchasersolicitorid", false, 100));
            TBL_Fields.Add(new VTable_FieldDef("[Purchaser's Solicitors]", "System.String", "purchasersolicitorname", false, 101));
            TBL_Fields.Add(new VTable_FieldDef("[Purchaser's Partner Name]", "System.String", "purchaser'spartnername", false, 102));

            TBL_Fields.Add(new VTable_FieldDef("[Dead File Nr]", "System.String", "deadfilenr", false, 103));
            TBL_Fields.Add(new VTable_FieldDef("[Dead File Box Nr]", "System.String", "deadfileboxnr", false, 104));
            TBL_Fields.Add(new VTable_FieldDef("[Priority]", "System.String", "priority", false, 105));
            TBL_Fields.Add(new VTable_FieldDef("[Comments]", "System.String", "comments", false, 106));
            TBL_Fields.Add(new VTable_FieldDef("[Reason]", "System.String", "reason", false, 107));
            TBL_Fields.Add(new VTable_FieldDef("[Update]", "System.DateTime", "update", false, 108));
            TBL_Fields.Add(new VTable_FieldDef("[Date Sent]", "System.DateTime", "datesent", false, 109));
            TBL_Fields.Add(new VTable_FieldDef("[Invoice Address]", "System.Int32", "invoiceaddress", false, 110));
            TBL_Fields.Add(new VTable_FieldDef("[Inv Date Paid]", "System.DateTime", "invdatepaid", false, 111));
            TBL_Fields.Add(new VTable_FieldDef("[Proj Release]", "System.DateTime", "projrelease", false, 112));
            TBL_Fields.Add(new VTable_FieldDef("[Probability]", "System.Int32", "probability", false, 113));
            TBL_Fields.Add(new VTable_FieldDef("[Contract Long Stop]", "System.DateTime", "contractlongstop", false, 114));
            TBL_Fields.Add(new VTable_FieldDef("[Proj Contract Long Stop]", "System.DateTime", "projcontractlongstop", false, 115));
            TBL_Fields.Add(new VTable_FieldDef("[Include in Sales Chart]", "System.Boolean", "includeinsaleschart", false, 116));

            //reset col ID
            int col = 0;
            foreach (VTable_FieldDef d in TBL_Fields)
            {
                d.CSVPosition = col;
                col++;
                d.FName = d.FName.Replace("[","").Replace("]","");
            }
        }

        public void Fill(Int32 SiteID)
        {
            IDbCommand cmd = _DatabaseConfig.GetHandler().getDataCommand("Select " + this.GetSQLFields() + " from " + this.Table.TableName + " where [FormID] = @[FormID]", this._DBConn);
            cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@[FormID]", Type.GetType("System.Int32")));
            ((IDbDataParameter)cmd.Parameters["@[FormID]"]).Value = SiteID;
            this.Fill(cmd);
        }
 

    }
}
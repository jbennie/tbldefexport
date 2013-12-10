using System;
using System.Collections.Generic;
using System.Collections;
//using System.Collections.Specialized; 
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Lincore.DataTools.DBHandler.OLEDB;
using Lincore.DataTools.DBConfig.OLEDB;
using Lincore.DataTools.vtable_common;
using Land.LegacyDb;
using Land.LegacyDb.Tables;

using Land.Data.Models;
using Land.Data;
using System.Reflection;
using Land.Data.Attributes;


namespace LandSync
{
    /// <summary>
    /// This class will sync the 
    /// </summary>
    public class SyncDB : IDisposable
    {
        ISyncSource Source;
        IDbConnection _connection;

        IList<object> log;

        SiteManagementContext db;
     
        public SyncDB(ISyncSource source, IList<object> llog)
        {
            log = llog;
            Source = source;
            _connection = source.ReadChangesFrom().GetHandler().getDataConnection();

            db = new SiteManagementContext();
        }
      
        private void logger(object obj, Lincore.SimpleEvent.RptEventArgs e)
        {
            log.Add(e._Message);
        }

        /// <summary>
        /// Read Legacy DB, Clean data and Export to Flat files 
        /// </summary>
        public void ExportLandDB(string exportto)
        {
            ExportTable(new VTable_Affiliates(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_Agents(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_ContactsMain(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_ContactsFromOutlook(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_ContactsIndividuals(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_ContactsOrganisations(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_ContractDetails(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_DeveloperCountyRequirements(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_DeveloperRequirementsMain(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_Greensheet_AdditionalDetails(Source.ReadChangesFrom()), exportto);
            //  ExportTable(new VTable_Greensheet_Comments(Source.ReadChangesFrom()), @"c:\RWH\csvdata\");
            ExportTable(new VTable_Greensheet_Data(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_Greensheet_Details(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_LandAgents(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_LocationDetails(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_Offer_Details(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_Owners(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_Site(Source.ReadChangesFrom()), exportto);
            ExportTable(new VTable_Towns(Source.ReadChangesFrom()), exportto); 

        }

        public void ImportContacts(string[] path, VTable_Base tblstruct)
        {

            var _Contacts = db.Contacts.Include(s => s.Type).Include(s => s.Town).ToList();
            DataTable tmp = tblstruct.ImportASCII_CSV(path[0], eMergeTableType.ImportSpec);

            InsertContactTypes(tmp);
            db.SaveChanges();

            foreach (DataRow r in tmp.Rows)
            {
                Int32 ItemId = MapInt32(r, "referenceid");

                if (!(_Contacts.Exists(s => s.referenceid == ItemId)))
                {
                    Contact item = new Contact();
                    if (MapContactData(r, ref item))
                    {
                        if (item.Code.Length > 0 || item.AddressLine1.Length > 0)
                        {
                            // only save if the item has usable info
                            db.Contacts.Add(item);
                            db.SaveChanges();
                        }
                        
                    }
                }
            }
           
        }
        public void ImportSites(string[] paths, VTable_Base tblstruct)
        {

            // VTable_Site tmp = new VTable_Site(Source.ReadChangesFrom());  //("sites", false, false);

            // add new method like this to read in our new file format. 
            DataTable tmp = tblstruct.ImportASCII_CSV(paths[0], eMergeTableType.ImportSpec);      

          //  VTable_Offer_Details tblOffers = new VTable_Offer_Details(Source.ReadChangesFrom());
          //  DataTable OffersTable = tblOffers.ImportASCII_CSV(paths[1], eMergeTableType.ImportSpec);

            InsertSiteTypes(tmp);
//            InsertOfferTypes(OffersTable);           

        //    int limitcount = 5;
            foreach (DataRow r in tmp.Rows)
            {
                int ItemId = MapInt32(r, "referenceid");
               
                if (db.Sites.Count(s => s.referenceid == ItemId) == 0)
                {
                    //site does not yet exit so create new                    
                    var item = new Site();
                    item.referenceid = ItemId;
                    if (MapSiteData(r, ref item))
                    {            
                        db.Sites.Add(item);// add to db version to save,.   
                        db.SaveChanges();
                    }
                }

                // debug break -- remove for prod test. 
           
              //  if (limitcount <= 0)
              //     break;
              //     limitcount--;
            }
         
        }

        public void ImportSiteOffers(string[] paths, VTable_Base tblstruct) 
        {            
            DataTable tmp = tblstruct.ImportASCII_CSV(paths[0], eMergeTableType.ImportSpec);                               
            InsertOfferTypes(tmp);  
            
            foreach (DataRow r in tmp.Rows)
            {
                int SiteId = MapInt32(r, "siteid");
                if (db.Sites.Count(s => s.referenceid == SiteId) == 1)
                {
                    //site does not yet exit so create new                    
                    var site = db.Sites.First(s => s.referenceid == SiteId);                
                    site.SiteOffers.Add(MapSiteOffer(r));
                    db.SaveChanges();                     
                }
            }
        }

        private void fixSiteName(Site s) 
        {
            if (String.IsNullOrEmpty(s.Name))
            {
                if (String.IsNullOrEmpty(s.AddressLineOne))
                {
                    s.Name = s.referenceid.ToString();
                }
                else
                {
                    s.Name = s.AddressLineOne;
                }
            }
        }

        public void ImportTowns(string[] paths, VTable_Base tblstruct) 
        {   
     
            DataTable tmp = tblstruct.ImportASCII_CSV(paths[0], eMergeTableType.ImportSpec);
            
            foreach (DataRow r in tmp.Rows)
            {
                String town = MapString(r, "town");
                String County = MapString(r, "county"); 

                if (!String.IsNullOrEmpty(town))
                {               // && s.County == County.ToUpper().Trim()  -- Duplicate towns + regions is allowed but only manually. 
                    if (db.Towns.Count(s => s.Name == town.ToUpper().Trim() ) == 0)
                    {
                        Town item = new Town();
                        if (MapTownData(r, ref item))
                        {
                            db.Towns.Add(item);
                            db.SaveChanges();                       
                        }
                    }                    
                }
            }
            

        }
      
        //============ Support Functions to assist mapping data to EF POCO types ========
 
        #region support functions to Export data from legacy db 
    
        private void ExportTable(VTable_Base tmp, string outfile)
        {
            tmp.OnLogError += new VTable_Base.VTableLogErrorHandler(logger);
            try
            {

                string SQL = VTable_FieldDef.SelectFrom(tmp.GetSQLFields(), tmp.Table.TableName) + " order by [" + tmp.TBL_Fields[0].ToString() + "] desc";

                //    log.Add(SQL);

                tmp.Fill(Source.ReadChangesFrom().GetHandler().getDataCommand(SQL));

                log.Add("Fill " + tmp.GetSQLTableName() + " Rows:" + tmp.Table.Rows.Count);

                if (tmp.Table.Rows.Count >= 1)
                {
                    tmp.Export_ImportSpec(outfile + tmp.GetSQLTableName().Replace(" ", "_") + ".csv", '|', UTF8Encoding.UTF8, true);
                    //    log.Add("file:" + outfile + tmp.GetSQLTableName().Replace(" ", "_") + ".csv"); 
                }
                else
                {
                    log.Add(SQL);
                }
            }
            catch (Exception ex)
            {
                log.Add(ex.Message);
            }
        }
        #endregion

        #region Lookup and Insert Complex Reference Types
        
        private void InsertContactTypes(DataTable srctbl)
        {
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "town")) CreateTown(s, "", "", "UK");
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "type")) CreateLandType<LandRoleType>(db.Roles, s);
        }

        private void InsertOfferTypes(DataTable srctbl)
        {
            //SEED THESE
            CreateLandType<ConditionalType>(db.ConditionalTypes, "CONDITIONAL", "CON", 6);
            CreateLandType<ConditionalType>(db.ConditionalTypes, "UNCONDITIONAL", "UNC", 9);
            CreateLandType<ConditionalType>(db.ConditionalTypes, "NOT SPECIFIED", "TBC");

            CreateLandType<DecissionType>(db.DecissionTypes, "ACCEPTED", "Y");
            CreateLandType<DecissionType>(db.DecissionTypes, "REJECTED", "N");
            CreateLandType<DecissionType>(db.DecissionTypes, "NOT SPECIFIED", "M");
            CreateLandType<DecissionType>(db.DecissionTypes, "PENDING", "P");

            foreach (Int32 i in VTable_Base.SelectDistinct(srctbl, "subjecttodetails2_id")) CreateLandType<OfferSubjecttoType>(db.OfferSubjecttoTypes, MapSubjecttoID(i), Convert.ToString(i), i);


        }

        private void InsertSiteTypes(DataTable srctbl)
        {

            foreach (string s in VTable_Base.SelectDistinct(srctbl, "town")) CreateTown(s, "", "", "UK");
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "originindividual")) CreateContactType<Agent>(db.LandAgents, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "buyerrwh")) CreateContactType<Negotiator>(db.LandNegotiators, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "ownername")) CreateContactType<Owner>(db.LandOwners, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "vendorsolicitorname")) CreateContactType<VendorSolicitor>(db.VendorSolicitors, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "landbuyer")) CreateContactType<Buyer>(db.LandBuyer, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "purchasername")) CreateContactType<Purchaser>(db.Purchasers, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "purchasersolicitorname")) CreateContactType<PurchaserSolicitor>(db.PurchaserSolicitors, s);

            foreach (string s in VTable_Base.SelectDistinct(srctbl, "brown_greenfield")) CreateLandType<LandFieldType>(db.FieldTypes, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "planningapproved")) CreateLandType<LandPlanningApprovalStateTypes>(db.PlanningApprovalTypes, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "typeofplanning")) CreateLandType<LandPlanningType>(db.PlanningTypes, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "summarydetails")) CreateLandType<LandSummaryType>(db.SummaryTypes, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "status")) CreateLandType<LandStatusType>(db.StatusTypes, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "projectedpredominantunit")) CreateLandType<LandPlotType>(db.PlotTypes, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "priority")) CreateLandType<LandPriorityType>(db.PriorityTypes, s);
            foreach (object o in VTable_Base.SelectDistinct(srctbl, "probability")) CreateLandType<LandProbabilityType>(db.ProbabilityTypes, Convert.ToString(o));
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "typeofsite")) CreateLandType<LandProjectType>(db.ProjectTypes, s);
            foreach (string s in VTable_Base.SelectDistinct(srctbl, "currentuseofland")) CreateLandType<LandCurrentuseType>(db.CurrentUseTypes, s);

        }

        // Generic functions 
        private void CreateContactType<T>(DbSet<T> dblist, string aname) where T : Contact
        {
            string name = "NOT SPECIFIED";
            if (!string.IsNullOrEmpty(aname))
            {
                name = aname.ToUpper().Trim();
            }

            if (dblist.Count(t => t.Code == name) == 0)
            {
                var t = Activator.CreateInstance<T>();
                t.Code = name;
                DecodeRealName(name, t);
                t.SetCreatedInfo();
                dblist.Add(t);
                db.SaveChanges(); 
            }
        }

        private T LookupContactType<T>(DbSet<T> dblist, string aname) where T : Contact
        {
            string name = "NOT SPECIFIED";
            if (!string.IsNullOrEmpty(aname))
            {
                name = aname.ToUpper().Trim();
            }
            return dblist.First(s => s.Code == name);
        }

        private void CreateLandType<T>(DbSet<T> dblist, string aname, string acode, Int32 sortnum) where T : LandUnitType
        {
            string name = "NOT SPECIFIED";
            string code = "TBC";
            if (!string.IsNullOrEmpty(aname))
            {
                name = aname.ToUpper().Trim();
                code = acode.ToUpper().Trim();
            }

            if (dblist.Count(t => t.Name == name) == 0)
            {
                var t = Activator.CreateInstance<T>();
                t.Name = name;
                t.Code = code;
                t.SortOrder = sortnum; 
                t.SetCreatedInfo();
                dblist.Add(t);
                db.SaveChanges();
            }
        }

        private void CreateLandType<T>(DbSet<T> dblist, string aname, string acode) where T : LandUnitType
        {
            string name = "NOT SPECIFIED";
            string code = "TBC"; 
            if (!string.IsNullOrEmpty(aname))
            {
                name = aname.ToUpper().Trim();
                code = acode.ToUpper().Trim(); 
            }

            if (dblist.Count(t => t.Name == name) == 0)
            {
                var t = Activator.CreateInstance<T>();
                t.Name = name;
                t.Code = code; 
                t.SetCreatedInfo();
                dblist.Add(t);
                db.SaveChanges();
            }
        }

        private void CreateLandType<T>(DbSet<T> dblist, string aname) where T : LandUnitType
        {
            string name = "NOT SPECIFIED";
            if (!string.IsNullOrEmpty(aname))
            {
                name = aname.ToUpper().Trim();
            }

            if (dblist.Count(t => t.Name == name) == 0)
            {
                var t = Activator.CreateInstance<T>();
                t.Name = name;
                t.SetCreatedInfo();
                dblist.Add(t);
                db.SaveChanges(); 
            }
        }

        private T LookupLandType<T>(DbSet<T> dblist, string aname) where T : LandUnitType
        {
            string name = "NOT SPECIFIED";
            if (!string.IsNullOrEmpty(aname))
            {
                name = aname.ToUpper().Trim();
            }
            return dblist.First(s => s.Name == name);
        }

        private void CreateTown(string aname, string county, string postcode, string country)
        {
            string name = "NOT SPECIFIED";
            if (!String.IsNullOrEmpty(aname))
            {
                name = aname.ToUpper().Trim();
            };

            if (db.Towns.Count(t => t.Name == name) == 0)
            {
                Town t = new Town();
                t.Name = name;
                t.County = (string.IsNullOrEmpty(county) ? "" : county.ToUpper().Trim());
                t.Country = (string.IsNullOrEmpty(country) ? "UK" : country.ToUpper().Trim());
                t.Postcode = (string.IsNullOrEmpty(postcode) ? "" : postcode.ToUpper().Trim());
                t.Latitude = "0";
                t.Longditude = "0";
                t.SetCreatedInfo();
                db.Towns.Add(t);
                db.SaveChanges(); 
            }
        }

        private Town LookupTown(string aname)
        {
            string name = "NOT SPECIFIED";
            if (!String.IsNullOrEmpty(aname))
            {
                name = aname.ToUpper().Trim();
            };

            return db.Towns.First(s => s.Name == name);
        }

        #endregion

        #region Inline Decode Functions
        private void DecodeRealName(string n, Contact c)
        {
            if (string.IsNullOrEmpty(n)) return;

            string[] pNames = n.Split(' ');

            if (n.Contains("MR") || n.Contains("MRS") || n.Contains("MISS") || n.Contains("MS") || n.Contains("DR") || n.Contains("PROF"))
            {
                if (pNames.Length == 2)
                {
                    c.Prefix = pNames[0];
                    c.LastName = pNames[1];
                }

                if (pNames.Length == 3)
                {
                    c.Prefix = pNames[0];
                    c.FirstName = pNames[1];
                    c.LastName = pNames[2];
                }

                if (pNames.Length > 3)
                {
                    c.Prefix = pNames[0];
                    c.LastName = pNames[pNames.Length - 1];

                    string tmp = "";
                    for (int i = 1; i <= pNames.Length - 2; i++)
                    {
                        tmp += pNames[i] + " ";
                    }
                    c.FirstName = tmp.Trim();
                }
            }
            else
            {
                switch (pNames.Length)
                {
                    case 2: 
                        {
                            c.FirstName = pNames[0];
                            c.LastName = pNames[1];
                            c.OrganisationName = n; 
                            break;
                        }
                    case 3: 
                        {
                            c.FirstName = pNames[0];
                            c.MiddleName = pNames[1];
                            c.LastName = pNames[2];
                            c.OrganisationName = n;
                            break; 
                        }
                    case 1:
                    default:
                        {
                            c.OrganisationName = n;
                            c.LastName = n;
                            break;
                        } 
                }
            }
        }

        private DateTime Null2DefaultDate(DateTime? dte)
        {
            if (dte == null) return new DateTime(1980, 12, 31);

            return (DateTime)dte;
        }

        private string MapSubjecttoID(Int32 id)
        {
            switch (id)
            {
                case 1: return "STC";
                case 7: return "STC, PLANNING";
                case 11: return "STC, PLANNING, S&S";
                case 12: return "STC, S&S";
                case 13: return "STC, APPEAL";
                default: return "NONE";
            }
        }
        #endregion

        #region MapField to ValueType
        private String MapString(DataRow r, String fieldname)
        {
            if (!r.Table.Columns.Contains(fieldname)) return "";
            if (r.IsNull(fieldname)) return "";
            return Convert.ToString(r[fieldname]);
        }
        private DateTime? MapDate(DataRow r, String fieldname)
        {
            if (!r.Table.Columns.Contains(fieldname)) return null;
            if (r.IsNull(fieldname)) return null;

            if (String.IsNullOrEmpty(Convert.ToString(r[fieldname]))) return null;
            try
            {
                return Convert.ToDateTime(r[fieldname]);
            }
            catch (Exception ex)
            {
                log.Add(ex);
                log.Add(fieldname);
                log.Add(r["referenceid"]);
            }

            return null;
        }

        private Int32 MapInt32(DataRow r, String fieldname)
        {
            if (!r.Table.Columns.Contains(fieldname)) return -1;
            if (r.IsNull(fieldname)) return -1;
            if (String.IsNullOrEmpty(Convert.ToString(r[fieldname]))) return -1;
            try
            {
                return Convert.ToInt32(r[fieldname]);
            }
            catch (Exception ex)
            {
                log.Add(ex);
                log.Add(fieldname);
                log.Add(r["referenceid"]);
            }

            return -1;
        }

        private Boolean MapStrategic(DataRow r, String fieldname)
        {
            if (!r.Table.Columns.Contains(fieldname)) return false;
            if (r.IsNull(fieldname)) return false;
            // convert may need some help..
            string s = Convert.ToString(r[fieldname]).ToLower();
            if (String.IsNullOrEmpty(s)) return false;

            try
            {
                return (s.Contains("strategic") || s.Contains("y") || s.Contains("0"));
            }
            catch (Exception ex)
            {
                log.Add(ex);
                log.Add(fieldname);
                log.Add(r["referenceid"]);
            }

            return false;
        }

        private Boolean MapBoolean(DataRow r, String fieldname, bool defaultvalue)
        {
            if (!r.Table.Columns.Contains(fieldname)) return defaultvalue;
            if (r.IsNull(fieldname)) return defaultvalue;
            if (String.IsNullOrEmpty(Convert.ToString(r[fieldname]))) return defaultvalue;
            // convert may need some help..
            try
            {
                return Convert.ToBoolean(r[fieldname]);
            }
            catch (Exception ex)
            {
                log.Add(ex);
                log.Add(fieldname);
                log.Add(r["referenceid"]);
            }

            return defaultvalue;
        }

        private Double MapDouble(DataRow r, String fieldname, Double defaultvalue)
        {
            if (!r.Table.Columns.Contains(fieldname)) return defaultvalue;
            if (r.IsNull(fieldname)) return defaultvalue;
            if (String.IsNullOrEmpty(Convert.ToString(r[fieldname]))) return defaultvalue;

            // convert may need some help..
            try
            {
                return Convert.ToDouble(r[fieldname]);
            }
            catch (Exception ex)
            {
                log.Add(ex);
                log.Add(fieldname);
                log.Add(r["referenceid"]);
            }

            return defaultvalue;
        }

        private Decimal MapDecimal(DataRow r, String fieldname, Decimal defaultvalue)
        {
            if (!r.Table.Columns.Contains(fieldname)) return defaultvalue;
            if (r.IsNull(fieldname)) return defaultvalue;
            if (String.IsNullOrEmpty(Convert.ToString(r[fieldname]))) return defaultvalue;
            // convert may need some help..
            try
            {
                return Convert.ToDecimal(r[fieldname]);
            }
            catch (Exception ex)
            {
                log.Add(ex);
                log.Add(fieldname);
                log.Add(r["referenceid"]);
            }

            return defaultvalue;
        }
        #endregion

        #region MapRow to ComplexDataType
        private bool MapTownData(DataRow r, ref Town item)
        {            
            item.SetCreatedInfo();   

            String aval = MapString(r, "town");
            if (!String.IsNullOrEmpty(aval))
            {
                item.Name = aval.ToUpper().Trim();
            }
            else {
                return false; // town name cannot be null or "". 
            }
           
               
            aval = MapString(r, "county");
            if (!String.IsNullOrEmpty(aval))
            {
                item.County = aval.ToUpper().Trim(); 
            }

            aval = MapString(r, "country");
            if (!String.IsNullOrEmpty(aval))
            {
                item.Country = aval.ToUpper().Trim();
            }
            
            return true;
        }

        private bool MapRoleTypes(DataRow r, ref LandRoleType item)
        {
            item.SetCreatedInfo();
            item.Name = MapString(r, "type");

            return true;
        }

        private bool MapContactData(DataRow r, ref Contact item)
        {
            bool success = true;
            try
            {
                item.SetCreatedInfo();


                PropertyInfo[] pa = item.GetType().GetProperties();

                for (int i = 0; i <= pa.Length - 1; i++)
                {
                    if (pa[i].PropertyType.ToString() == "System.String")
                    {
                        var attribs = pa[i].GetCustomAttributes(typeof(Vtableimport));
                        foreach (Attribute a in attribs)
                        {
                            if (a is Vtableimport)
                            {
                                string s = MapString(r, ((Vtableimport)a).Fieldname);

                                pa[i].SetValue(item, s);
                            }
                        }
                    }
                }

                item.IsStrategic = MapStrategic(r, "IsStrategic");
                item.referenceid = MapInt32(r, "referenceid");
               

                item.Town = LookupTown(MapString(r, "town"));
                item.Type = LookupLandType<LandRoleType>(db.Roles, MapString(r, "type"));

                if (String.IsNullOrEmpty(item.Code))
                {
                    item.Code = Contact.GetCode(item);
                }

                if (String.IsNullOrEmpty(item.AddressCountry))
                {
                    item.AddressCountry = "UK"; 
                }   

            }
            catch (Exception ex)
            {
                log.Add(ex.Message);
                success = false;
            }
            finally
            {
            }

            return success;
        }
 
        private bool MapSiteData(DataRow r, ref Site site)
        {
            site.SiteCreatedDate = Null2DefaultDate(MapDate(r, "datereceived"));
            site.SetCreatedInfo();
            site.Name = MapString(r, "developmentname");
            site.AddressLineOne = MapString(r, "siteaddress1");
            site.AddressLineTwo = MapString(r, "address2");
            site.AddressLineThree = MapString(r, "address3");
            site.Town = LookupTown(MapString(r, "town"));

            string Ctry = MapString(r, "country");
            site.AddressCountry = string.IsNullOrEmpty(Ctry) ? "UK" : Ctry.ToUpper().Trim();
            site.AddressCounty = MapString(r, "county");
            site.AddressPostcode = MapString(r, "postcode");

            site.Negotiator = LookupContactType<Negotiator>(db.LandNegotiators, MapString(r, "buyerrwh")); //(Negotiator)LookupContactbyInitial(MapString(r,"buyerrwh"));
            site.Owner = LookupContactType<Owner>(db.LandOwners, MapString(r, "ownername"));
            site.Agent = LookupContactType<Agent>(db.LandAgents, MapString(r, "originindividual"));

            site.DeadFileBoxNumber = MapString(r, "deadfileboxnr");
            site.DeadFileNumber = MapString(r, "deadfilenr");


            site.LandFieldType = LookupLandType<LandFieldType>(db.FieldTypes, MapString(r, "brown_greenfield"));
            site.LandPriorityType = LookupLandType<LandPriorityType>(db.PriorityTypes, MapString(r, "priority"));
            site.LandProbabilityType = LookupLandType<LandProbabilityType>(db.ProbabilityTypes, MapString(r, "probability"));

            site.LandStatusType = LookupLandType<LandStatusType>(db.StatusTypes, MapString(r, "status")); // STC etc 
            site.LandProjectType = LookupLandType<LandProjectType>(db.ProjectTypes, MapString(r, "typeofsite"));  // Immediate/ Strategic           
            site.LandSummaryType = LookupLandType<LandSummaryType>(db.SummaryTypes, MapString(r, "summarydetails")); // 
            site.LandCurrentuseType = LookupLandType<LandCurrentuseType>(db.CurrentUseTypes, MapString(r, "currentuseofland"));
            fixSiteName(site);

            site.SiteValuations.Add(MapSiteValuationData(r));
            site.Plots.Add(MapSitePlotData(r));
            site.SiteExchanges.Add(MapSiteExchange(r));
            site.SiteSaleOptions.Add(MapSiteSaleOptionData(r)); 
            site.PlanningApplications.Add(MapSitePlanningApplication(r));                                  
            return true;
        }

        private SiteSaleOption MapSiteSaleOptionData(DataRow r)
        {

            SiteSaleOption sso = new SiteSaleOption();
            sso.SetCreatedInfo();
            sso.Description = "Expires:" + MapString(r, "optionexpirydate") + " Length:" + MapString(r, "lengthofoption");
            sso.Reference = MapString(r, "optioninvoicenumber");
            sso.OptionStartDate = MapDate(r, "optioninvoicedate");

            sso.OptionExpiryDate = null;
            sso.Duration = 0; // MapDouble(r, "lengthofoption", 0);
            sso.Premium = MapDecimal(r, "optionpremium", 0);
            sso.MVPercent = MapDecimal(r, "omvpercent", 0);

            sso.Fee = MapDecimal(r, "optionfee", 0);
            sso.Commission = MapDecimal(r, "optionpercentcommission", 0);

            sso.ContractLongStopDate = MapDate(r, "contractlongstop");
            sso.ProjectedLongStopDate = MapDate(r, "projcontractlongstop");
            return sso;
            //site.SiteSaleOptions.Add(sso);

        }

        private Plot MapSitePlotData(DataRow r)
        {

            Plot p = new Plot();
            p.SetCreatedInfo();
            p.plotcount = Convert.ToInt32(r["plots"]);
            p.LandPlotType = LookupLandType<LandPlotType>(db.PlotTypes, MapString(r, "projectedpredominantunit"));
            p.totalareasqm = MapDouble(r, "sqmetres", 0);
            p.IsSocialHousing = false;
            return p;
        }

        private SiteValuation MapSiteValuationData(DataRow r)
        {
            SiteValuation v = new SiteValuation();
            v.SetCreatedInfo();
            v.ProjectedGDV = MapDecimal(r, "projectedgdv", 0);
            v.FinalPurchasePrice = MapDecimal(r, "finalpurprice", 0);
            v.GuidePrice = MapDecimal(r, "guideprice", 0);
            v.RTMPercent = MapDecimal(r, "rtmpercent", 0);
            v.RTMCommission = MapDecimal(r, "rtmcommission", 0);
            v.RTM = MapBoolean(r, "righttomarket", true);
            v.ValuationDate = Null2DefaultDate(MapDate(r, "datereceived"));
            v.PurchaseDate = MapDate(r, "saledateagreed_stc");
            return v;
        }

        private PlanningApplication MapSitePlanningApplication(DataRow r)
        {
            ///Where should has planning go... on site record?
            ///should it be derived from a valid planning application.
            PlanningApplication pa = new PlanningApplication();
            pa.SetCreatedInfo();
            pa.ApplicationDate = Null2DefaultDate(MapDate(r, "planningappdate"));
            pa.ApplicationSubmitionDate = MapDate(r, "appsubmissiondate");
            pa.CommitteeMeetingDate = MapDate(r, "committeedate");
            pa.LandPlanningType = LookupLandType<LandPlanningType>(db.PlanningTypes, MapString(r, "typeofplanning"));

            return pa;
        }

        private PlanningApproval MapPlanningApproval(DataRow r)
        {

            PlanningApproval pap = new PlanningApproval();
            pap.SetCreatedInfo();
            pap.AppealDate = MapDate(r, "appealdate");

            pap.FullApprovalDate = Null2DefaultDate(MapDate(r, "fullapprovaldate"));
            //pap.RejectionDate = Lincore.DataTools.Utility.DateUtils.DDMMMCCYY2DTE(MapString(r,"appealdate"]));
            pap.ReferenceNumber = MapString(r, "planningnr");
            pap.OfficerRecommendation = MapString(r, "officerrecommendation");


            pap.ApprovedDate = MapDate(r, "planningappdate");
            string tmp = MapString(r, "planningapproved");

            if (string.IsNullOrEmpty(tmp))
            {
                pap.LandPlanningApprovalStateType = LookupLandType<LandPlanningApprovalStateTypes>(db.PlanningApprovalTypes, "NOT SPECIFIED");
            }
            else if (tmp.ToUpper().Trim() == "REJECTED")
            {
                pap.LandPlanningApprovalStateType = LookupLandType<LandPlanningApprovalStateTypes>(db.PlanningApprovalTypes, "REJECTED");
            }
            else
            {
                pap.LandPlanningApprovalStateType = LookupLandType<LandPlanningApprovalStateTypes>(db.PlanningApprovalTypes, "APPROVED");
            }

            return pap;
        }

        private SiteOffer MapSiteOffer(DataRow r)
        {
            SiteOffer so = new SiteOffer();
            so.SetCreatedInfo();
            //   so.Site = site;
            so.OfferDate = Null2DefaultDate(MapDate(r, "offerdate"));
            so.OfferAmount = MapDecimal(r, "offeramount", 0);
            so.OfferUnits = MapInt32(r, "numberofunits");
            so.OfferNotes = MapString(r, "notes");
            so.ConditionalType = LookupLandType<ConditionalType>(db.ConditionalTypes, MapInt32(r, "conditionalid") == 6 ? "CONDITIONAL" : "UNCONDITIONAL");
            so.OfferSubjecttoType = LookupLandType<OfferSubjecttoType>(db.OfferSubjecttoTypes, MapSubjecttoID(MapInt32(r, "subjecttodetails2_id")));

            so.OfferDecisionDate = MapDate(r, "acceptancedate");
            if (so.OfferDecisionDate == null || so.OfferDecisionDate >= new DateTime(1981, 1, 1))
            {
                so.DecissionType = LookupLandType<DecissionType>(db.DecissionTypes, "ACCEPTED");
            }
            else
            {
                so.DecissionType = LookupLandType<DecissionType>(db.DecissionTypes, "REJECTED");
            }

            return so;
        }

        private SiteExchange MapSiteExchange(DataRow r)
        {
            SiteExchange se = new SiteExchange();
            se.SetCreatedInfo();

            se.SaleStartDate = Null2DefaultDate(MapDate(r, "SaleStartDate"));
            se.AgreedSTCDate = Null2DefaultDate(MapDate(r, "saledateagreed_stc"));
            se.ProjectedExchangeDate = Null2DefaultDate(MapDate(r, "projex"));
            se.ProjectedCompletionDate = Null2DefaultDate(MapDate(r, "projcompl"));
            se.ActualExchangeDate = Null2DefaultDate(MapDate(r, "exchdate"));
            se.ActualCompletionDate = Null2DefaultDate(MapDate(r, "compldate"));
            se.SearchesSubmitedDate = Null2DefaultDate(MapDate(r, "searchessubmitted"));
            se.PreliminaryEnquiryDate = Null2DefaultDate(MapDate(r, "preliminaryenquiries"));
            se.DraftContractSubmitedDate = Null2DefaultDate(MapDate(r, "draftcontractsubmitted"));
            se.ProjectedReleaseDate = Null2DefaultDate(MapDate(r, "projrelease"));
            se.InstructionConfirmedOnDate = MapDate(r, "confirmationofinstdate");

            se.Commission = MapDouble(r, "commission", 0);
            se.NettFeePercent = MapDouble(r, "nettfeepercent", 0);
            se.ConditionsOfExchange = MapString(r, "conditions");

            //se.BrokerSolicitor = LookupContactType<BrokerSolicitor>(db.BrokerSolicitors,MapString(r, "landbuyer"));
            se.Buyer = LookupContactType<Buyer>(db.LandBuyer, MapString(r, "landbuyer"));
            se.VendorSolicitor = LookupContactType<VendorSolicitor>(db.VendorSolicitors, MapString(r, "vendorsolicitorname"));
            se.Purchaser = LookupContactType<Purchaser>(db.Purchasers, MapString(r, "purchasername"));
            se.PurchaserSolicitor = LookupContactType<PurchaserSolicitor>(db.PurchaserSolicitors, MapString(r, "purchasersolicitorname"));

            return se;
        }


       
        #endregion

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
                _connection.Dispose();
            }

        }




    }
}

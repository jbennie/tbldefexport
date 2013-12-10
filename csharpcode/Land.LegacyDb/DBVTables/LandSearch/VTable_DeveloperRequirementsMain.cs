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
    public class VTable_DeveloperRequirementsMain : VTable_Base
    {
        public const string VTableName = "[Developers' Requirements Main]";  // DEV

        public VTable_DeveloperRequirementsMain(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            TBL_Fields.Add(new VTable_FieldDef("[ID]", "System.Int32", "id", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Developer Organisation ID]", "System.Int32", "developerorganisationid", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Date]", "System.DateTime", "date", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Strategic Developer]", "System.Boolean", "strategicdeveloper", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Prefix]", "System.String", "prefix", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Initial]", "System.String", "initial", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[FirstName]", "System.String", "firstname", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[MiddleName]", "System.String", "middlename", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[LastName]", "System.String", "lastname", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[BNH Resales]", "System.Boolean", "bnhresales", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Date of statement]", "System.String", "dateofstatement", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[Notes]", "System.String", "notes", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[Particular Towns]", "System.String", "particulartowns", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[Particular Areas]", "System.String", "particularareas", false, 14));
            TBL_Fields.Add(new VTable_FieldDef("[Planning (w or w/o)]", "System.String", "planning(worw_o)", false, 15));
            TBL_Fields.Add(new VTable_FieldDef("[MIN GDV]", "System.String", "mingdv", false, 16));
            TBL_Fields.Add(new VTable_FieldDef("[MAX GDV]", "System.String", "maxgdv", false, 17));
            TBL_Fields.Add(new VTable_FieldDef("[MIN AREA (ACRES)]", "System.String", "minarea(acres)", false, 18));
            TBL_Fields.Add(new VTable_FieldDef("[MAX AREA (ACRES)]", "System.String", "maxarea(acres)", false, 19));
            TBL_Fields.Add(new VTable_FieldDef("[MIN PLOTS]", "System.Int32", "minplots", false, 20));
            TBL_Fields.Add(new VTable_FieldDef("[MAX PLOTS]", "System.Int32", "maxplots", false, 21));
            TBL_Fields.Add(new VTable_FieldDef("[Min Plot Range Low]", "System.Int32", "minplotrangelow", false, 22));
            TBL_Fields.Add(new VTable_FieldDef("[Min Plot Range High]", "System.Int32", "minplotrangehigh", false, 23));
            TBL_Fields.Add(new VTable_FieldDef("[Max Plot Range Low]", "System.Int32", "maxplotrangelow", false, 24));
            TBL_Fields.Add(new VTable_FieldDef("[Max Plot Range High]", "System.Int32", "maxplotrangehigh", false, 25));
            TBL_Fields.Add(new VTable_FieldDef("[Brown Field]", "System.Boolean", "brownfield", false, 26));
            TBL_Fields.Add(new VTable_FieldDef("[Green Field]", "System.Boolean", "greenfield", false, 27));
            TBL_Fields.Add(new VTable_FieldDef("[Residential]", "System.Boolean", "residential", false, 28));
            TBL_Fields.Add(new VTable_FieldDef("[Mix Use]", "System.Boolean", "mixuse", false, 29));
            TBL_Fields.Add(new VTable_FieldDef("[Affordable/Social]", "System.Boolean", "affordable_social", false, 30));
            TBL_Fields.Add(new VTable_FieldDef("[Luxury]", "System.Boolean", "luxury", false, 31));
            TBL_Fields.Add(new VTable_FieldDef("[RETIREMENT]", "System.Boolean", "retirement", false, 32));
            TBL_Fields.Add(new VTable_FieldDef("[Dealer]", "System.Boolean", "dealer", false, 33));
            TBL_Fields.Add(new VTable_FieldDef("[SINGLE PLOTS]", "System.Boolean", "singleplots", false, 34));
            TBL_Fields.Add(new VTable_FieldDef("[Agricultural Use]", "System.Boolean", "agriculturaluse", false, 35));
            TBL_Fields.Add(new VTable_FieldDef("[Conditional]", "System.Boolean", "conditional", false, 36));
            TBL_Fields.Add(new VTable_FieldDef("[Unconditional]", "System.Boolean", "unconditional", false, 37));
            TBL_Fields.Add(new VTable_FieldDef("[Option]", "System.Boolean", "option", false, 38));
            TBL_Fields.Add(new VTable_FieldDef("[Joint Venture/Partnership]", "System.Boolean", "jointventure_partnership", false, 39));
            TBL_Fields.Add(new VTable_FieldDef("[New Build]", "System.Boolean", "newbuild", false, 40));
            TBL_Fields.Add(new VTable_FieldDef("[Refurbishment]", "System.Boolean", "refurbishment", false, 41));
            TBL_Fields.Add(new VTable_FieldDef("[Conversions]", "System.Boolean", "conversions", false, 42));
            TBL_Fields.Add(new VTable_FieldDef("[Strategic]", "System.Boolean", "strategic", false, 43));
            TBL_Fields.Add(new VTable_FieldDef("['Specialty']", "System.String", "'specialty'", false, 44));
            TBL_Fields.Add(new VTable_FieldDef("[Budget]", "System.String", "budget", false, 45));
            TBL_Fields.Add(new VTable_FieldDef("[Development Type]", "System.String", "developmenttype", false, 46));
            TBL_Fields.Add(new VTable_FieldDef("[Updated]", "System.String", "updated", false, 47));
            TBL_Fields.Add(new VTable_FieldDef("[Old ID]", "System.Int32", "oldid", false, 48)); 

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

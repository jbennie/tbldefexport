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
    public class VTable_LandAgents : VTable_Base
    {
        public const string VTableName = "[Land Agents]";

        public VTable_LandAgents(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            TBL_Fields.Add(new VTable_FieldDef("[ID]", "System.Int32", "referenceid", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[Company Name]", "System.String", "orgainisationname", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Address 1]", "System.String", "addressline1", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Address 2]", "System.String", "addressline2", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[Town]", "System.String", "town", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[County]", "System.String", "county", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Post Code]", "System.String", "postcode", false, 7));
            TBL_Fields.Add(new VTable_FieldDef("[Tel]", "System.String", "worknumber", false, 8));
            TBL_Fields.Add(new VTable_FieldDef("[Email]", "System.String", "defaultemail", false, 9));
            TBL_Fields.Add(new VTable_FieldDef("[Web site]", "System.String", "website", false, 10));
            TBL_Fields.Add(new VTable_FieldDef("[Company type]", "System.String", "type", false, 11));
            TBL_Fields.Add(new VTable_FieldDef("[Turnover]", "System.Int32", "turnover", false, 12));
            TBL_Fields.Add(new VTable_FieldDef("[Staff]", "System.Int32", "staff", false, 13));
            TBL_Fields.Add(new VTable_FieldDef("[Services]", "System.String", "services", false, 14));

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
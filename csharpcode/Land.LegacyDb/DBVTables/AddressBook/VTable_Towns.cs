using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common; 
using Lincore.DataTools;
using Lincore.DataTools.vtable_common;

namespace Land.LegacyDb.Tables
{
    public class VTable_Towns  : VTable_Base
    {
        public const string VTableName = "[Town Details]";

        public VTable_Towns(IDatabaseConfig _DBConfig)
            : base(VTableName, false)
        {
            Setup(VTableName, _DBConfig, true);
        }

        public override void SetTBLFields()
        {
            // [Town Details]
            TBL_Fields.Add(new VTable_FieldDef("[Town]", "System.String", "town", false, 1));
            TBL_Fields.Add(new VTable_FieldDef("[County]", "System.String", "county", false, 2));
            TBL_Fields.Add(new VTable_FieldDef("[Country]", "System.String", "country", false, 3));
            TBL_Fields.Add(new VTable_FieldDef("[Local Planning Office]", "System.String", "localplanningoffice", false, 4));
            TBL_Fields.Add(new VTable_FieldDef("[District Planning Office]", "System.String", "districtplanningoffice", false, 5));
            TBL_Fields.Add(new VTable_FieldDef("[Local Planning Office Tel No]", "System.String", "localplanningofficetelno", false, 6));
            TBL_Fields.Add(new VTable_FieldDef("[Local Planning Office Website]", "System.String", "localplanningofficewebsite", false, 7));
            //reset col ID
            int col = 0;
            foreach (VTable_FieldDef d in TBL_Fields)
            {
                d.CSVPosition = col;
                col++;
                d.FName = d.FName.Replace("[", "").Replace("]", "");
            }
        }

    }
}

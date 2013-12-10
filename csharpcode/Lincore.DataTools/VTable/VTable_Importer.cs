using System;
using System.Collections; 
using System.Data; 
using System.Data.Common;
using Lincore.DataTools.Utility; 

namespace Lincore.DataTools.vtable_common
{
	/// <summary>
	/// Summary description for VTable_Importer.
	/// 
	/// Reads a DataSource and prepares info in the Merge format of the destination table. 
	/// 
	/// </summary>
	public class VTable_Importer
	{
		public VTable_Importer()
		{
		}
	
		private DataTable BuildImportTable(ArrayList ImportFieldList)
		{
			DataTable aTable =  new DataTable(); 
			aTable.Clear(); 
			aTable.Columns.Clear(); 
						
			foreach (VTable_FieldDef o in ImportFieldList)
			{
				if (o.IsImportField)
					aTable.Columns.Add(new DataColumn(o.FImportName, Type.GetType(o.FType)));	
			}	

			return aTable; 
		}

		/// <summary>
		/// Read in a , sepearated ascii file , assumes file is in the same format as the table 
		/// </summary>
		/// <param name="Filename"></param>
		public virtual DataTable LoadFromFile(ArrayList FieldDefs, string Filename)
		{
			DataTable ImporTable = BuildImportTable(FieldDefs); 
		
			CSVtoDataTable tmp = new CSVtoDataTable(ImporTable); 
			tmp.LoadfromFile(Filename,System.Text.Encoding.UTF8,true); 
			return ImporTable; 
		}
	}
}

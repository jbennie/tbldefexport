using System;
using System.Data; 
using System.Data.Common;

namespace Lincore.DataTools.Utility
{
	/// <summary>
	/// Summary description for DataTableHelper.
	/// </summary>
	public abstract class DataTableHelper
	{
 

		private static bool RowEqual(object[] Values, object[] OtherValues)
		{
			if(Values == null)
				return false;

			for(int i = 0; i < Values.Length; i++)
			{
				if(!Values[i].Equals(OtherValues[i]))
					return false;
			}                       
			return true;
		} 

		public static DataTable Distinct(DataTable Table, DataColumn[] Columns)
		{
			//Empty table
			DataTable table = new DataTable("Distinct");

			//Sort variable
			string sort = string.Empty;

			//Add Columns & Build Sort expression
			for(int i = 0; i < Columns.Length; i++)
			{
				table.Columns.Add(Columns[i].ColumnName,Columns[i].DataType);
				sort += Columns[i].ColumnName + ",";
			}

			//Select all rows and sort
			DataRow[] sortedrows = Table.Select(string.Empty,sort.Substring(0,sort.Length-1));

			object[] currentrow = null;
			object[] previousrow = null;

			table.BeginLoadData();
			foreach(DataRow row in sortedrows)
			{
				//Current row
				currentrow = new object[Columns.Length];
				for(int i = 0; i < Columns.Length; i++)
				{
					currentrow[i] = row[Columns[i].ColumnName];
				}

				//Match Current row to previous row
				if(!RowEqual(previousrow, currentrow))
					table.LoadDataRow(currentrow,true);

				//Previous row
				previousrow = new object[Columns.Length];
				for(int i = 0; i < Columns.Length; i++)
				{
					previousrow[i] = row[Columns[i].ColumnName];
				}
			}

			table.EndLoadData();
			return table; 
		}

		public static DataTable Distinct(DataTable Table, DataColumn Column)

		{

			return Distinct(Table, new DataColumn[]{Column});

		}

		public static DataTable Distinct(DataTable Table, string Column)

		{

			return Distinct(Table, Table.Columns[Column]);

		}

		public static DataTable Distinct(DataTable Table, params string[] Columns)

		{

			DataColumn[] columns = new DataColumn[Columns.Length];

			for(int i = 0; i < Columns.Length; i++)

			{

				columns[i] = Table.Columns[Columns[i]];

      

			}

			return Distinct(Table, columns);

		}

		public static DataTable Distinct(DataTable Table)
		{

			DataColumn[] columns = new DataColumn[Table.Columns.Count];
			for(int i = 0; i < Table.Columns.Count; i++)
			{
				columns[i] = Table.Columns[i];
			}

			return Distinct(Table, columns);

		}

	}
}

using System;
using System.Data; 
using System.Data.Common; 
using System.Collections;

namespace Lincore.DataTools.Utility
{
	/// <summary>
	/// Summary description for DataView2CSV.
	/// </summary>
	public class DataView2CSV
	{
		public DataView2CSV()
		{
		}

		private static string ConverttoCSV(String[] s, int count)
		{
			string sout = ""; 
			for (int i = 0; i <= count-1 ; i++)
			{
				if (sout != "") sout += ",";
				sout += s[i];
			}
			return sout; 
		}
		public static void ConverttoCSV(DataView dv, string filename, char sepChar)
		{			
			ConverttoCSV(dv, filename, sepChar ,System.Text.Encoding.UTF8); 
		}

		public static void ConverttoCSV(DataView dv, string filename, char sepChar, System.Text.Encoding enc)
		{
			DataTable dt = dv.Table; 
			System.IO.StreamWriter writer = new System.IO.StreamWriter(filename,false, enc);

			// first write a line with the columns name
			string sep  = ""; 
			bool firstcol = true; 

			System.Text.StringBuilder builder = new System.Text.StringBuilder(""); 
			foreach (DataColumn col in dt.Columns)
			{
				if (firstcol) 
				{
					builder.Append(col.ColumnName);
					firstcol  = false; 
				}
				else 
				{
					builder.Append(sep).Append(col.ColumnName);

				}
				sep = sepChar.ToString(); 
			}
			writer.WriteLine(builder.ToString()); 
			
			DataRow Row; 
			// then write all the rows
			for( int i = 0 ;  i <= dv.Count -1 ; i++)													
			{	
				Row = dv[i].Row; 
				sep = "";
				builder = new System.Text.StringBuilder("");

				firstcol = true;
				foreach (DataColumn col in dt.Columns)
				{
					if (firstcol) 
					{
						builder.Append(Row[col.ColumnName]);
						firstcol = false;
					} 
					else 
					{ 
						builder.Append(sep).Append(Row[col.ColumnName]);
					}
					sep = sepChar.ToString(); 
				}
				writer.WriteLine(builder.ToString()); 
			}

			writer.Flush();
			writer.Close(); 
		}


	}
}

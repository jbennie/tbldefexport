using System;
using System.Data; 
using System.Data.Common;

namespace Lincore.DataTools.Utility
{
	/// <summary>
	/// Summary description for DataTable2CSV.
	/// </summary>
	public abstract class DataTable2CSV
	{
		public DataTable2CSV()
		{
			//
			// TODO: Add constructor logic here
			//
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

		public static void ConverttoCSV(DataTable dt, string filename, char sepChar)
		{	  
			ConverttoCSV(dt, filename,sepChar, System.Text.Encoding.UTF8); 
		}

		public static void ConverttoCSV(DataTable dt, string filename, char sepChar, System.Text.Encoding enc)
		{
			System.IO.StreamWriter writer = new System.IO.StreamWriter(filename,false, enc);

			// first write a line with the columns name
			string sep  = ""; 
			bool firstcol = true; 

			System.Text.StringBuilder builder = new  System.Text.StringBuilder(""); 

			foreach (DataColumn col in dt.Columns)
			{									
				if (firstcol) 
				{
					builder.Append(col.ColumnName);
					firstcol = false; 
				}
				else 
				{
					builder.Append(sep).Append(col.ColumnName);
				}
				sep = sepChar.ToString(); 
			}
			writer.WriteLine(builder.ToString()); 

			// then write all the rows
			foreach (DataRow Row in dt.Rows)
			{
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

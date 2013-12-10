using System;
using System.Data; 
using System.Data.Common; 
using System.Collections; 
using System.Xml;
using System.IO; 

namespace Lincore.DataTools.Utility
{
	/// <summary>
	/// Summary description for DataTable2Excel.
	/// Version : 1.0.0 
	/// Date: 22nd Oct 2006 
	/// Author : Jay Bennie 
	/// </summary>
	public class DataTable2Excel
	{
		public DataTable2Excel()
		{
		}

		/// <summary>
		/// Implements a SpreadML Document - can be read by Excel 2000 and above. 
		/// </summary>
		/// <param name="aWriter"></param>
		public static void SaveAsSpreadML(string filename, DataSet ds)
		{
			try
			{
				if (System.IO.File.Exists(filename)) System.IO.File.Delete(filename); 

				XmlTextWriter aWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8 ); 
				aWriter.Formatting = Formatting.Indented; 
				ToSpreadML(aWriter, ds); 
				aWriter.Flush();
				aWriter.Close(); 
			}
			catch 
			{
			}

		}

        public static void HTTPWriterSpreadML(TextWriter datastream, DataTable dt)
        {
            DataSet ds = new DataSet("Wookbook1");
            ds.Tables.Add(dt);

            XmlTextWriter aWriter = new XmlTextWriter(datastream);   //datastream, System.Text.Encoding.UTF8);
            aWriter.Formatting = Formatting.Indented;
            ToSpreadML(aWriter, ds);
        }

        public static void StreamSpreadML(MemoryStream datastream, DataTable dt)
        {
            DataSet ds = new DataSet("Wookbook1");
            ds.Tables.Add(dt);

            XmlTextWriter aWriter = new XmlTextWriter(datastream, System.Text.Encoding.UTF8);
            aWriter.Formatting = Formatting.None;
            ToSpreadML(aWriter, ds);
        }

		/// <summary>
		/// Implements a SpreadML Document - can be read by Excel 2000 and above. 
		/// </summary>
		/// <param name="aWriter"></param>
		public static void SaveAsSpreadML(string filename, DataTable dt)
		{

			// createa temporary DataSet 
			DataSet ds = new DataSet("Wookbook1"); 
			ds.Tables.Add(dt); 

			try
			{
				if (System.IO.File.Exists(filename)) System.IO.File.Delete(filename); 

				XmlTextWriter aWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8 ); 
				aWriter.Formatting = Formatting.Indented; 
				ToSpreadML(aWriter, ds); 
				aWriter.Flush();
				aWriter.Close(); 
			}
			catch 
			{
			}

		}

		private static void ToSpreadML(XmlWriter aWriter, DataSet ds)
		{

			aWriter.WriteStartDocument(); 

			aWriter.WriteProcessingInstruction("mso-application", "progid=\"Excel.Sheet\""); 
			
			aWriter.WriteStartElement("Workbook"); 
			aWriter.WriteAttributeString("xmlns","",null , "urn:schemas-microsoft-com:office:spreadsheet");
			aWriter.WriteAttributeString("xmlns","o",null , "urn:schemas-microsoft-com:office:office");
			aWriter.WriteAttributeString("xmlns","x",null , "urn:schemas-microsoft-com:office:excel");
			aWriter.WriteAttributeString("xmlns","dt",null , "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882");
			aWriter.WriteAttributeString("xmlns","ss",null , "urn:schemas-microsoft-com:office:spreadsheet");
			aWriter.WriteAttributeString("xmlns","html",null , "http://www.w3.org/TR/REC-html40");



			aWriter.WriteStartElement("Styles"); 
			aWriter.WriteStartElement("Style"); 

			aWriter.WriteAttributeString("ss","ID",null, "ShortDT"); 
			
			aWriter.WriteStartElement("NumberFormat"); 
			aWriter.WriteAttributeString("ss", "Format" , null, "Short Date"); 
			aWriter.WriteEndElement(); 
			aWriter.WriteEndElement(); 

			aWriter.WriteEndElement(); 
					

			foreach(DataTable tbl in ds.Tables)
			{
				ToSpreadML(aWriter, tbl, true);
			}

			aWriter.WriteEndDocument(); 
		}


		private static string XLIndex(int i, bool header)
		{ 
		
			int a = i +1; 
			if (header) a++; // add 2 rather than 1 
			return a.ToString(); 
		}

		private static string XLIndex(int i)
		{ 
			int a = i +1; 
			return a.ToString(); 
		}


		private static void ToSpreadML(XmlWriter aWriter, DataTable aTable, bool colheader)
		{
			int rowindex = 0; 
			// writer the contents of this Grid as a SpreadML 

			aWriter.WriteStartElement("Worksheet"); 
			aWriter.WriteAttributeString("ss","Name", null, aTable.TableName); 	
			
			aWriter.WriteStartElement("Table"); 

			if (colheader)
			{

				aWriter.WriteStartElement("Row"); 
				aWriter.WriteAttributeString("ss", "Index" , null, XLIndex(rowindex, false)); 

				ToSpreadML(aWriter, aTable.Columns); 
				aWriter.WriteEndElement();	

			}

			
			for (rowindex = 0; rowindex <= aTable.Rows.Count -1; rowindex++)
			{
				aWriter.WriteStartElement("Row"); 

				if (rowindex >= 0)
				{
					//if (IsRowNull(rowindex -1))
					aWriter.WriteAttributeString("ss", "Index" , null, XLIndex(rowindex, colheader)); 
				}

				ToSpreadML(aWriter, aTable.Rows[rowindex]); 
				aWriter.WriteEndElement();	
			}
			aWriter.WriteEndElement(); // end table 

			aWriter.WriteEndElement();

		}

		private static void ToSpreadML(XmlWriter aWriter, DataRow aRow )
		{

			for (int colindex = 0; colindex <= aRow.Table.Columns.Count -1; colindex++)
			{
			//	if (!aRow.IsNull(colindex))
				//{
					aWriter.WriteStartElement("Cell"); 

					if (colindex >= 0)
					{
						if (aRow.IsNull(colindex))
						{
							aWriter.WriteAttributeString("ss", "Index" , null, XLIndex(colindex)); 
						}
					}

				if (!aRow.IsNull(colindex))
				{

					switch (aRow.Table.Columns[colindex].DataType.FullName.ToLower())
					{
						case "system.decimal": 
						case "system.double":
						case "system.int":
						case "system.int16":
						case "system.int32":
						case "system.int64":
						{
							if (  aRow[colindex].ToString() != "NaN")
							{
								aWriter.WriteStartElement("Data"); 
								aWriter.WriteAttributeString("ss", "Type", null, "Number"); 
								aWriter.WriteString( aRow[colindex].ToString()); 
								aWriter.WriteEndElement();
							}
							else 
							{
								aWriter.WriteStartElement("Data"); 
								aWriter.WriteAttributeString("ss", "Type", null, "String"); 
								aWriter.WriteString( aRow[colindex].ToString()); 
								aWriter.WriteEndElement();
							}
							break;
						}
						case "system.datetime": 
						{
							aWriter.WriteAttributeString("ss", "StyleID", null, "ShortDT"); 

							aWriter.WriteStartElement("Data"); 
							aWriter.WriteAttributeString("ss", "Type", null, "DateTime"); 
							aWriter.WriteString( ((DateTime)aRow[colindex]).ToString("s")); 
							aWriter.WriteEndElement();
							break; 
						}
											
						default :
						{
							aWriter.WriteStartElement("Data"); 
							aWriter.WriteAttributeString("ss", "Type", null, "String"); 
							aWriter.WriteString( aRow[colindex].ToString()); 
							aWriter.WriteEndElement();
							break;
						}
					}
				} 
				else 
				{
					aWriter.WriteStartElement("Data"); 
					aWriter.WriteAttributeString("ss", "Type", null, "String"); 
					aWriter.WriteString(""); 
					aWriter.WriteEndElement();
				}

					aWriter.WriteEndElement(); // end cell 
			//	}
			}
		}


		private static void ToSpreadML(XmlWriter aWriter, DataColumnCollection headers )
		{

			for (int colindex = 0; colindex <= headers.Count -1; colindex++)
			{
				aWriter.WriteStartElement("Cell"); 

				if (colindex == 0)
				{
					aWriter.WriteAttributeString("ss", "Index" , null, XLIndex(colindex)); 
				}
			
				aWriter.WriteStartElement("Data"); 
				aWriter.WriteAttributeString("ss", "Type", null, "String"); 
				aWriter.WriteString( headers[colindex].ColumnName); 
				aWriter.WriteEndElement();

				aWriter.WriteEndElement(); // end cell 
			}
		}
	}
}

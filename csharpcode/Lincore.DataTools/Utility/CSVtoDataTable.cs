using System;
using System.IO; 
using System.Collections; 
using System.Data; 
using System.Data.Common;

namespace Lincore.DataTools.Utility
{	 	
	
	public class CSVtoDataTable
	{



		/// <summary>
		/// Import a csv file to a simple string based datatable
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="incHeader"></param>
		/// <param name="enc"></param>
		/// <returns></returns>
		public static DataTable CovertCSVtoDataTable(string filename, System.Text.Encoding enc, bool incHeader)
		{
	
			DataTable aTable = new DataTable(System.IO.Path.GetFileNameWithoutExtension(filename).Replace(" ", "_")); 

			for(int i = 0 ; i <= 150; i++)
			{
				aTable.Columns.Add(new DataColumn("Field"+i.ToString(), Type.GetType("System.String"))); 
			}
			
			CSVtoDataTable tmp = new CSVtoDataTable(aTable); 
			tmp.LoadfromFile(filename,enc,incHeader); 

			return aTable; 
		}


		/*
				 * This class is designed to read a CSV file Line by line 
				 * the PMS "" extract and insert the data into the LNPRS DataBase. 
				 * 
				 * */

		private DataTable _Table;
        public char Delimiter = '|'; 

		public CSVtoDataTable(DataTable aTable)
		{
			_Table = aTable; 
		}

		
		#region FileLoader 
	

		public void LoadfromFile(string filename)
		{
			LoadfromFile (filename, System.Text.Encoding.Unicode, false); 
		}
		public void LoadfromFile(string filename, System.Text.Encoding an_encoding, bool setheadertext)
		{
			System.IO.FileStream afile = new FileStream(filename, FileMode.Open,FileAccess.Read);
			System.IO.BufferedStream bstream = new BufferedStream(afile);
			System.IO.StreamReader astream = new StreamReader(bstream,an_encoding);
			
			string buffer = "";
				  
			try 
			{	
				
				if (setheadertext)			
				{   
					// read in the headers. - one first line. 
					buffer = astream.ReadLine();  				
					Line2Header(ParseCSVLine(buffer)); 
				}
			
				string[] datarow;
				do 
				{
					buffer = astream.ReadLine();
					if (buffer != null)
					{
						if (buffer != "")
						{
							datarow =  ParseCSVLine( buffer);
							// do work here - potentially put an event to delelegate the action. 

							Line2DataRow(datarow); 	
						}

					}

				}while (buffer != null);
			}

				
		//	catch (Exception ex)
		//	{
		//		throw new Exception("Error Parsing CSV File [" + filename +"] - " + ex.Message + " buffer =  " + buffer);
		//	}

			finally 
			{
				astream.Close();
				bstream.Close();
				afile.Close();
			}
			
		}


		private enum Ctrls {addChar, skipChar, delimit };	
		private string[] ParseCSVLine(string Buffer)
		{
			// i know that in this instance there are only 36 attributes per row 
			ArrayList aRow = new ArrayList();

			bool inQuotes = false; 
			
			int i = 0;
			string FieldValue = "";

			Ctrls Ctrl = Ctrls.addChar;

			do 
			{
				switch (Buffer[i])
				{
					case '"' : 
					{
						inQuotes = !inQuotes;
						Ctrl = Ctrls.skipChar; 
						break; 
					}
					case '|' : 
					{
						if (inQuotes) 
							Ctrl = Ctrls.addChar;
						else 
							Ctrl = Ctrls.delimit;

						break;
					}                   
					default : Ctrl = Ctrls.addChar; break;
				}

				switch (Ctrl)
				{
					case Ctrls.addChar : 
					{ 
						FieldValue += Buffer[i];
						break;
					}
					case Ctrls.delimit: 
					{ 
						aRow.Add(FieldValue);
						FieldValue = "";
						break;
					}

					case Ctrls.skipChar :
					default :  // nothing to do 
						break;
				}

				i++;
			}while ( i < Buffer.Length);
			// add the last item to the array. 

			aRow.Add(FieldValue);

			string[] tmp = new string[aRow.Count];
			aRow.CopyTo(tmp);
			return tmp;
		}


		private bool _delspare = true; 
		private void Line2DataRow(string[] aLine)
		{
			if (_delspare) 
			if (aLine.Length <= _Table.Columns.Count) 
			{
				for (int i = _Table.Columns.Count-1; i >= aLine.Length ; i--) 
				{
					_Table.Columns.Remove(_Table.Columns[i]); 
				}
				
				_delspare = false; 
			}
			

			DataRow aRow = _Table.NewRow(); 
			for( int i =0 ; i <= _Table.Columns.Count -1; i++)
			{
				if (i <= aLine.Length -1)
				aRow[i] = (aLine[i]); 
			}
			_Table.Rows.Add(aRow); 

		}	

		private void Line2Header(string[] aLine)
		{
			for (int i =0; i <= aLine.Length -1; i++)
			{
                string tmp = aLine[i].ToString().Trim();
                if (tmp.Contains(":"))
                {                    
                    _Table.Columns[i].ColumnName = aLine[i].ToString().Trim().Replace("[", "").Replace("]", "").Split(':')[2];
                }
                else
                {
                    _Table.Columns[i].ColumnName = tmp;                    
                }
			}
			// remove surplus columns 
			int j = _Table.Columns.Count -1; 
			while (_Table.Columns.Count > aLine.Length)
			{
				_Table.Columns.Remove("Field"+ Convert.ToString(j)); 
				j--;
			}
			 
		
		
		}
		
		#endregion

	}
}

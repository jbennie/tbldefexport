using System;
using System.Collections;

namespace Lincore.DataTools.vtable_common
{
	public class VTable_FieldDef
	{
		public VTable_FieldDef(string name , string type)
		{
			FName = name; 
			FType = type; 
			FImportName = name;
		}

		public VTable_FieldDef(string name , string type, bool IsKey)
		{
			FName = name; 
			FType = type; 
			FImportName = name;
			IsKeyField = IsKey; 
		}

		public VTable_FieldDef(string name , string type, Int32 Pos)
		{
			FName = name; 
			FType = type; 
			FImportName = name;
			CSVPosition = Pos; 
		}

		public VTable_FieldDef(string name , string type, bool IsKey, Int32 Pos)
		{
			FName = name; 
			FType = type; 
			FImportName = name;
			IsKeyField = IsKey; 
			CSVPosition = Pos; 
		}
		
		/// <summary>
		/// Define the Field names Dynamic Query and table building 
		/// Import and Merge can be supported via FImportName Filed
		/// Import reads in a File with the FimportNames 
		/// Merge Translates from FImportName to FName 
		/// </summary>
		/// <param name="name">Attribute Name</param>
		/// <param name="type"></param>
		/// <param name="importname">by default = Attribute name , if empty string , not importable , otherwise is alt ImportField name </param>
		public VTable_FieldDef(string name , string type, string importname)
		{
			FName = name; 
			FType = type; 
			FImportName = importname; 
		}

		public VTable_FieldDef(string name , string type, string importname, bool IsKey)
		{
			FName = name; 
			FType = type; 
			FImportName = importname; 
			IsKeyField = IsKey; 
		}	

		public VTable_FieldDef(string name , string type, string importname, Int32 Pos)
		{
			FName = name; 
			FType = type; 
			FImportName = importname; 
			CSVPosition = Pos; 
		}

		public VTable_FieldDef(string name , string type, string importname, bool IsKey, Int32 Pos)
		{
			FName = name; 
			FType = type; 
			FImportName = importname; 
			IsKeyField = IsKey; 
			CSVPosition = Pos; 
		}	

		public string FName =""; 
		public string FType = ""; 
		public string FImportName = ""; 		 
		public bool IsImportField { get { return FImportName.Length >= 1; }}
		public bool IsKeyField = false; 

		public Int32 CSVPosition = -1;  // -1 is not set // values >= 0  within range are set.  

		public static string GetSQLFields(ArrayList List)
		{
			string query = ""; 
			bool first = true; 

			foreach(VTable_FieldDef o in List)
			{
				if (first)
				{
					query += o.FName;
					first = false; 
				}
				else 
				{
					query += " , " + o.FName;
				}
			}

			return query; 
		}

		public static string GetImportSQLFields(ArrayList List)
		{
			string query = ""; 
			bool first = true; 

			foreach(VTable_FieldDef o in List)
			{
				if (o.IsImportField)
				{
					if (first)
					{
						query += o.FImportName;
						first = false; 
					}
					else 
					{
						query += " , " + o.FImportName;
					}
				}
			}

			return query; 
		}

        /// <summary>
        /// Builds a simple select query Select {Fields} from {FromTable}
        /// </summary>
        /// <param name="Fields"></param>
        /// <param name="From"></param>
        /// <returns></returns>
        public static string SelectFrom(string Fields, string FromTable)
        {
            return "Select " + Fields + " from " + FromTable;
        }

        public override string ToString() { return FName; }

        public string ToHeaderDef() { 
        //    return "pos=[" + CSVPosition + "]:type=[" + FType + "]:displayname=[" + FName + "]:fieldname=[" + FImportName + "]"; 
            
            return "[" + CSVPosition + ":" + FType + ":" + FImportName + "]"; 

        }

		/*
		public static string BuildFilterArg(VTable_FieldDef o)
		{
			string FilterArg = ""; 
			switch (o.FType)
			{
				case "System.String":
				{
					FilterArg +=  o.FArgName + " = '" + o.FArg + "' "; 
					break;
				}

				case "System.Double":
				case "System.Int32":
				{
					FilterArg +=  o.FArgName + " = " + o.FArg.ToString() + " "; 
					break;
				}

				case "System.DateTime":
				{
					FilterArg +=  o.FArgName + " = #" + ((DateTime)o.FArg).ToLongDateString() + "# "; 
					break;
				}
			}

			return FilterArg; 
		}

		
		public static string GetKeyWhereFields(ArrayList List)
		{
			string query = ""; 
			bool first = true; 

			foreach(VTable_FieldDef o in List)
			{
				if (first)
				{
					query +=  BuildFilterArg(o);
					first = false; 
				}
				else 
				{
					query += " , " + BuildFilterArg(o);
				}
			}

			return query; 
		}
		*/
		
	
	}
	
}

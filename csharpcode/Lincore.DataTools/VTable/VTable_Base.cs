using System;
using System.Data; 
using System.Data.Common;
using System.Collections;
using System.IO; 
using Lincore.DataTools.EncryptionLib;
using Lincore.DataTools.Utility; 
using Lincore.DataTools.DBHandler;   

using Lincore.SimpleEvent; 

/* 
 * Virtual DB Lib superceeds Classes developed for whittingdale db 
 * This Library Contains Methods to Allow Easy trasition between db platforms
 * 
 * Author: Joseph Bennie
 * Date: 8th Feb 2007 
 * */

namespace Lincore.DataTools.vtable_common
{
	/// <summary>
	/// Summary description for VTable_Base.
	/// an abstract database access Class 
	/// 
	/// </summary>
	public class VTable_Base
	{


		public delegate void VTableLogErrorHandler(object s, RptEventArgs e); 
		public event VTableLogErrorHandler OnLogError; 

		protected string _SQLTableName = "";
   
		protected IDbDataAdapter _ADP;
		protected IDbConnection _DBConn;
        protected IDatabaseConfig _DatabaseConfig;

	
		public ArrayList TBL_Fields; 
		public ArrayList VC_Fields; 
			
		public DataTable Table; 
		public DataView CheckView; 

		public object[] findTheseVals;
		public DataColumn[] PKeys; 
		public string SESSION_USERNAME = "Unknown"; 

		public EncryptionLib.MD5Encrypt MD5Engine; 
		public ArrayList MD5KeyFields ; 
		
		private bool _EnableVC = false;
        private bool _EnableFieldDelimiter = true;
        private char _BeginDelimiterChar = ']';
        private char _EndDelimiterChar = '['; 

		public VTable_Base(string aTablename)
		{
			
			Table = new DataTable(aTablename); 	
			TBL_Fields = new ArrayList();
			SetTBLFields(); 
					
			VC_Fields = new ArrayList(); 
			if (_EnableVC) 
			{	
				SESSION_USERNAME =  System.Security.Principal.WindowsIdentity.GetCurrent().Name; 
				MD5Engine = new MD5Encrypt("mySaltValue"); 
				dteBlockVCChange =  DateTime.Now; 
				MD5KeyFields = new ArrayList(); 
				SetVCFields();
			}
		}

		public VTable_Base(string aTablename,bool bEnableVC )
		{
			Table = new DataTable(aTablename); 	
			TBL_Fields = new ArrayList();
			SetTBLFields(); 
			
			_EnableVC = bEnableVC; 	
            VC_Fields = new ArrayList(); 

            if (_EnableVC) 
			{					                				
				SESSION_USERNAME =  System.Security.Principal.WindowsIdentity.GetCurrent().Name; 
				MD5Engine = new MD5Encrypt("mySaltValue"); 
				dteBlockVCChange =  DateTime.Now; 
				MD5KeyFields = new ArrayList(); 
				SetVCFields();
			}
		}

        public VTable_Base(string aTablename, bool bEnableVC, bool bEnableFieldDelimiter)
        {
            Table = new DataTable(aTablename);
            TBL_Fields = new ArrayList();
            SetTBLFields();

            _EnableVC = bEnableVC;
            _EnableFieldDelimiter = bEnableFieldDelimiter; 
            VC_Fields = new ArrayList();

            if (_EnableVC)
            {
                SESSION_USERNAME = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                MD5Engine = new MD5Encrypt("mySaltValue");
                dteBlockVCChange = DateTime.Now;
                MD5KeyFields = new ArrayList();
                SetVCFields();
            }
        }


		public virtual void SetTBLFields(){}
		private void SetVCFields()
		{
			VC_Fields.Add( new VTable_FieldDef("VC_Create","System.String", "", true)); 
			VC_Fields.Add( new VTable_FieldDef("VC_State","System.String", "")); 			
			VC_Fields.Add( new VTable_FieldDef("VC_Approve","System.String", "") ); 
			VC_Fields.Add( new VTable_FieldDef("VC_Reject","System.String", "") ); 			
			VC_Fields.Add( new VTable_FieldDef("VC_Checksum","System.String", "") );
		}

        /// <summary>
        /// Define datatable structure.
        /// </summary>
        /// <param name="SqlTableName"></param>
        /// <param name="aDatabaseConfig"></param>
        /// <param name="bEnableFieldDelimiter"></param>
		public void Setup(string SqlTableName, IDatabaseConfig aDatabaseConfig, bool bEnableFieldDelimiter)
		{
			_SQLTableName = SqlTableName;
            _DatabaseConfig = aDatabaseConfig; 
			_DBConn = _DatabaseConfig.GetHandler().getDataConnection();
            _EnableFieldDelimiter = bEnableFieldDelimiter; 
            _BeginDelimiterChar = aDatabaseConfig.GetHandler().getFieldBeginDelimiter();
            _EndDelimiterChar = aDatabaseConfig.GetHandler().getFieldEndDelimiter(); 


            if (_EnableVC)
            {
                _ADP = _DatabaseConfig.GetHandler().getDataAdapter("Select " + GetSQLFields() + " from " + _SQLTableName + " where VC_State = 'Approved'", _DBConn);
            }
            else
            {
                _ADP = _DatabaseConfig.GetHandler().getDataAdapter("Select " + GetSQLFields() + " from " + _SQLTableName, _DBConn);
            
            }
                //new SqlDataAdapter("Select "+ GetSQLFields() +" from "+_SQLTableName+" where VC_State = 'Approved'", _DBConn); 

			// Create Own Update, insert and Delete commands 
			//_Builder = new SqlCommandBuilder(_ADP); 
			// replace the update command
			_ADP.UpdateCommand = BuildUpdate(_DBConn); 
			_ADP.DeleteCommand = BuildDelete(_DBConn);
			_ADP.InsertCommand = BuildInsert(_DBConn);
		}


        private IDbCommand BuildUpdate(IDbConnection DBConn)
		{

            IDbCommand cmd = _DatabaseConfig.GetHandler().getDataCommand(GetUpdateSQL(), DBConn);   // new SqlCommand(GetUpdateSQL(), DBConn); 		
			
			#region Set mappings for update able fields 
			foreach(VTable_FieldDef o in TBL_Fields)
			{				
				if (!o.IsKeyField)
				{

                    cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@" + o.FName, Type.GetType(o.FType))); 
					((IDataParameter)cmd.Parameters["@"+o.FName ]).SourceColumn = o.FName;
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceVersion = DataRowVersion.Default;			
				}
			}

            if(_EnableVC)
			foreach(VTable_FieldDef o in VC_Fields)
			{				
				if (!o.IsKeyField)
				{
                    cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@" + o.FName, Type.GetType(o.FType))); 
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceColumn = o.FName;
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceVersion = DataRowVersion.Default;			
				}
			}
			#endregion 

			#region set mappings for Key Fields

			foreach(VTable_FieldDef o in TBL_Fields)
			{				
				if (o.IsKeyField)
				{
                    cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@" + o.FName, Type.GetType(o.FType))); 
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceColumn = o.FName;
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceVersion = DataRowVersion.Original;
				}
			}

            if(_EnableVC)         
			foreach(VTable_FieldDef o in VC_Fields)
			{				
				if (o.IsKeyField)
				{
                    cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@" + o.FName, Type.GetType(o.FType))); 
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceColumn = o.FName;
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceVersion = DataRowVersion.Original;
				}
			}
			#endregion 

			return cmd; 
		}

		private IDbCommand BuildInsert(IDbConnection DBConn)
		{

            IDbCommand cmd = _DatabaseConfig.GetHandler().getDataCommand(GetInsertSQL(), DBConn); 
                //new DBCommand(GetInsertSQL(), DBConn); 		
			
			#region Set mappings for update able fields 
			foreach(VTable_FieldDef o in TBL_Fields)
			{
                cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@" + o.FName, Type.GetType(o.FType))); 
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceColumn = o.FName;
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceVersion = DataRowVersion.Default;				
			}

			if (_EnableVC)
			foreach(VTable_FieldDef o in VC_Fields)
			{
                cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@" + o.FName, Type.GetType(o.FType))); 
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceColumn = o.FName;
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceVersion = DataRowVersion.Default;			
			}
			#endregion 

			return cmd; 
		}

		private IDbCommand BuildDelete(IDbConnection DBConn)
		{
            IDbCommand cmd = _DatabaseConfig.GetHandler().getDataCommand(GetDeleteSQL(), DBConn); 		
			
			#region Set mappings for Key fields 
			foreach(VTable_FieldDef o in TBL_Fields)
			{		
				if (o.IsKeyField)
				{
                    cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@" + o.FName, Type.GetType(o.FType))); 
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceColumn = o.FName;
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceVersion = DataRowVersion.Original;				
				}
			}

			if (_EnableVC)
			foreach(VTable_FieldDef o in VC_Fields)
			{
				if (o.IsKeyField)
				{
                    cmd.Parameters.Add(_DatabaseConfig.GetHandler().getDataParameter("@" + o.FName, Type.GetType(o.FType))); 
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceColumn = o.FName;
					((IDataParameter)cmd.Parameters["@"+o.FName]).SourceVersion = DataRowVersion.Original;	
				}
			}
			#endregion 

			return cmd; 
		}



		public virtual void DefineTable(DataTable aTable)
		{
			
			aTable.Clear(); 
			aTable.Columns.Clear(); 
						
			foreach (VTable_FieldDef o in TBL_Fields)
			{
                try
                {
                    aTable.Columns.Add(new DataColumn(o.FName, Type.GetType(o.FType)));
                   
                }
                catch (Exception ex)
                {
                    if (OnLogError != null) OnLogError(this, new RptEventArgs( this.GetSQLTableName() + " "+ o.FName +"<"+ o.FType +"> :"+ex.Message));
                }

			}	

			if (_EnableVC)
			{
				foreach (VTable_FieldDef o in VC_Fields)
				{
					aTable.Columns.Add(new DataColumn(o.FName, Type.GetType(o.FType)));	
				}	
								
				aTable.Columns["VC_Create"].AllowDBNull = false; 
				aTable.Columns["VC_Create"].DefaultValue = ""; 

				aTable.Columns["VC_State"].AllowDBNull = false; 
				aTable.Columns["VC_State"].DefaultValue = "Created";
			}
		
			SetPrimaryKeys(aTable); 

			// Setup Default View; 
			CheckView = new DataView(aTable);
		}

		private void SetPrimaryKeys(DataTable aTable)
		{			 
			ArrayList Keys = GetKeyFields(); 
			findTheseVals = new object[Keys.Count];
			DataColumn[] PrimaryKeyColumns = new DataColumn[Keys.Count];

			for(int i = 0 ; i <= Keys.Count -1 ; i++)
			{
				PrimaryKeyColumns[i] = aTable.Columns[((VTable_FieldDef)Keys[i]).FName];
			}
			aTable.PrimaryKey = PrimaryKeyColumns;			
		}

		/// <summary>
		/// The input table for MergeTabletoMaster
		/// </summary>
		/// <param name="aTable"></param>
	


		#region Use of these calls invalidates the Checksum 

		/*
		 * The Intention is to use these calls only for Special operations, such as creating a clean db during initial setup. 
		 * Or where there is unexpected behaviour using the merg function in Version control. 
		 * A better solution is to use the function to copy the items markd to a histroy table. 
		 * */

		/// <summary>
		/// DeleteALL_DirectSQL should only be used where you do want to clear all the version control data. 
		/// Override as required 
		/// </summary>
		public void DeleteAll_DirectSQL()
		{
			try
			{
				_DBConn.Close(); 
				_DBConn.Open();

                IDbCommand cmd = _DatabaseConfig.GetHandler().getDataCommand("Delete from " + GetSQLTableName(), _DBConn); 
				cmd.ExecuteNonQuery(); 
			}
			catch(Exception EX)
			{
				Logger.LogWriter.Write(EX.ToString()); 
			}
			finally
			{
				_DBConn.Close(); 
			}
		}

		public void DeleteAll_DirectSQL(IDbCommand cmd)
		{
			try
			{
				cmd.Connection = _DBConn; 

				_DBConn.Close(); 
				_DBConn.Open(); 
 
				
				cmd.ExecuteNonQuery(); 

			}
			catch(Exception EX)
			{
				Logger.LogWriter.Write(EX.ToString()); 
			}
			finally
			{
				_DBConn.Close(); 
			}
		}

		/// <summary>
		/// Deletes all rejected items in a table.  
		/// </summary>
		public void DeleteRejected_DirectSQL()
		{
            if (_EnableVC)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    _DBConn.Close();
                    _DBConn.Open();

                    IDbCommand cmd = _DatabaseConfig.GetHandler().getDataCommand("Delete from " + GetSQLTableName() + " where VC_State = 'Rejected'", _DBConn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception EX)
                {
                    Logger.LogWriter.Write(EX.ToString());
                }
                finally
                {
                    _DBConn.Close();
                }
            }
		}

		/// <summary>
		/// Sets all items marked as approved to rejected 
		/// used with caution - create table specific items
		/// </summary>
		public void RejectApproved_DirectSQL()
        {
            if (_EnableVC)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    _DBConn.Close();
                    _DBConn.Open();

                    IDbCommand cmd = _DatabaseConfig.GetHandler().getDataCommand("Update " + GetSQLTableName() + " set VC_State  = 'Rejected', VC_Rejected = '" + GetTSU_Stamp(dt) + "' where VC_State = 'Approved'", _DBConn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception EX)
                {
                    Logger.LogWriter.Write(EX.ToString());
                }
                finally
                {
                    _DBConn.Close();
                }
            }
		}

		#endregion 



		public void Fill()
		{
			try
			{
				_DBConn.Close(); 
				_DBConn.Open(); 
				
				DefineTable(Table); 

                if (_ADP is MySql.Data.MySqlClient.MySqlDataAdapter)
                    ((MySql.Data.MySqlClient.MySqlDataAdapter)_ADP).Fill(Table);

                if (_ADP is System.Data.SqlClient.SqlDataAdapter)
                    ((System.Data.SqlClient.SqlDataAdapter)_ADP).Fill(Table);

                if (_ADP is System.Data.OleDb.OleDbDataAdapter)
                    ((System.Data.OleDb.OleDbDataAdapter)_ADP).Fill(Table); 

                System.Data.SqlClient.SqlDataAdapter tmp = new System.Data.SqlClient.SqlDataAdapter(); 
			}
			catch(Exception Ex)
			{
                if (OnLogError != null) {
                
                    OnLogError(this, new RptEventArgs(Ex.ToString())); 
                    OnLogError(this, new RptEventArgs(_ADP.SelectCommand.CommandText)); 
                    OnLogError(this, new RptEventArgs(_ADP.SelectCommand.Connection.ConnectionString)); 
                    OnLogError(this, new RptEventArgs(_ADP.SelectCommand.CommandType.ToString())); 
                }
			}
			finally
			{
				_DBConn.Close(); 
			}
		}

		public void Fill(IDbCommand aCommand)
		{
			try
			{
				_DBConn.Close(); 
				_DBConn.Open(); 
				DefineTable(Table); 
				_ADP.SelectCommand = aCommand; 
				_ADP.SelectCommand.Connection = _DBConn;

                if (_ADP is MySql.Data.MySqlClient.MySqlDataAdapter)
                    ((MySql.Data.MySqlClient.MySqlDataAdapter)_ADP).Fill(Table);

                if (_ADP is System.Data.SqlClient.SqlDataAdapter)
                    ((System.Data.SqlClient.SqlDataAdapter)_ADP).Fill(Table);

                if (_ADP is System.Data.OleDb.OleDbDataAdapter)
                    ((System.Data.OleDb.OleDbDataAdapter)_ADP).Fill(Table); 
			}
			catch (Exception Ex)
			{
				Logger.LogWriter.Write(Ex.Message);
                Logger.LogWriter.Write(aCommand.CommandText);
                Logger.LogWriter.Write(aCommand.Connection.ConnectionString);
                Logger.LogWriter.Write(aCommand.CommandType.ToString()); 
               
			}
			finally 
			{
				_DBConn.Close(); 
			}
		}


		/// <summary>
		/// Writes the changes in the DataTable back to the real table. 
		/// Now that the table requires an auditable trail, there is no deleteion ability  
		/// and Data MUST BE inSERTED AND DELETED IN A WAY that will preserve the old data. 
		/// Databack to the table.
		/// </summary>
 
		public void UpdateChanges()
		{
			try
			{
				_DBConn.Close(); 
				_DBConn.Open(); 
				//_ADP.Update(Table); 				 

                if (_ADP is MySql.Data.MySqlClient.MySqlDataAdapter)
                    ((MySql.Data.MySqlClient.MySqlDataAdapter)_ADP).Update(Table);

                if (_ADP is System.Data.SqlClient.SqlDataAdapter)
                    ((System.Data.SqlClient.SqlDataAdapter)_ADP).Update(Table);

                if (_ADP is System.Data.OleDb.OleDbDataAdapter)
                    ((System.Data.OleDb.OleDbDataAdapter)_ADP).Update(Table);
			}
			catch (Exception Ex)
			{
				Logger.LogWriter.Write( Ex.Message  ,"VTable:UpdateChangeError"); 				
			}
			finally
			{
				_DBConn.Close();
			}
		}
	

		#region Functions to support Version Control

		protected DateTime dteBlockVCChange; 
		protected bool UseBlockDate = false; // Defaults to Current DateTime 


		/// <summary>
		/// enables the user to Copy Data Between structure Types. 
		/// </summary>
		/// <param name="toRow"></param>
		/// <param name="FromRow"></param>
		/// <param name="_MTT"></param>
		public void CopyRowDataTo(DataRow FromRow, DataRow toRow, eMergeTableType _MTT)
		{
			foreach(VTable_FieldDef o in TBL_Fields)
			{
				if (_MTT == eMergeTableType.ImportSpec)
				{
					if (o.IsImportField)
					{
						toRow[o.FName] = FromRow[o.FImportName]; 
					}
				}
				else 
				{
					toRow[o.FName] = FromRow[o.FName]; 
				}
			}
		}


		/// <summary>
		/// This is the only approved way to make a change to the production data 
		/// Note this Functiong Assumes the ImportSpec
		/// </summary>
		/// <param name="aTable"></param>
		public int MergeDataTableintoMasterTable(DataTable aTable)
		{
			return MergeDataTableintoMasterTable(aTable, eMergeTableType.ImportSpec);  
		}


		public int MergeDataTableintoMasterTable(DataTable aTable, eMergeTableType _MTT)
		{
			dteBlockVCChange =  DateTime.Now; // ensures that the newmerge items get a Recent TS 
			int MergeCount = 0; 

			foreach(DataRow aRow in aTable.Rows)
			{
				// this version applies a test to check if row is same 
				if (RejectExistingMasterRow(aRow,_MTT))
				{ 
					MergeNewDataRow(aRow,_MTT);
					MergeCount ++;
				}
			}
			
			return MergeCount; 
		}


		public int Merge_RejectfromMasterTable(DataTable aTable, eMergeTableType _MTT)
		{
			dteBlockVCChange =  DateTime.Now; // ensures that the newmerge items get a Recent TS 
			int MergeCount = 0; 

			foreach(DataRow aRow in aTable.Rows)
			{
				if (RejectMasterRow(aRow,_MTT))
				{ 
					MergeCount ++;
				}
			}
			
			return MergeCount; 
		}

		public DataTable GetEmptyMergeTable()
		{
			return GetEmptyMergeTable(eMergeTableType.ImportSpec); 
		}

		public DataTable GetEmptyMergeTable(eMergeTableType _MTT)
		{
			DataTable aTable =  new DataTable(); 
			aTable.Clear(); 
			aTable.Columns.Clear(); 
						
			foreach (VTable_FieldDef o in TBL_Fields)
			{
				if (_MTT == eMergeTableType.ImportSpec)
				{
					if (o.IsImportField)
						aTable.Columns.Add(new DataColumn(o.FImportName, Type.GetType(o.FType)));	
				}
				else 
				{
					aTable.Columns.Add(new DataColumn(o.FName, Type.GetType(o.FType)));
				}
			}	

			return aTable; 
		}


		/// <summary>
		/// Look for an existing approved row that is to be rejected and mark rejected
		/// </summary>
		/// <param name="aRow"></param>
		/// <returns></returns>
		protected bool RejectExistingMasterRow(DataRow aRow, eMergeTableType _MTT)
		{
            if (_EnableVC)
            {
                CheckView.RowFilter = BuildRowFilterArgfromKey(GetMergeKeyFields(), aRow, _MTT) + " and VC_State = 'Approved'";
            }
            else 
            {
                CheckView.RowFilter = BuildRowFilterArgfromKey(GetMergeKeyFields(), aRow, _MTT); 
            }
		
			if(CheckView.Count >= 1)
			{
				if(!IsRowSame(aRow, CheckView[0].Row,_MTT))
				{
					VersionCtrl_RejectRow(CheckView[0].Row); 
					return true; 
				}				
				return false; 				
			}
			// No row found to compaire, hence must be different
			return true;
		}


		/// <summary>
		/// No test for Is same
		/// </summary>
		/// <param name="aRow"></param>
		/// <param name="_MTT"></param>
		/// <returns></returns>
		protected bool RejectMasterRow(DataRow aRow, eMergeTableType _MTT)
		{
            if (_EnableVC)
            {
                CheckView.RowFilter = BuildRowFilterArgfromKey(GetMergeKeyFields(), aRow, _MTT) + " and VC_State = 'Approved'";
            }
            else
            {
                CheckView.RowFilter = BuildRowFilterArgfromKey(GetMergeKeyFields(), aRow, _MTT); 
            }
			if(CheckView.Count >= 1)
			{
			//	if(!IsRowSame(aRow, CheckView[0].Row,_MTT))
			//	{
					VersionCtrl_RejectRow(CheckView[0].Row); 
					return true; 
			//	}				
			//	return false; 				
			}
			// No row found to compaire, hence must be different
			return true;
		}

	
		/// <summary>
		/// Test Import Row for Differences 
		/// </summary>
		/// <param name="myRow"></param>
		/// <param name="aRow"></param>
		/// <returns></returns>
		protected bool IsRowSame(DataRow myRow, DataRow aRow, eMergeTableType _MTT)
		{
			Int32 ScoreTotal = 0; 
			Int32 Score = 0; 
			foreach(VTable_FieldDef o in TBL_Fields)
			{
				if (_MTT == eMergeTableType.ImportSpec)
				{
					if (o.IsImportField)
					{
						ScoreTotal++; 				
						if (myRow[o.FImportName].ToString().Trim().ToUpper() == aRow[o.FName].ToString().Trim().ToUpper())
						{
							Score++;
						}					
					}
				}
				else 
				{
					ScoreTotal++; 				
					if (myRow[o.FName].ToString().Trim().ToUpper() == aRow[o.FName].ToString().Trim().ToUpper())
					{
						Score++;
					}	

				}
			}

			return ScoreTotal == Score; 
		}

		/// <summary>
		/// Copy Import Fields from merge row to new Row 
		/// </summary>
		/// <param name="aRow"></param>
		protected void MergeNewDataRow(DataRow aRow, eMergeTableType _MTT)
		{		   
			DataRow myRow = Table.NewRow(); 

			foreach(VTable_FieldDef o in TBL_Fields)
			{
				if (_MTT == eMergeTableType.ImportSpec)
				{
					if (o.IsImportField)
					{
						myRow[o.FName] = aRow[o.FImportName]; 
					}
				}
				else 
				{
					myRow[o.FName] = aRow[o.FName]; 
				}
			}

			VersionCtrl_CreateRow(myRow);
			Table.Rows.Add(myRow); 
		}


		protected void VersionCtrl_CreateRow(DataRow aRow)
		{		

			if (_EnableVC)
			{
				/*
				 * Set Created TSU, Defaults for Approve, reject and inital value of VC_State
				 * */
				//aRow.BeginEdit();
				aRow["VC_Create"] = GetTSU_Stamp(); 		
				aRow["VC_Approve"] = Convert.DBNull; 
				aRow["VC_Reject"] = Convert.DBNull;

				aRow["VC_State"] =	 "Pending"; 
				aRow["VC_Checksum"] =  Convert.DBNull; 			
				//aRow.EndEdit(); 
			}
		}

		protected void VersionCtrl_RejectRow(DataRow aRow)
		{		
			if (_EnableVC)
			{
				/*
				 * Set VS_State to Rejected
				 * Set VS_rejected = TSU(currentUser);
				 * */

				aRow.BeginEdit();
				aRow["VC_State"] = "Rejected"; 
				aRow["VC_Reject"] = GetTSU_Stamp(); 
				aRow.EndEdit();

				aRow.BeginEdit(); 
				aRow["VC_Checksum"] = GetSignature(aRow);
				aRow.EndEdit(); 
			}
		}

		protected void VersionCtrl_ApproveRow(DataRow aRow)
		{
			if (_EnableVC)
			{
				/*
				 * Set VS_State to Approved
				 * Set VS_rejected = TSU(currentUser);			 
				 * */

				aRow.BeginEdit(); 
				aRow["VC_State"] = "Approved"; 
				aRow["VC_Approve"] = GetTSU_Stamp(); 
				aRow.EndEdit(); 

				aRow.BeginEdit(); 
				aRow["VC_Checksum"] = GetSignature(aRow);
				aRow.EndEdit(); 
			}
		}		


		public void ApprovePending()
		{
			if (_EnableVC)
			{
				DataView aView = new DataView(Table); 
				aView.RowFilter = "VC_State = 'Pending'"; 

				for( int i = aView.Count -1; i>= 0 ; i--)
				{
					VersionCtrl_ApproveRow(aView[i].Row);
				}
			}			
		}

		public void RejectPending()
		{
			if (_EnableVC)
			{
				DataView aView = new DataView(Table); 
				aView.RowFilter = "VC_State = 'Pending'"; 

				for( int i = aView.Count-1; i> 0 ; i--)
				{
					VersionCtrl_RejectRow(aView[i].Row);
				}	
			}
		}

		/// <summary>
		/// Has the Effect of a Full Deletion
		/// </summary>
		public void RejectApproved()
		{
			if (_EnableVC)
			{
				DataView aView = new DataView(Table); 
				aView.RowFilter = "VC_State = 'Approved'"; 

				for( int i = aView.Count-1; i> 0 ; i--)
				{
					VersionCtrl_RejectRow(aView[i].Row);
				}
			}
		}


		/// <summary>
		/// Get the CCYYMMDD_HHMM_@USER value  
		/// </summary>
		/// <returns></returns>
		protected string GetTSU_Stamp()		
		{
			if (UseBlockDate)
			{   return GetTSU_Stamp(dteBlockVCChange); 
				 
			}
			else 
			{
				return GetTSU_Stamp(DateTime.Now); 
			}
		}

		/// <summary>
		///	Get the CCYYMMDD_HHMM_@USER value
		/// </summary>
		/// <param name="dte"></param>
		/// <returns></returns>
		public string GetTSU_Stamp(DateTime dte)		
		{
			return DateUtils.DTEtoCCYYMMDD_HHMMSS(dte) +"_"+ SESSION_USERNAME; 
		}

		/// <summary>
		/// Returns the string to be used
		/// </summary>
		/// <returns></returns>
		protected virtual string GetHashSeed(DataRow aRow)
		{	
			string Seed = ""; 

			foreach(VTable_FieldDef o in TBL_Fields)
			{
				if (!aRow.IsNull(o.FName))
				{
					if (o.FType == "System.DateTime")
					{
                        Seed += DateUtils.DTEtoCCYYMMDD((DateTime)(aRow[o.FName])); 
					}
					else
					{
						Seed += aRow[o.FName].ToString().Trim(); 
					}
				}
				else
				{
					Seed += "NULL";
				}
			}

			if (_EnableVC) 
			{
				Seed += aRow["VC_State"].ToString(); 
				Seed += aRow["VC_Approve"].ToString(); 
			}
		
			return Seed; 
		}

		public string GetSignature(DataRow aRow)
		{
			return MD5Engine.Encrypt(GetHashSeed(aRow)); 
		}

		public DataTable GetTamperedData()
		{

			if (!_EnableVC) throw new Exception("Version Control Disabled in this VTable Object"); 

			DataTable tmp = Table.Clone(); 
			tmp.TableName = "Tampered_" +Table.TableName; 

			DataRow arow; 
			foreach(DataRow Dr in this.Table.Rows)
			{
				if (Dr["VC_Checksum"].ToString() != GetSignature(Dr))
				{
					arow = tmp.NewRow(); 
					arow.BeginEdit(); 

					foreach(DataColumn s in tmp.Columns)
					{
						arow[s.ColumnName] = Dr[s.ColumnName]; 
					}

					arow.EndEdit(); 
					tmp.Rows.Add(arow); 					 
				}
			}

			return tmp; 
		}
		#endregion 

		#region Functions to get lists of Attributes 

        private string ApplyFieldDelimiter(string s) 
        {
            if (_EnableFieldDelimiter) return _BeginDelimiterChar.ToString() + s + _EndDelimiterChar.ToString();
            return s; 
        }

		public virtual string GetSQLTableName()
		{
			return _SQLTableName; 
		}
		public virtual string GetSQLFields()
		{
			string query = ""; 
			bool first = true; 

			foreach(VTable_FieldDef o in TBL_Fields)
			{
				if (first)
				{
                    query += ApplyFieldDelimiter(o.FName);
					first = false; 
					}
				else 
				{
                    query += " , " + ApplyFieldDelimiter(o.FName);
				}
			}

			if (_EnableVC) 
			foreach(VTable_FieldDef o in VC_Fields)
			{
				if (first)
				{
                    query += ApplyFieldDelimiter(o.FName);
					first = false; 
				}
				else 
				{
                    query += " , " + ApplyFieldDelimiter(o.FName);
				}
			}

			return query; 
		}
		

		public virtual string GetSQLFields(ArrayList List)
		{
			string query = ""; 
			bool first = true; 

			foreach(VTable_FieldDef o in List)
			{
				if (first)
				{
                    query += ApplyFieldDelimiter(o.FName);
					first = false; 
				}
				else 
				{
                    query += " , " + ApplyFieldDelimiter(o.FName);
				}
			}

			return query; 
		}
		
	
		/// <summary>
		/// Use in situations where the VC Fields are not required , ie during merge 
		/// </summary>
		/// <returns></returns>
		public virtual ArrayList GetMergeKeyFields()
		{
			ArrayList tmp = new ArrayList(); 

			foreach(VTable_FieldDef o in TBL_Fields)
			{
				if ((o.IsKeyField) && (o.IsImportField))
				{
					tmp.Add(o); 
				}
			}
			return tmp;
		}

		/// <summary>
		/// Use when using the Standard table
		/// </summary>
		/// <returns></returns>
		public virtual ArrayList GetKeyFields()
		{
			ArrayList tmp = new ArrayList(); 

			foreach(VTable_FieldDef o in TBL_Fields)
			{
				if (o.IsKeyField)
				{
					tmp.Add(o); 
				}
			}

			if (_EnableVC)
			foreach(VTable_FieldDef o in VC_Fields)
			{
				if (o.IsKeyField)
				{
					tmp.Add(o); 
				}
			}

			return tmp; 
		}
		

		
		public string GetDeleteSQL()
		{
			string query = "delete * from " + _SQLTableName  + " where " ; 

			bool first = true; 
			foreach(VTable_FieldDef o in TBL_Fields )
			{
				if (o.IsKeyField)
				{
					if (first)
					{
                        query += ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
						first = false; 
					}
					else 
					{
                        query += " and " + ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
					}
				}
			}

			if (_EnableVC)
			foreach(VTable_FieldDef o in VC_Fields )
			{
				if (o.IsKeyField)
				{
					if (first)
					{
                        query += ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
						first = false; 
					}
					else 
					{
                        query += " and " + ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
					}
				}
			}

			return query; 
		}

		public string GetInsertSQL()
		{
			string query = "Insert into " + _SQLTableName + " ( " + GetSQLFields() + " ) values ( "  ; 

			bool first = true;

			foreach(VTable_FieldDef o in TBL_Fields )
			{
				if (first)
				{
					query += " @"+ o.FName ;
					first = false; 
				}
				else 
				{
					query += " , @"+ o.FName;
				}
			}

			if (_EnableVC)
			foreach(VTable_FieldDef o in VC_Fields )
			{
				if (first)
				{
					query += " @"+ o.FName ;
					first = false; 
				}
				else 
				{
					query += " , @"+ o.FName;
				}
				
			}

			query += " )"; 


			return query; 
		}


		public string GetUpdateSQL()
		{
			string query = "Update " + _SQLTableName + " Set "; 
			bool first = true; 

			foreach(VTable_FieldDef o in TBL_Fields )
			{
				if (!o.IsKeyField)
				{
					if (first)
					{
                        query += ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
						first = false; 
					}
					else 
					{
                        query += " , " + ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
					}
				}
			}

			if (_EnableVC)
			foreach(VTable_FieldDef o in VC_Fields )
			{
				if (!o.IsKeyField)
				{
					if (first)
					{
                        query += ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
						first = false; 
					}
					else 
					{
                        query += " , " + ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
					}
				}
			}


			query += " where " ; 
			// where conditions 

			first = true; 
			foreach(VTable_FieldDef o in TBL_Fields )
			{
				if (o.IsKeyField)
				{
					if (first)
					{
                        query += ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
						first = false; 
					}
					else 
					{
                        query += " and " + ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
					}
				}
			}

			if (_EnableVC)
			foreach(VTable_FieldDef o in VC_Fields )
			{
				if (o.IsKeyField)
				{
					if (first)
					{
                        query += ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
						first = false; 
					}
					else 
					{
                        query += " and " + ApplyFieldDelimiter(o.FName) + @" = @" + o.FName;
					}
				}
			}


			return query; 
		}


		/// <summary>
		/// gets row filter segment using FName 
		/// </summary>
		/// <param name="o"></param>
		/// <param name="aRow"></param>
		/// <returns></returns>
		public string BuildRowFilterArg(VTable_FieldDef o,  DataRow aRow, eMergeTableType _MTT)
		{
			string FilterArg = ""; 

			if (_MTT == eMergeTableType.ImportSpec)
			{

				if (!aRow.IsNull(o.FImportName))
				{
					switch (o.FType)
					{
						case "System.String":
						{
					
							FilterArg +=  o.FName + " = '" + aRow[o.FImportName] + "' "; 
					
							break;
						}

						case "System.Double":
						case "System.Int32":
						{
							FilterArg +=  o.FName + " = " + aRow[o.FImportName] + " "; 
							break;
						}

						case "System.DateTime":
						{
							FilterArg +=  o.FName + " = #" + ((DateTime)aRow[o.FImportName]).ToLongDateString() + "# "; 
							break;
						}
					}
				}
				else 
				{
					FilterArg += o.FName + " is NULL "; 
				}
			}
			else
			{
				if (!aRow.IsNull(o.FName))
				{
					switch (o.FType)
					{
						case "System.String":
						{
					
							FilterArg +=  o.FName + " = '" + aRow[o.FName] + "' "; 
					
							break;
						}

						case "System.Double":
						case "System.Int32":
						{
							FilterArg +=  o.FName + " = " + aRow[o.FName] + " "; 
							break;
						}

						case "System.DateTime":
						{
							FilterArg +=  o.FName + " = #" + ((DateTime)aRow[o.FName]).ToLongDateString() + "# "; 
							break;
						}
					}
				}
				else 
				{
					FilterArg += o.FName + " is NULL "; 
				}

			}

			return FilterArg; 
		}

		public string BuildRowFilterArgfromKey(ArrayList List, DataRow aRow, eMergeTableType _MTT)
		{
			
			string FilterArg = ""; 
			bool First = true; 

			foreach(VTable_FieldDef o in List) 
			{
				if (First) 
				{
					First = false; 
				} 
				else 
				{
					FilterArg += " and " ; 
				}

				FilterArg += BuildRowFilterArg(o,aRow,_MTT);
			}
			return FilterArg; 
		}



		#endregion
	
		#region Static Select Methods 
		
        #region old, but useful
        /*
		public static VTable_Base Select(string tablename, string statement, string dbString, eDatabaseType _dbtype)
		{	
			switch (_dbtype)
			{				
				case eDatabaseType.SQLServer : 
				{
					SqlConnection aConnection = new SqlConnection(dbString);	  
					return Select(tablename,statement,aConnection); 
				}
				case eDatabaseType.OleDB : 
				{
					OleDbConnection aConnection = new OleDbConnection(dbString);	
					return Select(tablename,statement,aConnection); 
				}
			}

			throw new Exception("DataBase type not defined"); 
		}
	
		public static VTable_Base Select(string tablename, string statement, OleDbConnection aoleConnection)
		{

			VTable_Base tmp = new VTable_Base(tablename,false); 
			OleDbDataAdapter aDp = new OleDbDataAdapter(statement, aoleConnection); 
			
			try 
			{	
				aDp.SelectCommand.Connection.Close(); 
				aDp.SelectCommand.Connection.Open(); 
				aDp.Fill(tmp.Table); 
				//aDp.SelectCommand.Connection.Close(); 
			}
			catch(Exception Ex)
			{
				Logger.LogWriter.Write(Ex.Message); 
			}   
			finally 
			{
				aDp.SelectCommand.Connection.Close();
			}

			return tmp; 
		}	
	
		public static VTable_Base Select(string tablename, string statement, SqlConnection aConnection)
		{

			VTable_Base tmp = new VTable_Base(tablename,false); 
			SqlDataAdapter aDp = new SqlDataAdapter(statement, aConnection); 
			
			try 
			{	
				aDp.SelectCommand.Connection.Close(); 
				aDp.SelectCommand.Connection.Open(); 
				aDp.Fill(tmp.Table); 
				//aDp.SelectCommand.Connection.Close(); 
			}
			catch(Exception Ex)
			{
				Logger.LogWriter.Write(Ex.Message); 
			}   
			finally 
			{
				aDp.SelectCommand.Connection.Close();
			}

			return tmp; 
		}
         * 
         * public static VTable_Base Select(string tablename, OleDbCommand aSelectCommand)
		{

			VTable_Base tmp = new VTable_Base(tablename,false); 
			OleDbDataAdapter aDp = new OleDbDataAdapter( aSelectCommand); 

			try 
			{
				aDp.SelectCommand.Connection.Close(); 
				aDp.SelectCommand.Connection.Open(); 
				aDp.Fill(tmp.Table); 
				//	aDp.SelectCommand.Connection.Close(); 
			}
			catch(Exception Ex)
			{
				Logger.LogWriter.Write(Ex.Message); 
			}  
			finally 
			{
				aDp.SelectCommand.Connection.Close();
			}
			return tmp; 
		}

		public static VTable_Base Select(string tablename, SqlCommand aSelectCommand)
		{

			VTable_Base tmp = new VTable_Base(tablename,false); 
			SqlDataAdapter aDp = new SqlDataAdapter(aSelectCommand); 

			try 
			{
				aDp.SelectCommand.Connection.Close(); 
				aDp.SelectCommand.Connection.Open(); 
				aDp.Fill(tmp.Table); 
				//	aDp.SelectCommand.Connection.Close(); 
			}
			catch(Exception Ex)
			{
				Logger.LogWriter.Write(Ex.Message); 
			}  
			finally 
			{
				aDp.SelectCommand.Connection.Close();
			}
			return tmp; 
		}

	

       */
        #endregion

        public static VTable_Base Select(string tablename, string statement, IDatabaseConfig _DatabaseConfig)
        {

            VTable_Base tmp = new VTable_Base(tablename, false);
            IDbConnection aConnection = _DatabaseConfig.GetHandler().getDataConnection(); 
            IDbDataAdapter aDp = _DatabaseConfig.GetHandler().getDataAdapter(statement, aConnection);    // new SqlDataAdapter(statement, aConnection);

            try
            {
                aDp.SelectCommand.Connection.Close();
                aDp.SelectCommand.Connection.Open();
             //   aDp.Fill(tmp.Table);

                if (aDp is MySql.Data.MySqlClient.MySqlDataAdapter)
                    ((MySql.Data.MySqlClient.MySqlDataAdapter)aDp).Fill(tmp.Table);

                if (aDp is System.Data.SqlClient.SqlDataAdapter)
                    ((System.Data.SqlClient.SqlDataAdapter)aDp).Fill(tmp.Table);

                if (aDp is System.Data.OleDb.OleDbDataAdapter)
                    ((System.Data.OleDb.OleDbDataAdapter)aDp).Fill(tmp.Table); 
                //aDp.SelectCommand.Connection.Close(); 
            }
            catch (Exception Ex)
            {
                Logger.LogWriter.Write(Ex.Message);
            }
            finally
            {
                aDp.SelectCommand.Connection.Close();
            }

            return tmp;
        }

        public static VTable_Base Select(string tablename, IDbCommand aSelectCommand, IDatabaseConfig _DatabaseConfig)
        {

            VTable_Base tmp = new VTable_Base(tablename, false);
            IDbDataAdapter aDp = _DatabaseConfig.GetHandler().getDataAdapter(aSelectCommand);    
          
            try
            {
                aDp.SelectCommand.Connection.Close();
                aDp.SelectCommand.Connection.Open();
               // aDp.Fill(tmp.Table);
                //	aDp.SelectCommand.Connection.Close(); 
                if (aDp is MySql.Data.MySqlClient.MySqlDataAdapter)
                    ((MySql.Data.MySqlClient.MySqlDataAdapter)aDp).Fill(tmp.Table);

                if (aDp is System.Data.SqlClient.SqlDataAdapter)
                    ((System.Data.SqlClient.SqlDataAdapter)aDp).Fill(tmp.Table);

                if (aDp is System.Data.OleDb.OleDbDataAdapter)
                    ((System.Data.OleDb.OleDbDataAdapter)aDp).Fill(tmp.Table); 
            }
            catch (Exception Ex)
            {
                Logger.LogWriter.Write(Ex.Message);
            }
            finally
            {
                aDp.SelectCommand.Connection.Close();
            }
            return tmp;
        }
	
		
		
		#endregion

		#region SelectDistinct Variations
		public ArrayList SelectDistinct(string FieldName)
		{
			if (Table.Columns.Contains(FieldName))
			{
				ArrayList tmp = new ArrayList(); 

				object obj; 
				foreach(DataRow Dr in Table.Rows)
				{
					obj = Dr[FieldName]; 
					if (!tmp.Contains(obj)) tmp.Add(obj); 
				}
				return tmp; 
			}

			throw new Exception("Field not in Table"); 
		}

		public static ArrayList SelectDistinct(DataTable aTable, string FieldName)
		{
			if (aTable.Columns.Contains(FieldName))
			{
				ArrayList tmp = new ArrayList(); 

				object obj; 
				foreach(DataRow Dr in aTable.Rows)
				{
					obj = Dr[FieldName]; 
					if (!tmp.Contains(obj)) tmp.Add(obj); 
				}
				return tmp; 
			}

			throw new Exception("Field not in Table"); 
		}

		public static ArrayList SelectDistinct(DataView aVw, string FieldName)
		{
			if (aVw.Table.Columns.Contains(FieldName))
			{
				ArrayList tmp = new ArrayList(); 

				object obj; 
				for(int i = 0; i <= aVw.Count -1 ; i++)
				{
					obj = aVw[i][FieldName]; 
					if (!tmp.Contains(obj)) tmp.Add(obj); 
				}
				return tmp; 
			}

			throw new Exception("Field not in View"); 
		}

		#endregion


		#region CSV Import

		public void RenameCSVFields(DataTable CSVTable, eMergeTableType _MTT)
		{
			foreach(VTable_FieldDef o in TBL_Fields)
			{
				if ((o.CSVPosition >= 0)&& (o.CSVPosition <= CSVTable.Columns.Count -1))
				{
					if (_MTT == eMergeTableType.ImportSpec)
					{
						CSVTable.Columns[o.CSVPosition].ColumnName = o.FImportName;
					}
					else 
					{
						CSVTable.Columns[o.CSVPosition].ColumnName = o.FName;
					}
				}

			}
		}


		private string Convert2CSV(Object[] o)
		{
			string s = "";
			for (int i = 0 ; i <= o.Length -1; i++)
			{
				if (i ==0) 
					s = o[i].ToString(); 
				else 
				{
					s = s+"| "+o[i].ToString(); 
				}
			}

			return s; 
		}

		/// <summary>
		/// Returns a Merge Formated Table 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="_MTT"></param>
		/// <returns></returns>
		public DataTable ImportASCII_CSV(string csvfile, eMergeTableType _MTT)
		{
			DataTable merge = GetEmptyMergeTable(_MTT); 

			DataTable raw = CSVtoDataTable.CovertCSVtoDataTable(csvfile, System.Text.Encoding.UTF8, true); 
			RenameCSVFields(raw, _MTT);

			DataRow n; 

			foreach(DataRow R in raw.Rows)
			{
				n = merge.NewRow(); 

				#region convert CSV Import fields as string to RealType 
				foreach(DataColumn dc in merge.Columns)
				{

					if (!R.IsNull(dc.ColumnName)) 
					{				
					
						try
						{
							switch (dc.DataType.ToString())
							{
								case "System.DateTime":
								{
                                    n[dc.ColumnName] = DateUtils.DDMMMCCYY2DTE(R[dc.ColumnName].ToString().Trim()); 							
									break; 
								}

								case "System.Int32":
								{
									n[dc.ColumnName] =  Convert.ToInt32(R[dc.ColumnName].ToString().Trim()); 
									break; 
								} 

								case "System.Double":
								{
									n[dc.ColumnName] =  Convert.ToDouble(R[dc.ColumnName].ToString().Trim()); 
									break; 
								} 

								case "System.Boolean":
								{
									n[dc.ColumnName] =  Convert.ToBoolean(R[dc.ColumnName].ToString().Trim()); 
									break; 
								} 

								case "System.String": 
								default:
								{
									n[dc.ColumnName] = R[dc.ColumnName].ToString().Trim(); 
									break; 
								}
							}
						}
						catch 
						{
							if (OnLogError != null ) { OnLogError(this, new RptEventArgs("Data Error col["+dc.ColumnName+"]: " + Convert2CSV(R.ItemArray))); }																													
							n[dc.ColumnName] = Convert.DBNull; 
						}
					}
					else 
					{
						n[dc.ColumnName] = Convert.DBNull; 
					}
				}
				#endregion 
				merge.Rows.Add(n); 
			}
			
			return merge; 
		}

		#endregion 


		private static string FormatAsImportType(object o , string _Type)
		{

            if (o == DBNull.Value)
            {
                if (_Type == "System.Int32") return Convert.ToString(0); 
                if (_Type == "System.String") return "\"\"";
                if (_Type == "System.Double") return Convert.ToString(0.0d); ;
                if (_Type == "System.Boolean") return Convert.ToString(false);
                if (_Type == "System.DateTime") return DateUtils.DTEtoPMS_DD_MMM_CCYY(new DateTime(1980, 12, 31)); 
                return "NULL";
            }

			try
			{

				switch(_Type)			
				{
					case "System.DateTime":
					{
                        
                        return DateUtils.DTEtoPMS_DD_MMM_CCYY(((DateTime)o)); 					
					}
					case "System.String": 
					{                      
                        string s = Convert.ToString(o); 
                        if (String.IsNullOrEmpty(s)) return "\"\"";

						return "\"" + s.Replace('"',' ').Replace("&","and").Replace("%","percent").Replace('\\','/').Replace('|','-').Replace('\r',' ').Replace('\n',' ').Replace("\r\n","-").Trim().ToUpper() + "\""; 
					}                  
					default:
					{                       
                        return Convert.ToString(o); 			
					}
				}

			}
			catch
			{
				return o.ToString(); 
			}
		}

		public void Export_ImportSpec( string filename, char sepChar, System.Text.Encoding enc, bool bWithHeaderDef)
		{

			DataView dv = new DataView(this.Table); 
			dv.Sort = GetSQLFields(GetKeyFields()); 

			ArrayList SortedFields = new ArrayList(); 


			// sort the items into the Order required for Import. 
			// if the position = -1 :  ignore

			int maxPos = 0 ; 
			for (int i = 0 ; i <= this.TBL_Fields.Count-1; i++) 
			{
				if (maxPos < ((VTable_FieldDef)TBL_Fields[i]).CSVPosition) 
					maxPos = ((VTable_FieldDef)TBL_Fields[i]).CSVPosition; 
			}

			for (int i = 0 ; i <= maxPos; i++) 
			{
				for (int j=0 ; j<= TBL_Fields.Count-1; j++)
				{
					if (((VTable_FieldDef)TBL_Fields[j]).CSVPosition == i)
					{
						SortedFields.Add(TBL_Fields[j]); 
						break;
					}
				}
			}

			DataTable dt = dv.Table; 
			System.IO.StreamWriter writer = new System.IO.StreamWriter(filename,false, enc);

			// first write a line with the columns name
			string sep  = ""; 
			bool firstcol = true; 

			System.Text.StringBuilder builder = new System.Text.StringBuilder(""); 

            if (bWithHeaderDef){
                //sep = "";
                sep = sepChar.ToString();
               // builder = new System.Text.StringBuilder("");

                firstcol = true;
                foreach (VTable_FieldDef col in SortedFields)
                {
                    if (firstcol)
                    {
                        builder.Append(col.ToHeaderDef());
                        firstcol = false;
                    }
                    else
                    {
                        builder.Append(sep).Append(col.ToHeaderDef());
                    }                    
                }
                writer.WriteLine(builder.ToString());
            }
					
			DataRow Row; 
            
            sep = sepChar.ToString();
			// then write all the rows
			for( int i = 0 ;  i <= dv.Count -1 ; i++)													
			{
                try
                {
                    Row = dv[i].Row;
            //        sep = "";
                    builder = new System.Text.StringBuilder("");

                    firstcol = true;
                    foreach (VTable_FieldDef col in SortedFields)
                    {
                        if (firstcol)
                        {
                            builder.Append(FormatAsImportType(Row[col.FName], col.FType));
                            firstcol = false;
                        }
                        else
                        {
                            builder.Append(sep).Append(FormatAsImportType(Row[col.FName], col.FType));
                        }
                        
                    }
                    writer.WriteLine(builder.ToString());
                }
                catch (Exception ex)
                {
                    if (OnLogError != null) OnLogError(this, new RptEventArgs("Row " + i.ToString() + " :"+ ex.Message)); 
                }

			}

			writer.Flush();
			writer.Close(); 
		}

	

		public void Export(string filename)
		{
			try
			{
				StreamWriter sw = new StreamWriter(filename, false);  
				try 
				{
				 
					foreach(DataRow Dr in Table.Rows)
					{
						sw.WriteLine(convert2CSV(Dr)); 
					}
				}
				catch (Exception Ex)
				{
					if (OnLogError != null) OnLogError(this, new RptEventArgs(Ex.Message)); 
				}
				finally 
				{
					if (sw != null)
					{
						sw.Flush(); 
						sw.Close(); 
					}
				}
			}
			catch(Exception Ex)
			{
				if (OnLogError != null) OnLogError(this, new RptEventArgs(Ex.Message)); 
			}

			
		}
		 
		private string convert2CSV(DataRow r)
		{
			string s = ""; 
			bool first = true; 
			
			foreach(VTable_FieldDef f in this.TBL_Fields)
			{
				if (first)
				{
					s= ""; 
					first = false; 
				}
				else
				{
						s += "|"; 
				}

				if (r[f.FName] != null) 
				{
					s+= "NULL"; 
				}
				else
				{
					switch(f.FType)
					{
						case "System.DateTime": 
						{						
							s+= DateUtils.DTEtoPMS_DD_MMM_CCYY((DateTime)r[f.FName]); 
							break;
						}

						case "System.String": 
						{
							s+= "\""+r[f.FName].ToString() + "\""; 
							break; 
						}

						default: 
						{
							s+= r[f.FName].ToString() ; 
							break; 
						}
					}
				}

			}
			return ""; 
		}

	}



	public enum eMergeTableType {RealSpec =0, ImportSpec = 1}; 
	public enum eDatabaseType {OleDB =0, SQLServer =1, TextFile=2}; 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LandSync;
using Land.LegacyDb.Tables;
using Lincore.SimpleEvent; 


namespace LandApp
{
   public class Work_ExportCSV
    {
        public delegate void RptGeneratedHandler(object s, RptEventArgs e);
        public event RptGeneratedHandler OnRptGenerated;

        public delegate void LogGeneratedHandler(object s, LogEventArgs e);
        public event LogGeneratedHandler OnLogGenerated;

       public Work_ExportCSV()
       {
           log = new List<object>();
       }

       private string filename;
       private string exportto;
       public IList<object> log;

       public void Start(Object obj)
       {
           WorkArgs args = (WorkArgs)obj;
           filename = (string)(args.KeyPairArgs["filename"]);
           exportto = (string)(args.KeyPairArgs["dest"]);
 
           LegacyDatabaseDef db = new LegacyDatabaseDef(filename);
           SyncDB sync = new SyncDB(db, log);
           try
           {             
               sync.ExportLandDB(exportto);               
           }
           catch (Exception ex)
           {
               log.Add(ex);               
           }
           finally
           { 
               if (OnLogGenerated != null) { OnLogGenerated(this, new LogEventArgs(log.ToArray())); }	
           if (OnRptGenerated != null) { OnRptGenerated(this, new RptEventArgs("Database Export Complete")); }	
           }

          
       }
    }
}

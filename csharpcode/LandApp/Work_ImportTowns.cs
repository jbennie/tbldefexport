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
   public class Work_ImportTowns
    {

        public delegate void RptGeneratedHandler(object s, RptEventArgs e);
        public event RptGeneratedHandler OnRptGenerated;

        public delegate void LogGeneratedHandler(object s, LogEventArgs e);
        public event LogGeneratedHandler OnLogGenerated; 

       public Work_ImportTowns() 
       {
           log = new List<object>();
       }

       private string dbpath;
       private string townfile;
       public IList<object> log;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="obj">filename , datafile</param>
       public void Start(Object obj)
       {
           WorkArgs args = (WorkArgs)obj;
           dbpath = (string)(args.KeyPairArgs["filename"]);
           townfile = (string)(args.KeyPairArgs["Townfile"]);
           
           
           LegacyDatabaseDef db = new LegacyDatabaseDef(dbpath);
           SyncDB sync = new SyncDB(db, log);
           try
           {
               sync.ImportTowns(new string[] { townfile }, new VTable_Towns(db.ReadChangesFrom()));
           }
           catch (Exception ex)
           {
               log.Add(ex);
           }
           finally
           {

               if (OnLogGenerated != null) { OnLogGenerated(this, new LogEventArgs(log.ToArray())); }
               if (OnRptGenerated != null) { OnRptGenerated(this, new RptEventArgs("Database Import Complete")); }
           }
		
       }
    }
}

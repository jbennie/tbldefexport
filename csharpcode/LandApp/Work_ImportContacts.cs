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
   public class Work_ImportContacts
    {

        public delegate void RptGeneratedHandler(object s, RptEventArgs e);
        public event RptGeneratedHandler OnRptGenerated;

        public delegate void LogGeneratedHandler(object s, LogEventArgs e);
        public event LogGeneratedHandler OnLogGenerated; 

       public Work_ImportContacts() 
       {
           log = new List<object>();
       }

       private string dbpath;
       private string contactfile;
       public IList<object> log;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="obj">filename , datafile</param>
       public void Start(Object obj)
       {
           WorkArgs args = (WorkArgs)obj;
           dbpath = (string)(args.KeyPairArgs["filename"]);
           contactfile = (string)(args.KeyPairArgs["Contactfile"]);
           
           
           LegacyDatabaseDef db = new LegacyDatabaseDef(dbpath);
           SyncDB sync = new SyncDB(db, log);
           try
           {
               sync.ImportContacts(new string[] { contactfile }, new VTable_ContactsMain(db.ReadChangesFrom()));
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

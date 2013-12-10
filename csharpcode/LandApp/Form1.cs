using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Lincore.SimpleEvent; 

namespace LandApp
{
    public partial class Form1 : Form
    {     
        public delegate void MessageHandler(object s, RptEventArgs e);
        public event MessageHandler OnMsgGenerated;
        
        public Form1()
        {
            InitializeComponent();
            OnMsgGenerated += new MessageHandler(tmp_MsgGenerated);             
        }

        delegate void SetTextCallback(string text);
        delegate void SetLogCallback(object[] o);

        private void SetText(string text) 
        {
            if (this.lstStatusLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lstStatusLog.Items.Add(text);
            }            
        }

        private void SetLog(object[] o)
        {
            if (this.lstStatusLog.InvokeRequired)
            {
                SetLogCallback d = new SetLogCallback(SetLog);
                this.Invoke(d, new object[] { o });
            }
            else
            {
                this.lstStatusLog.Items.AddRange(o);                
            }
        }
                        
        public void tmp_MsgGenerated(object sender, RptEventArgs e)
        {
            this.SetText(e._Message);
        }

        public void tmp_LogGenerated(object sender, LogEventArgs e)
        {
            this.SetLog(e.logs.ToArray());
        }

        public void tmp_ReportGenerated(object sender, RptEventArgs e)
        {
            this.SetText(e._Message);
        }

        private void lstStatusLog_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(((ListBox)sender).SelectedItem.ToString());
        }

        public void RunExportInBackground(Object obj)         
        {
            if (OnMsgGenerated != null) { OnMsgGenerated(this, new RptEventArgs("Running Export")); }						
            
            Work_ExportCSV w = new Work_ExportCSV();
            w.OnRptGenerated +=  new Work_ExportCSV.RptGeneratedHandler(tmp_ReportGenerated);
            w.OnLogGenerated += new Work_ExportCSV.LogGeneratedHandler(tmp_LogGenerated);
            
            w.Start(obj);

            if (OnMsgGenerated != null) { OnMsgGenerated(this, new RptEventArgs("Completed Export")); }	
        }

        public void RunSiteImportInBackground(Object obj) 
        {
            if (OnMsgGenerated != null) { OnMsgGenerated(this, new RptEventArgs("Running Import Sites")); }			
			
            Work_ImportSites w = new Work_ImportSites();
            w.OnRptGenerated += new Work_ImportSites.RptGeneratedHandler(tmp_ReportGenerated);
            w.OnLogGenerated += new Work_ImportSites.LogGeneratedHandler(tmp_LogGenerated);
            w.Start(obj);
        
            if (OnMsgGenerated != null) { OnMsgGenerated(this, new RptEventArgs("Completed Import Sites")); }						                             
        }


        public void RunContactImportInBackground(Object obj)
        {
            if (OnMsgGenerated != null) { OnMsgGenerated(this, new RptEventArgs("Running Import Contacts")); }

            Work_ImportContacts w = new Work_ImportContacts();
            w.OnRptGenerated += new Work_ImportContacts.RptGeneratedHandler(tmp_ReportGenerated);
            w.OnLogGenerated += new Work_ImportContacts.LogGeneratedHandler(tmp_LogGenerated);
            w.Start(obj);

            if (OnMsgGenerated != null) { OnMsgGenerated(this, new RptEventArgs("Completed Import contacts")); }						                             
        }

        public void RunTownImportInBackground(Object obj)
        {
            if (OnMsgGenerated != null) { OnMsgGenerated(this, new RptEventArgs("Running Import Towns")); }

            Work_ImportTowns w = new Work_ImportTowns();
            w.OnRptGenerated += new Work_ImportTowns.RptGeneratedHandler(tmp_ReportGenerated);
            w.OnLogGenerated += new Work_ImportTowns.LogGeneratedHandler(tmp_LogGenerated);
            w.Start(obj);

            if (OnMsgGenerated != null) { OnMsgGenerated(this, new RptEventArgs("Completed Import Towns")); }
        }

        private void Export() 
        {
        OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Access DB|*.mdb";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Thread Job = new Thread(new ParameterizedThreadStart(RunExportInBackground));
                WorkArgs wa = new WorkArgs();
                wa.KeyPairArgs.Add("filename", fd.FileName); 
                wa.KeyPairArgs.Add("dest", @"c:\RWH\csvdata\");
                Job.Start(wa);             
            }  
        }

        private void CallImportSites() 
        {
         OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Access DB|*.mdb";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Thread Job = new Thread(new ParameterizedThreadStart(RunSiteImportInBackground));
                WorkArgs wa = new WorkArgs();
                wa.KeyPairArgs.Add("filename", fd.FileName); 
                wa.KeyPairArgs.Add("Sitefile", @"c:\RWH\csvdata\sites.csv");
                wa.KeyPairArgs.Add("Offerfile", @"c:\RWH\csvdata\[Offer_Details].csv");

                Job.Start(wa);               
            }
        }

        private void CallImportContacts()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Access DB|*.mdb";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Thread Job = new Thread(new ParameterizedThreadStart(RunContactImportInBackground));
                WorkArgs wa = new WorkArgs();
                wa.KeyPairArgs.Add("filename", fd.FileName);
                wa.KeyPairArgs.Add("Contactfile", @"c:\RWH\csvdata\[Contacts_List_-_Main].csv");
            
                Job.Start(wa);
            }
        }

        private void CallImportTowns()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Access DB|*.mdb";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Thread Job = new Thread(new ParameterizedThreadStart(RunTownImportInBackground));
                WorkArgs wa = new WorkArgs();
                wa.KeyPairArgs.Add("filename", fd.FileName);
                wa.KeyPairArgs.Add("Townfile", @"c:\RWH\csvdata\[Town_Details].csv");

                Job.Start(wa);
            }
        }

        private void btnSyncDB_Click(object sender, EventArgs e)
        {
            Export();                      
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            CallImportSites();            
        }

        private void btnImportContacts_Click(object sender, EventArgs e)
        {
            CallImportContacts(); 
        }

        private void btnTowns_Click(object sender, EventArgs e)
        {
            CallImportTowns(); 
        }


    }
}

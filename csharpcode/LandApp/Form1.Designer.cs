namespace LandApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstStatusLog = new System.Windows.Forms.ListBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnSyncDB = new System.Windows.Forms.Button();
            this.btnImportContacts = new System.Windows.Forms.Button();
            this.btnTowns = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstStatusLog
            // 
            this.lstStatusLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstStatusLog.FormattingEnabled = true;
            this.lstStatusLog.Location = new System.Drawing.Point(1, 9);
            this.lstStatusLog.Name = "lstStatusLog";
            this.lstStatusLog.Size = new System.Drawing.Size(748, 498);
            this.lstStatusLog.TabIndex = 0;
            this.lstStatusLog.DoubleClick += new System.EventHandler(this.lstStatusLog_DoubleClick);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Location = new System.Drawing.Point(318, 518);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(124, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import Sites";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnSyncDB
            // 
            this.btnSyncDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSyncDB.Location = new System.Drawing.Point(12, 518);
            this.btnSyncDB.Name = "btnSyncDB";
            this.btnSyncDB.Size = new System.Drawing.Size(168, 23);
            this.btnSyncDB.TabIndex = 2;
            this.btnSyncDB.Text = "SyncDB : Extract";
            this.btnSyncDB.UseVisualStyleBackColor = true;
            this.btnSyncDB.Click += new System.EventHandler(this.btnSyncDB_Click);
            // 
            // btnImportContacts
            // 
            this.btnImportContacts.Location = new System.Drawing.Point(203, 518);
            this.btnImportContacts.Name = "btnImportContacts";
            this.btnImportContacts.Size = new System.Drawing.Size(109, 23);
            this.btnImportContacts.TabIndex = 3;
            this.btnImportContacts.Text = "Import Contacts";
            this.btnImportContacts.UseVisualStyleBackColor = true;
            this.btnImportContacts.Click += new System.EventHandler(this.btnImportContacts_Click);
            // 
            // btnTowns
            // 
            this.btnTowns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTowns.Location = new System.Drawing.Point(448, 518);
            this.btnTowns.Name = "btnTowns";
            this.btnTowns.Size = new System.Drawing.Size(124, 23);
            this.btnTowns.TabIndex = 4;
            this.btnTowns.Text = "Import Towns";
            this.btnTowns.UseVisualStyleBackColor = true;
            this.btnTowns.Click += new System.EventHandler(this.btnTowns_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 553);
            this.Controls.Add(this.btnTowns);
            this.Controls.Add(this.btnImportContacts);
            this.Controls.Add(this.btnSyncDB);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.lstStatusLog);
            this.Name = "Form1";
            this.Text = "Land Desktop";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstStatusLog;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSyncDB;
        private System.Windows.Forms.Button btnImportContacts;
        private System.Windows.Forms.Button btnTowns;
    }
}


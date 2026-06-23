using System;
using System.Drawing;
using System.Windows.Forms;

namespace APT
{
    partial class ReportTemplateMenuPanel
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.navPanel = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnNavBack = new System.Windows.Forms.Button();
            this.btnNavLogout = new System.Windows.Forms.Button();
            this.listTemplates = new System.Windows.Forms.ListView();
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUpdated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Title
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(20, 55);
            this.lblTitle.Size = new Size(600, 25);
            this.lblTitle.Text = "UC-06: ניהול תבניות דוחות כספיים (BS / P&L / Cash Flow)";
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;

            // Navigation bar
            this.navPanel.BackColor = System.Drawing.Color.LightGray;
            this.navPanel.Location = new System.Drawing.Point(0, 0);
            this.navPanel.Size = new System.Drawing.Size(1024, 50);
            this.btnHome.Location = new Point(10, 10); this.btnHome.Size = new Size(80, 30); this.btnHome.Text = "בית"; this.btnHome.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); this.btnHome.ForeColor = System.Drawing.Color.White; this.btnHome.UseVisualStyleBackColor = false; this.btnHome.Click += (s, e) => btnHome_Click(s, e);
            this.btnNavBack.Anchor = (AnchorStyles.Top | AnchorStyles.Right); this.btnNavBack.Location = new Point(934, 10); this.btnNavBack.Size = new Size(80, 30); this.btnNavBack.Text = "חזור"; this.btnNavBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); this.btnNavBack.ForeColor = System.Drawing.Color.White; this.btnNavBack.UseVisualStyleBackColor = false; this.btnNavBack.Click += (s, e) => btnNavBack_Click(s, e);
            this.btnNavLogout.Anchor = (AnchorStyles.Top | AnchorStyles.Right); this.btnNavLogout.Location = new Point(844, 10); this.btnNavLogout.Size = new Size(80, 30); this.btnNavLogout.Text = "יציאה"; this.btnNavLogout.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); this.btnNavLogout.ForeColor = System.Drawing.Color.White; this.btnNavLogout.UseVisualStyleBackColor = false; this.btnNavLogout.Click += (s, e) => btnNavLogout_Click(s, e);
            this.navPanel.Controls.AddRange(new Control[] { this.btnHome, this.btnNavBack, this.btnNavLogout });

            // Templates list
            this.listTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colType, this.colName, this.colUpdated });
            this.listTemplates.FullRowSelect = true;
            this.listTemplates.Location = new System.Drawing.Point(20, 90);
            this.listTemplates.Size = new System.Drawing.Size(600, 250);
            this.listTemplates.BackColor = System.Drawing.Color.White;
            this.listTemplates.UseCompatibleStateImageBehavior = false;
            this.listTemplates.View = System.Windows.Forms.View.Details;
            this.listTemplates.SelectedIndexChanged += (s, e) => listTemplates_SelectedIndexChanged(s, e);
            this.listTemplates.DoubleClick += (s, e) => listTemplates_DoubleClick(s, e);
            this.colType.Text = "סוג דוח"; this.colType.Width = 150;
            this.colName.Text = "שם התבנית"; this.colName.Width = 260;
            this.colUpdated.Text = "עודכן לאחרונה"; this.colUpdated.Width = 180;

            // Edit button
            this.btnEdit.Location = new Point(20, 355); this.btnEdit.Size = new Size(160, 40); this.btnEdit.Text = "ערוך תבנית";
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(33, 150, 243); this.btnEdit.ForeColor = System.Drawing.Color.White; this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += (s, e) => btnEdit_Click(s, e);

            this.Controls.AddRange(new Control[] { this.navPanel, this.lblTitle, this.listTemplates, this.btnEdit });
            this.Size = new System.Drawing.Size(1024, 768);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnNavBack;
        private System.Windows.Forms.Button btnNavLogout;
        private System.Windows.Forms.ListView listTemplates;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colUpdated;
        private System.Windows.Forms.Button btnEdit;
    }
}

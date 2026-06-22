using System;
using System.Drawing;
using System.Windows.Forms;

namespace APT
{
    partial class LedgerEntryPanel
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblTitle = new System.Windows.Forms.Label();

            this.navPanel = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnNavBack = new System.Windows.Forms.Button();
            this.btnNavLogout = new System.Windows.Forms.Button();

            this.listEntries = new System.Windows.Forms.ListView();
            this.colEntryId = new System.Windows.Forms.ColumnHeader();
            this.colCaseId = new System.Windows.Forms.ColumnHeader();
            this.colAccountCode = new System.Windows.Forms.ColumnHeader();
            this.colAccountType = new System.Windows.Forms.ColumnHeader();
            this.lblEntryId = new System.Windows.Forms.Label();
            this.lblCaseId = new System.Windows.Forms.Label();
            this.lblAccountCode = new System.Windows.Forms.Label();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.lblAccountType = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblReportingPeriod = new System.Windows.Forms.Label();
            this.lblIsNew = new System.Windows.Forms.Label();
            this.txtEntryId = new System.Windows.Forms.TextBox();
            this.txtCaseId = new System.Windows.Forms.TextBox();
            this.txtAccountCode = new System.Windows.Forms.TextBox();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.cmbAccountType = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.dtReportingPeriod = new System.Windows.Forms.DateTimePicker();
            this.chkIsNew = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Title Label
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(20, 55);
            this.lblTitle.Size = new Size(400, 25);
            this.lblTitle.Text = "UC-04: ערכי חשבונות - תמיכה בהתאמה צולבת";
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;

            // Navigation Bar
            this.navPanel.BackColor = System.Drawing.Color.LightGray;
            this.navPanel.Location = new System.Drawing.Point(0, 0);
            this.navPanel.Size = new System.Drawing.Size(1024, 50);
            this.btnHome.Location = new Point(10, 10); this.btnHome.Size = new Size(80, 30); this.btnHome.Text = "בית"; this.btnHome.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); this.btnHome.ForeColor = System.Drawing.Color.White; this.btnHome.UseVisualStyleBackColor = false; this.btnHome.Click += (s, e) => btnHome_Click(s, e); this.toolTip.SetToolTip(this.btnHome, "חזור לעמוד הבית");
            this.btnNavBack.Anchor = (AnchorStyles.Top | AnchorStyles.Right); this.btnNavBack.Location = new Point(934, 10); this.btnNavBack.Size = new Size(80, 30); this.btnNavBack.Text = "חזור"; this.btnNavBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); this.btnNavBack.ForeColor = System.Drawing.Color.White; this.btnNavBack.UseVisualStyleBackColor = false; this.btnNavBack.Click += (s, e) => btnNavBack_Click(s, e); this.toolTip.SetToolTip(this.btnNavBack, "חזור לעמוד הבית");
            this.btnNavLogout.Anchor = (AnchorStyles.Top | AnchorStyles.Right); this.btnNavLogout.Location = new Point(844, 10); this.btnNavLogout.Size = new Size(80, 30); this.btnNavLogout.Text = "יציאה"; this.btnNavLogout.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); this.btnNavLogout.ForeColor = System.Drawing.Color.White; this.btnNavLogout.UseVisualStyleBackColor = false; this.btnNavLogout.Click += (s, e) => btnNavLogout_Click(s, e); this.toolTip.SetToolTip(this.btnNavLogout, "התנתק והחזר ללוגין");
            this.navPanel.Controls.AddRange(new Control[] { this.btnHome, this.btnNavBack, this.btnNavLogout });

            this.listEntries.Columns.AddRange(new[] { colEntryId, colCaseId, colAccountCode, colAccountType });
            this.listEntries.FullRowSelect = true;
            this.listEntries.BackColor = System.Drawing.Color.White;
            this.listEntries.Location = new System.Drawing.Point(20, 85);
            this.listEntries.Size = new System.Drawing.Size(450, 200);
            this.listEntries.View = System.Windows.Forms.View.Details;
            this.listEntries.SelectedIndexChanged += (s, e) => listEntries_SelectedIndexChanged(s, e);

            colEntryId.Text = "מזהה"; colEntryId.Width = 60;
            colCaseId.Text = "תיק"; colCaseId.Width = 60;
            colAccountCode.Text = "קוד"; colAccountCode.Width = 80;
            colAccountType.Text = "סוג"; colAccountType.Width = 80;

            int y = 80;
            lblEntryId.AutoSize = true; lblEntryId.Location = new Point(700, y); lblEntryId.Text = "מזהה";
            txtEntryId.Location = new Point(580, y); txtEntryId.ReadOnly = true; txtEntryId.Size = new Size(100, 22); y += 30;

            lblCaseId.AutoSize = true; lblCaseId.Location = new Point(700, y); lblCaseId.Text = "תיק";
            txtCaseId.Location = new Point(580, y); txtCaseId.Size = new Size(100, 22); y += 30;

            lblAccountCode.AutoSize = true; lblAccountCode.Location = new Point(700, y); lblAccountCode.Text = "קוד חשבון";
            txtAccountCode.Location = new Point(580, y); txtAccountCode.Size = new Size(100, 22); y += 30;

            lblAccountName.AutoSize = true; lblAccountName.Location = new Point(700, y); lblAccountName.Text = "שם חשבון";
            txtAccountName.Location = new Point(580, y); txtAccountName.Size = new Size(100, 22); y += 30;

            lblAccountType.AutoSize = true; lblAccountType.Location = new Point(700, y); lblAccountType.Text = "סוג חשבון";
            cmbAccountType.Location = new Point(580, y); cmbAccountType.Size = new Size(100, 24); y += 30;

            lblAmount.AutoSize = true; lblAmount.Location = new Point(700, y); lblAmount.Text = "סכום";
            txtAmount.Location = new Point(580, y); txtAmount.Size = new Size(100, 22); y += 30;

            lblReportingPeriod.AutoSize = true; lblReportingPeriod.Location = new Point(700, y); lblReportingPeriod.Text = "תקופה";
            dtReportingPeriod.Location = new Point(580, y); dtReportingPeriod.Size = new Size(100, 22); y += 30;

            lblIsNew.AutoSize = true; lblIsNew.Location = new Point(700, y); lblIsNew.Text = "חדש";
            chkIsNew.Location = new Point(580, y); chkIsNew.Size = new Size(100, 24); y += 30;

            btnNew.Location = new Point(20, 310); btnNew.Size = new Size(80, 30); btnNew.Text = "חדש"; this.toolTip.SetToolTip(btnNew, "צור ערך חדש");
            btnSave.BackColor = System.Drawing.Color.FromArgb(76, 175, 80); btnSave.ForeColor = System.Drawing.Color.White; btnSave.UseVisualStyleBackColor = false; this.toolTip.SetToolTip(btnSave, "שמור ערך חדש");
            btnUpdate.UseVisualStyleBackColor = true; this.toolTip.SetToolTip(btnUpdate, "עדכן ערך");
            btnDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54); btnDelete.ForeColor = System.Drawing.Color.White; btnDelete.UseVisualStyleBackColor = false; this.toolTip.SetToolTip(btnDelete, "מחק ערך");
            btnBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); btnBack.ForeColor = System.Drawing.Color.White; btnBack.UseVisualStyleBackColor = false; this.toolTip.SetToolTip(btnBack, "חזור לעמוד הבית");
            btnNew.Click += (s, e) => btnNew_Click(s, e);
            btnSave.Location = new Point(110, 250); btnSave.Size = new Size(80, 30); btnSave.Text = "שמור";
            btnSave.Click += (s, e) => btnSave_Click(s, e);
            btnUpdate.Location = new Point(200, 250); btnUpdate.Size = new Size(80, 30); btnUpdate.Text = "עדכן";
            btnUpdate.Click += (s, e) => btnUpdate_Click(s, e);
            btnDelete.Location = new Point(290, 250); btnDelete.Size = new Size(80, 30); btnDelete.Text = "מחק";
            btnDelete.Click += (s, e) => btnDelete_Click(s, e);
btnBack.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnBack.Location = new Point(934, 10); btnBack.Size = new Size(80, 30); btnBack.Text = "חזור";
            btnBack.Click += (s, e) => btnBack_Click(s, e);

            this.Controls.AddRange(new Control[] { this.navPanel, this.lblTitle, listEntries, lblEntryId, txtEntryId, lblCaseId, txtCaseId, lblAccountCode, txtAccountCode,
                lblAccountName, txtAccountName, lblAccountType, cmbAccountType, lblAmount, txtAmount, lblReportingPeriod, dtReportingPeriod,
                lblIsNew, chkIsNew, btnNew, btnSave, btnUpdate, btnDelete, btnBack });

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(1024, 768);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListView listEntries;
        private System.Windows.Forms.ColumnHeader colEntryId;
        private System.Windows.Forms.ColumnHeader colCaseId;
        private System.Windows.Forms.ColumnHeader colAccountCode;
        private System.Windows.Forms.ColumnHeader colAccountType;
        private System.Windows.Forms.Label lblEntryId;
        private System.Windows.Forms.Label lblCaseId;
        private System.Windows.Forms.Label lblAccountCode;
        private System.Windows.Forms.Label lblAccountName;
        private System.Windows.Forms.Label lblAccountType;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblReportingPeriod;
        private System.Windows.Forms.Label lblIsNew;
        private System.Windows.Forms.TextBox txtEntryId;
        private System.Windows.Forms.TextBox txtCaseId;
        private System.Windows.Forms.TextBox txtAccountCode;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.ComboBox cmbAccountType;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.DateTimePicker dtReportingPeriod;
        private System.Windows.Forms.CheckBox chkIsNew;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnNavBack;
        private System.Windows.Forms.Button btnNavLogout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

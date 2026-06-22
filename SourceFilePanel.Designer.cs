using System;
using System.Drawing;
using System.Windows.Forms;

namespace APT
{
    partial class SourceFilePanel
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

            this.listFiles = new System.Windows.Forms.ListView();
            this.colFileId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCaseId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFileId = new System.Windows.Forms.Label();
            this.lblCaseId = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblFileType = new System.Windows.Forms.Label();
            this.lblUploadDate = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDataFormat = new System.Windows.Forms.Label();
            this.txtFileId = new System.Windows.Forms.TextBox();
            this.cmbCaseId = new System.Windows.Forms.ComboBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.dtUploadDate = new System.Windows.Forms.DateTimePicker();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbDataFormat = new System.Windows.Forms.ComboBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Title Label
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(20, 55);
            this.lblTitle.Size = new Size(400, 25);
            this.lblTitle.Text = "UC-02: העלאת קבצי מקור";
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

            this.listFiles.Columns.AddRange(new[] { this.colFileId, this.colCaseId, this.colFileName, this.colStatus });
            this.listFiles.FullRowSelect = true;
            this.listFiles.Location = new System.Drawing.Point(20, 85);
            this.listFiles.Size = new System.Drawing.Size(400, 200);
            this.listFiles.BackColor = System.Drawing.Color.White;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.View = System.Windows.Forms.View.Details;
            this.listFiles.SelectedIndexChanged += (s, e) => listFiles_SelectedIndexChanged(s, e);

            this.colFileId.Text = "מזהה"; this.colFileId.Width = 50;
            this.colCaseId.Text = "תיק"; this.colCaseId.Width = 50;
            this.colFileName.Text = "שם קובץ"; this.colFileName.Width = 150;
            this.colStatus.Text = "סטטוס"; this.colStatus.Width = 100;

            int y = 80;
            lblFileId.AutoSize = true; lblFileId.Location = new Point(700, y); lblFileId.Text = "מזהה";
            txtFileId.Location = new Point(580, y); txtFileId.ReadOnly = true; txtFileId.Size = new Size(100, 22); y += 30;

            lblCaseId.AutoSize = true; lblCaseId.Location = new Point(700, y); lblCaseId.Text = "תיק";
            cmbCaseId.Location = new Point(580, y); cmbCaseId.Size = new Size(100, 24); cmbCaseId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; y += 30;

            lblFileName.AutoSize = true; lblFileName.Location = new Point(700, y); lblFileName.Text = "שם קובץ";
            txtFileName.Location = new Point(580, y); txtFileName.Size = new Size(100, 22);
            btnBrowse.Location = new Point(475, y); btnBrowse.Size = new Size(95, 24); btnBrowse.Text = "עיון...";
            btnBrowse.BackColor = System.Drawing.Color.FromArgb(96, 125, 139); btnBrowse.ForeColor = System.Drawing.Color.White; btnBrowse.UseVisualStyleBackColor = false;
            btnBrowse.Click += (s, e) => btnBrowse_Click(s, e); this.toolTip.SetToolTip(btnBrowse, "בחר קובץ מהדיסק");
            y += 30;

            lblFileType.AutoSize = true; lblFileType.Location = new Point(700, y); lblFileType.Text = "סוג קובץ";
            cmbFileType.Location = new Point(580, y); cmbFileType.Size = new Size(100, 24); y += 30;

            lblUploadDate.AutoSize = true; lblUploadDate.Location = new Point(700, y); lblUploadDate.Text = "תאריך העלאה";
            dtUploadDate.Location = new Point(580, y); dtUploadDate.Size = new Size(100, 22); y += 30;

            lblStatus.AutoSize = true; lblStatus.Location = new Point(700, y); lblStatus.Text = "סטטוס";
            cmbStatus.Location = new Point(580, y); cmbStatus.Size = new Size(100, 24); y += 30;

            lblDataFormat.AutoSize = true; lblDataFormat.Location = new Point(700, y); lblDataFormat.Text = "פורמט";
            cmbDataFormat.Location = new Point(580, y); cmbDataFormat.Size = new Size(100, 24); y += 30;

            btnNew.Location = new Point(20, 310); btnNew.Size = new Size(120, 35); btnNew.Text = "חדש";
            btnNew.Click += (s, e) => btnNew_Click(s, e); this.toolTip.SetToolTip(btnNew, "צור קובץ חדש");

            btnSave.Location = new Point(20, 355); btnSave.Size = new Size(120, 35); btnSave.Text = "שמור";
            btnSave.BackColor = System.Drawing.Color.FromArgb(76, 175, 80); btnSave.ForeColor = System.Drawing.Color.White; btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += (s, e) => btnSave_Click(s, e); this.toolTip.SetToolTip(btnSave, "שמור קובץ חדש");

            btnUpdate.Location = new Point(20, 400); btnUpdate.Size = new Size(120, 35); btnUpdate.Text = "עדכן";
            btnUpdate.BackColor = System.Drawing.Color.FromArgb(33, 150, 243); btnUpdate.ForeColor = System.Drawing.Color.White; btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += (s, e) => btnUpdate_Click(s, e); this.toolTip.SetToolTip(btnUpdate, "עדכן סטטוס");

            btnDelete.Location = new Point(20, 445); btnDelete.Size = new Size(120, 35); btnDelete.Text = "מחק";
            btnDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54); btnDelete.ForeColor = System.Drawing.Color.White; btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += (s, e) => btnDelete_Click(s, e); this.toolTip.SetToolTip(btnDelete, "מחק קובץ");

            btnBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); btnBack.ForeColor = System.Drawing.Color.White; btnBack.UseVisualStyleBackColor = false; this.toolTip.SetToolTip(btnBack, "חזור לעמוד הבית");
btnBack.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnBack.Location = new Point(934, 10); btnBack.Size = new Size(80, 30); btnBack.Text = "חזור";
            btnBack.Click += (s, e) => btnBack_Click(s, e);

            this.Controls.AddRange(new Control[] { this.navPanel, this.lblTitle, listFiles, lblFileId, txtFileId, lblCaseId, cmbCaseId, lblFileName, txtFileName, btnBrowse,
                lblFileType, cmbFileType, lblUploadDate, dtUploadDate, lblStatus, cmbStatus, lblDataFormat, cmbDataFormat,
                btnNew, btnSave, btnUpdate, btnDelete, btnBack });

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(1024, 768);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListView listFiles;
        private System.Windows.Forms.ColumnHeader colFileId;
        private System.Windows.Forms.ColumnHeader colCaseId;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Label lblFileId;
        private System.Windows.Forms.Label lblCaseId;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.Label lblUploadDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDataFormat;
        private System.Windows.Forms.TextBox txtFileId;
        private System.Windows.Forms.ComboBox cmbCaseId;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.DateTimePicker dtUploadDate;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbDataFormat;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnBrowse;
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

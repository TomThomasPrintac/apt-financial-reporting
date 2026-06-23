using System;
using System.Drawing;
using System.Windows.Forms;

namespace APT
{
    partial class FinancialReportPanel
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

            this.listReports = new System.Windows.Forms.ListView();
            this.colReportId = new System.Windows.Forms.ColumnHeader();
            this.colCaseId = new System.Windows.Forms.ColumnHeader();
            this.colReportType = new System.Windows.Forms.ColumnHeader();
            this.colSigned = new System.Windows.Forms.ColumnHeader();
            this.lblReportId = new System.Windows.Forms.Label();
            this.lblCaseId = new System.Windows.Forms.Label();
            this.lblReportType = new System.Windows.Forms.Label();
            this.lblReportFormat = new System.Windows.Forms.Label();
            this.lblGeneratedDate = new System.Windows.Forms.Label();
            this.lblIsSigned = new System.Windows.Forms.Label();
            this.lblSignedDate = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSeniorAccountantId = new System.Windows.Forms.Label();
            this.txtReportId = new System.Windows.Forms.TextBox();
            this.txtCaseId = new System.Windows.Forms.TextBox();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.cmbReportFormat = new System.Windows.Forms.ComboBox();
            this.dtGeneratedDate = new System.Windows.Forms.DateTimePicker();
            this.chkIsSigned = new System.Windows.Forms.CheckBox();
            this.dtSignedDate = new System.Windows.Forms.DateTimePicker();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtSeniorAccountantId = new System.Windows.Forms.TextBox();
            this.lblPeriodStart = new System.Windows.Forms.Label();
            this.dtPeriodStart = new System.Windows.Forms.DateTimePicker();
            this.lblPeriodEnd = new System.Windows.Forms.Label();
            this.dtPeriodEnd = new System.Windows.Forms.DateTimePicker();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Title Label
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(20, 55);
            this.lblTitle.Size = new Size(400, 25);
            this.lblTitle.Text = "UC-05: יצירת דוחות - דוחות כספיים";
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

            this.listReports.Columns.AddRange(new[] { colReportId, colCaseId, colReportType, colSigned });
            this.listReports.FullRowSelect = true;
            this.listReports.BackColor = System.Drawing.Color.White;
            this.listReports.Location = new System.Drawing.Point(20, 85);
            this.listReports.Size = new System.Drawing.Size(450, 200);
            this.listReports.View = System.Windows.Forms.View.Details;
            this.listReports.SelectedIndexChanged += (s, e) => listReports_SelectedIndexChanged(s, e);

            colReportId.Text = "דוח"; colReportId.Width = 60;
            colCaseId.Text = "תיק"; colCaseId.Width = 60;
            colReportType.Text = "סוג"; colReportType.Width = 100;
            colSigned.Text = "חתימה"; colSigned.Width = 80;

            int y = 80;
            lblReportId.AutoSize = true; lblReportId.Location = new Point(700, y); lblReportId.Text = "דוח";
            txtReportId.Location = new Point(580, y); txtReportId.ReadOnly = true; txtReportId.Size = new Size(100, 22); y += 30;

            lblCaseId.AutoSize = true; lblCaseId.Location = new Point(700, y); lblCaseId.Text = "תיק";
            txtCaseId.Location = new Point(580, y); txtCaseId.Size = new Size(100, 22); y += 30;

            lblReportType.AutoSize = true; lblReportType.Location = new Point(700, y); lblReportType.Text = "סוג דוח";
            cmbReportType.Location = new Point(580, y); cmbReportType.Size = new Size(100, 24); y += 30;

            lblReportFormat.AutoSize = true; lblReportFormat.Location = new Point(700, y); lblReportFormat.Text = "פורמט";
            cmbReportFormat.Location = new Point(580, y); cmbReportFormat.Size = new Size(100, 24); y += 30;

            lblGeneratedDate.AutoSize = true; lblGeneratedDate.Location = new Point(700, y); lblGeneratedDate.Text = "תאריך יצירה";
            dtGeneratedDate.Location = new Point(580, y); dtGeneratedDate.Size = new Size(100, 22); y += 30;

            lblIsSigned.AutoSize = true; lblIsSigned.Location = new Point(700, y); lblIsSigned.Text = "חתום";
            chkIsSigned.Location = new Point(580, y); chkIsSigned.Size = new Size(100, 24); y += 30;

            lblSignedDate.AutoSize = true; lblSignedDate.Location = new Point(700, y); lblSignedDate.Text = "תאריך חתימה";
            dtSignedDate.Location = new Point(580, y); dtSignedDate.Size = new Size(100, 22); y += 30;

            lblStatus.AutoSize = true; lblStatus.Location = new Point(700, y); lblStatus.Text = "סטטוס";
            cmbStatus.Location = new Point(580, y); cmbStatus.Size = new Size(100, 24); y += 30;

            lblSeniorAccountantId.AutoSize = true; lblSeniorAccountantId.Location = new Point(700, y); lblSeniorAccountantId.Text = "רואה חשבון";
            txtSeniorAccountantId.Location = new Point(580, y); txtSeniorAccountantId.ReadOnly = true; txtSeniorAccountantId.Size = new Size(100, 22); y += 30;

            lblPeriodStart.AutoSize = true; lblPeriodStart.Location = new Point(700, y); lblPeriodStart.Text = "תקופה מתאריך";
            dtPeriodStart.Location = new Point(560, y); dtPeriodStart.Size = new Size(120, 22); dtPeriodStart.Format = System.Windows.Forms.DateTimePickerFormat.Short; y += 30;

            lblPeriodEnd.AutoSize = true; lblPeriodEnd.Location = new Point(700, y); lblPeriodEnd.Text = "תקופה עד תאריך";
            dtPeriodEnd.Location = new Point(560, y); dtPeriodEnd.Size = new Size(120, 22); dtPeriodEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short; y += 30;

            btnNew.Location = new Point(20, 310); btnNew.Size = new Size(120, 35); btnNew.Text = "חדש";
            btnNew.Click += (s, e) => btnNew_Click(s, e); this.toolTip.SetToolTip(btnNew, "צור דוח חדש");

            btnSave.Location = new Point(20, 355); btnSave.Size = new Size(120, 35); btnSave.Text = "שמור";
            btnSave.BackColor = System.Drawing.Color.FromArgb(76, 175, 80); btnSave.ForeColor = System.Drawing.Color.White; btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += (s, e) => btnSave_Click(s, e); this.toolTip.SetToolTip(btnSave, "שמור דוח חדש");

            btnUpdate.Location = new Point(20, 400); btnUpdate.Size = new Size(120, 35); btnUpdate.Text = "עדכן";
            btnUpdate.BackColor = System.Drawing.Color.FromArgb(33, 150, 243); btnUpdate.ForeColor = System.Drawing.Color.White; btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += (s, e) => btnUpdate_Click(s, e); this.toolTip.SetToolTip(btnUpdate, "עדכן דוח");

            btnDelete.Location = new Point(20, 445); btnDelete.Size = new Size(120, 35); btnDelete.Text = "מחק";
            btnDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54); btnDelete.ForeColor = System.Drawing.Color.White; btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += (s, e) => btnDelete_Click(s, e); this.toolTip.SetToolTip(btnDelete, "מחק דוח");

            btnSign.Location = new Point(20, 490); btnSign.Size = new Size(120, 35); btnSign.Text = "חתום";
            btnSign.BackColor = System.Drawing.Color.FromArgb(63, 81, 181); btnSign.ForeColor = System.Drawing.Color.White; btnSign.UseVisualStyleBackColor = false;
            btnSign.Click += (s, e) => btnSign_Click(s, e); this.toolTip.SetToolTip(btnSign, "חתום על דוח");

            btnBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); btnBack.ForeColor = System.Drawing.Color.White; btnBack.UseVisualStyleBackColor = false; this.toolTip.SetToolTip(btnBack, "חזור לעמוד הבית");
btnBack.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnBack.Location = new Point(934, 10); btnBack.Size = new Size(70, 30); btnBack.Text = "חזור";
            btnBack.Click += (s, e) => btnBack_Click(s, e);

            this.Controls.AddRange(new Control[] { this.navPanel, this.lblTitle, listReports, lblReportId, txtReportId, lblCaseId, txtCaseId, lblReportType, cmbReportType,
                lblReportFormat, cmbReportFormat, lblGeneratedDate, dtGeneratedDate, lblIsSigned, chkIsSigned, lblSignedDate, dtSignedDate,
                lblStatus, cmbStatus, lblSeniorAccountantId, txtSeniorAccountantId, lblPeriodStart, dtPeriodStart, lblPeriodEnd, dtPeriodEnd, btnNew, btnSave, btnUpdate, btnDelete, btnSign, btnBack });

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(1024, 768);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListView listReports;
        private System.Windows.Forms.ColumnHeader colReportId;
        private System.Windows.Forms.ColumnHeader colCaseId;
        private System.Windows.Forms.ColumnHeader colReportType;
        private System.Windows.Forms.ColumnHeader colSigned;
        private System.Windows.Forms.Label lblReportId;
        private System.Windows.Forms.Label lblCaseId;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.Label lblReportFormat;
        private System.Windows.Forms.Label lblGeneratedDate;
        private System.Windows.Forms.Label lblIsSigned;
        private System.Windows.Forms.Label lblSignedDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSeniorAccountantId;
        private System.Windows.Forms.TextBox txtReportId;
        private System.Windows.Forms.TextBox txtCaseId;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.ComboBox cmbReportFormat;
        private System.Windows.Forms.DateTimePicker dtGeneratedDate;
        private System.Windows.Forms.CheckBox chkIsSigned;
        private System.Windows.Forms.DateTimePicker dtSignedDate;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtSeniorAccountantId;
        private System.Windows.Forms.Label lblPeriodStart;
        private System.Windows.Forms.DateTimePicker dtPeriodStart;
        private System.Windows.Forms.Label lblPeriodEnd;
        private System.Windows.Forms.DateTimePicker dtPeriodEnd;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnNavBack;
        private System.Windows.Forms.Button btnNavLogout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

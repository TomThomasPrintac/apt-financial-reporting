using System;
using System.Drawing;
using System.Windows.Forms;

namespace APT
{
    partial class PayrollLedgerMatchPanel
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

            this.listMatches = new System.Windows.Forms.ListView();
            this.colMatchId = new System.Windows.Forms.ColumnHeader();
            this.colPayrollId = new System.Windows.Forms.ColumnHeader();
            this.colEntryId = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.lblMatchId = new System.Windows.Forms.Label();
            this.lblPayrollId = new System.Windows.Forms.Label();
            this.lblEntryId = new System.Windows.Forms.Label();
            this.lblCaseId = new System.Windows.Forms.Label();
            this.lblMatchStatus = new System.Windows.Forms.Label();
            this.lblVariance = new System.Windows.Forms.Label();
            this.lblMatchedDate = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtMatchId = new System.Windows.Forms.TextBox();
            this.txtPayrollId = new System.Windows.Forms.TextBox();
            this.txtEntryId = new System.Windows.Forms.TextBox();
            this.txtCaseId = new System.Windows.Forms.TextBox();
            this.cmbMatchStatus = new System.Windows.Forms.ComboBox();
            this.txtVariance = new System.Windows.Forms.TextBox();
            this.dtMatchedDate = new System.Windows.Forms.DateTimePicker();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRunCrossMatch = new System.Windows.Forms.Button();
            this.lblVarianceThreshold = new System.Windows.Forms.Label();
            this.txtVarianceThreshold = new System.Windows.Forms.TextBox();
            this.btnUpdateThreshold = new System.Windows.Forms.Button();
            this.lblVarianceSummary = new System.Windows.Forms.Label();
            this.lblReportingPeriod = new System.Windows.Forms.Label();
            this.dtReportingPeriod = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();

            // Title Label
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(20, 55);
            this.lblTitle.Size = new Size(400, 25);
            this.lblTitle.Text = "UC-04: התאמה צולבת - משכורה לחשבונות";
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

            this.listMatches.Columns.AddRange(new[] { colMatchId, colPayrollId, colEntryId, colStatus });
            this.listMatches.FullRowSelect = true;
            this.listMatches.BackColor = System.Drawing.Color.White;
            this.listMatches.Location = new System.Drawing.Point(20, 85);
            this.listMatches.Size = new System.Drawing.Size(450, 200);
            this.listMatches.View = System.Windows.Forms.View.Details;
            this.listMatches.SelectedIndexChanged += (s, e) => listMatches_SelectedIndexChanged(s, e);

            colMatchId.Text = "התאמה"; colMatchId.Width = 60;
            colPayrollId.Text = "משכורת"; colPayrollId.Width = 60;
            colEntryId.Text = "ערך"; colEntryId.Width = 60;
            colStatus.Text = "סטטוס"; colStatus.Width = 100;

            int y = 80;
            lblMatchId.AutoSize = true; lblMatchId.Location = new Point(700, y); lblMatchId.Text = "התאמה";
            txtMatchId.Location = new Point(580, y); txtMatchId.ReadOnly = true; txtMatchId.Size = new Size(100, 22); y += 30;

            lblPayrollId.AutoSize = true; lblPayrollId.Location = new Point(700, y); lblPayrollId.Text = "משכורת";
            txtPayrollId.Location = new Point(580, y); txtPayrollId.Size = new Size(100, 22); y += 30;

            lblEntryId.AutoSize = true; lblEntryId.Location = new Point(700, y); lblEntryId.Text = "ערך";
            txtEntryId.Location = new Point(580, y); txtEntryId.Size = new Size(100, 22); y += 30;

            lblCaseId.AutoSize = true; lblCaseId.Location = new Point(700, y); lblCaseId.Text = "תיק";
            txtCaseId.Location = new Point(580, y); txtCaseId.Size = new Size(100, 22); y += 30;  // ניתן לעריכה — נדרש להזנת תיק להצלבה

            lblMatchStatus.AutoSize = true; lblMatchStatus.Location = new Point(700, y); lblMatchStatus.Text = "סטטוס";
            cmbMatchStatus.Location = new Point(580, y); cmbMatchStatus.Size = new Size(100, 24); y += 30;

            lblVariance.AutoSize = true; lblVariance.Location = new Point(700, y); lblVariance.Text = "סטיה";
            txtVariance.Location = new Point(580, y); txtVariance.Size = new Size(100, 22); y += 30;

            lblMatchedDate.AutoSize = true; lblMatchedDate.Location = new Point(700, y); lblMatchedDate.Text = "תאריך";
            dtMatchedDate.Location = new Point(580, y); dtMatchedDate.Size = new Size(100, 22); y += 30;

            lblNotes.AutoSize = true; lblNotes.Location = new Point(700, y); lblNotes.Text = "הערות";
            txtNotes.Location = new Point(580, y); txtNotes.Multiline = true; txtNotes.Size = new Size(100, 50); y += 60;

            // Reporting Period
            lblReportingPeriod.AutoSize = true; lblReportingPeriod.Location = new Point(700, 80); lblReportingPeriod.Text = "תקופה";
            dtReportingPeriod.Location = new Point(580, 80); dtReportingPeriod.Size = new Size(100, 22);

            // Run Cross-Match button
            btnRunCrossMatch.Location = new Point(20, 330); btnRunCrossMatch.Size = new Size(120, 35); btnRunCrossMatch.Text = "הרץ התאמה צולבת";
            btnRunCrossMatch.BackColor = System.Drawing.Color.LimeGreen; btnRunCrossMatch.ForeColor = System.Drawing.Color.White;
            btnRunCrossMatch.Click += (s, e) => btnRunCrossMatch_Click(s, e);

            // Variance Threshold
            lblVarianceThreshold.AutoSize = true; lblVarianceThreshold.Location = new Point(20, 375); lblVarianceThreshold.Text = "סף סטיה:";
            txtVarianceThreshold.Location = new Point(100, 370); txtVarianceThreshold.Text = "100"; txtVarianceThreshold.Size = new Size(60, 22);
            btnUpdateThreshold.Location = new Point(170, 370); btnUpdateThreshold.Size = new Size(80, 22); btnUpdateThreshold.Text = "עדכן";
            btnUpdateThreshold.Click += (s, e) => btnUpdateThreshold_Click(s, e);

            // Variance Summary
            lblVarianceSummary.AutoSize = false; lblVarianceSummary.Location = new Point(20, 400); lblVarianceSummary.Size = new Size(400, 30);
            lblVarianceSummary.Text = "סה\"כ סטיה: -"; lblVarianceSummary.BackColor = System.Drawing.Color.LightYellow;
            lblVarianceSummary.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            // CRUD Buttons
            btnNew.Location = new Point(20, 310); btnNew.Size = new Size(120, 35); btnNew.Text = "חדש";
            btnNew.Click += (s, e) => btnNew_Click(s, e); this.toolTip.SetToolTip(btnNew, "צור התאמה חדשה");

            btnSave.Location = new Point(20, 355); btnSave.Size = new Size(120, 35); btnSave.Text = "שמור";
            btnSave.BackColor = System.Drawing.Color.FromArgb(76, 175, 80); btnSave.ForeColor = System.Drawing.Color.White; btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += (s, e) => btnSave_Click(s, e); this.toolTip.SetToolTip(btnSave, "שמור התאמה חדשה");

            btnUpdate.Location = new Point(20, 400); btnUpdate.Size = new Size(120, 35); btnUpdate.Text = "עדכן";
            btnUpdate.BackColor = System.Drawing.Color.FromArgb(33, 150, 243); btnUpdate.ForeColor = System.Drawing.Color.White; btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += (s, e) => btnUpdate_Click(s, e); this.toolTip.SetToolTip(btnUpdate, "עדכן התאמה");

            btnDelete.Location = new Point(20, 445); btnDelete.Size = new Size(120, 35); btnDelete.Text = "מחק";
            btnDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54); btnDelete.ForeColor = System.Drawing.Color.White; btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += (s, e) => btnDelete_Click(s, e); this.toolTip.SetToolTip(btnDelete, "מחק התאמה");

            btnBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); btnBack.ForeColor = System.Drawing.Color.White; btnBack.UseVisualStyleBackColor = false; this.toolTip.SetToolTip(btnBack, "חזור לעמוד הבית");
btnBack.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            btnBack.Location = new Point(934, 10); btnBack.Size = new Size(80, 30); btnBack.Text = "חזור";
            btnBack.Click += (s, e) => btnBack_Click(s, e);

            this.Controls.AddRange(new Control[] { this.navPanel, this.lblTitle, listMatches, lblMatchId, txtMatchId, lblPayrollId, txtPayrollId, lblEntryId, txtEntryId,
                lblCaseId, txtCaseId, lblMatchStatus, cmbMatchStatus, lblVariance, txtVariance, lblMatchedDate, dtMatchedDate,
                lblNotes, txtNotes, lblReportingPeriod, dtReportingPeriod, btnRunCrossMatch, lblVarianceThreshold, txtVarianceThreshold, btnUpdateThreshold,
                lblVarianceSummary, btnNew, btnSave, btnUpdate, btnDelete, btnBack });

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(1024, 768);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListView listMatches;
        private System.Windows.Forms.ColumnHeader colMatchId;
        private System.Windows.Forms.ColumnHeader colPayrollId;
        private System.Windows.Forms.ColumnHeader colEntryId;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Label lblMatchId;
        private System.Windows.Forms.Label lblPayrollId;
        private System.Windows.Forms.Label lblEntryId;
        private System.Windows.Forms.Label lblCaseId;
        private System.Windows.Forms.Label lblMatchStatus;
        private System.Windows.Forms.Label lblVariance;
        private System.Windows.Forms.Label lblMatchedDate;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtMatchId;
        private System.Windows.Forms.TextBox txtPayrollId;
        private System.Windows.Forms.TextBox txtEntryId;
        private System.Windows.Forms.TextBox txtCaseId;
        private System.Windows.Forms.ComboBox cmbMatchStatus;
        private System.Windows.Forms.TextBox txtVariance;
        private System.Windows.Forms.DateTimePicker dtMatchedDate;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnNavBack;
        private System.Windows.Forms.Button btnNavLogout;
        private System.Windows.Forms.Button btnRunCrossMatch;
        private System.Windows.Forms.Label lblVarianceThreshold;
        private System.Windows.Forms.TextBox txtVarianceThreshold;
        private System.Windows.Forms.Button btnUpdateThreshold;
        private System.Windows.Forms.Label lblVarianceSummary;
        private System.Windows.Forms.Label lblReportingPeriod;
        private System.Windows.Forms.DateTimePicker dtReportingPeriod;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

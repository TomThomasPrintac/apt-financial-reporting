using System;
using System.Drawing;
using System.Windows.Forms;

namespace APT
{
    partial class ReportTemplateEditorPanel
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
            this.lblLastUpdated = new System.Windows.Forms.Label();
            this.navPanel = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnNavBack = new System.Windows.Forms.Button();
            this.btnNavLogout = new System.Windows.Forms.Button();
            this.listFields = new System.Windows.Forms.ListView();
            this.colField = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFieldName = new System.Windows.Forms.Label();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.btnAddField = new System.Windows.Forms.Button();
            this.btnUpdateField = new System.Windows.Forms.Button();
            this.btnDeleteField = new System.Windows.Forms.Button();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.btnGeneratePdf = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Title + last-updated
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new Point(20, 55);
            this.lblTitle.Size = new Size(700, 25);
            this.lblTitle.Text = "עריכת תבנית";
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;

            this.lblLastUpdated.AutoSize = false;
            this.lblLastUpdated.Location = new Point(20, 82);
            this.lblLastUpdated.Size = new Size(400, 20);
            this.lblLastUpdated.Text = "עודכן לאחרונה: -";
            this.lblLastUpdated.ForeColor = System.Drawing.Color.DimGray;

            // Navigation bar
            this.navPanel.BackColor = System.Drawing.Color.LightGray;
            this.navPanel.Location = new System.Drawing.Point(0, 0);
            this.navPanel.Size = new System.Drawing.Size(1024, 50);
            this.btnHome.Location = new Point(10, 10); this.btnHome.Size = new Size(80, 30); this.btnHome.Text = "בית"; this.btnHome.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); this.btnHome.ForeColor = System.Drawing.Color.White; this.btnHome.UseVisualStyleBackColor = false; this.btnHome.Click += (s, e) => btnHome_Click(s, e);
            this.btnNavBack.Anchor = (AnchorStyles.Top | AnchorStyles.Right); this.btnNavBack.Location = new Point(934, 10); this.btnNavBack.Size = new Size(80, 30); this.btnNavBack.Text = "חזור"; this.btnNavBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); this.btnNavBack.ForeColor = System.Drawing.Color.White; this.btnNavBack.UseVisualStyleBackColor = false; this.btnNavBack.Click += (s, e) => btnNavBack_Click(s, e);
            this.btnNavLogout.Anchor = (AnchorStyles.Top | AnchorStyles.Right); this.btnNavLogout.Location = new Point(844, 10); this.btnNavLogout.Size = new Size(80, 30); this.btnNavLogout.Text = "יציאה"; this.btnNavLogout.BackColor = System.Drawing.Color.FromArgb(158, 158, 158); this.btnNavLogout.ForeColor = System.Drawing.Color.White; this.btnNavLogout.UseVisualStyleBackColor = false; this.btnNavLogout.Click += (s, e) => btnNavLogout_Click(s, e);
            this.navPanel.Controls.AddRange(new Control[] { this.btnHome, this.btnNavBack, this.btnNavLogout });

            // Fields list ("category of fields")
            this.listFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colField });
            this.listFields.FullRowSelect = true;
            this.listFields.Location = new System.Drawing.Point(20, 115);
            this.listFields.Size = new System.Drawing.Size(350, 300);
            this.listFields.BackColor = System.Drawing.Color.White;
            this.listFields.UseCompatibleStateImageBehavior = false;
            this.listFields.View = System.Windows.Forms.View.Details;
            this.listFields.SelectedIndexChanged += (s, e) => listFields_SelectedIndexChanged(s, e);
            this.colField.Text = "שדה בתבנית"; this.colField.Width = 330;

            // Field name input
            this.lblFieldName.AutoSize = true; this.lblFieldName.Location = new Point(500, 120); this.lblFieldName.Text = "שם שדה:";
            this.txtFieldName.Location = new Point(400, 145); this.txtFieldName.Size = new Size(180, 22);

            // Buttons
            this.btnAddField.Location = new Point(400, 185); this.btnAddField.Size = new Size(180, 35); this.btnAddField.Text = "הוסף שדה";
            this.btnAddField.BackColor = System.Drawing.Color.FromArgb(76, 175, 80); this.btnAddField.ForeColor = System.Drawing.Color.White; this.btnAddField.UseVisualStyleBackColor = false;
            this.btnAddField.Click += (s, e) => btnAddField_Click(s, e);

            this.btnUpdateField.Location = new Point(400, 225); this.btnUpdateField.Size = new Size(180, 35); this.btnUpdateField.Text = "עדכן שדה";
            this.btnUpdateField.BackColor = System.Drawing.Color.FromArgb(33, 150, 243); this.btnUpdateField.ForeColor = System.Drawing.Color.White; this.btnUpdateField.UseVisualStyleBackColor = false;
            this.btnUpdateField.Click += (s, e) => btnUpdateField_Click(s, e);

            this.btnDeleteField.Location = new Point(400, 265); this.btnDeleteField.Size = new Size(180, 35); this.btnDeleteField.Text = "מחק שדה";
            this.btnDeleteField.BackColor = System.Drawing.Color.FromArgb(244, 67, 54); this.btnDeleteField.ForeColor = System.Drawing.Color.White; this.btnDeleteField.UseVisualStyleBackColor = false;
            this.btnDeleteField.Click += (s, e) => btnDeleteField_Click(s, e);

            this.btnSaveTemplate.Location = new Point(400, 320); this.btnSaveTemplate.Size = new Size(180, 45); this.btnSaveTemplate.Text = "שמור תבנית";
            this.btnSaveTemplate.BackColor = System.Drawing.Color.FromArgb(63, 81, 181); this.btnSaveTemplate.ForeColor = System.Drawing.Color.White; this.btnSaveTemplate.UseVisualStyleBackColor = false;
            this.btnSaveTemplate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveTemplate.Click += (s, e) => btnSaveTemplate_Click(s, e);

            this.btnGeneratePdf.Location = new Point(400, 375); this.btnGeneratePdf.Size = new Size(180, 45); this.btnGeneratePdf.Text = "צור PDF (תצוגה)";
            this.btnGeneratePdf.BackColor = System.Drawing.Color.FromArgb(211, 47, 47); this.btnGeneratePdf.ForeColor = System.Drawing.Color.White; this.btnGeneratePdf.UseVisualStyleBackColor = false;
            this.btnGeneratePdf.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnGeneratePdf.Click += (s, e) => btnGeneratePdf_Click(s, e);

            this.Controls.AddRange(new Control[] { this.navPanel, this.lblTitle, this.lblLastUpdated, this.listFields,
                this.lblFieldName, this.txtFieldName, this.btnAddField, this.btnUpdateField, this.btnDeleteField, this.btnSaveTemplate, this.btnGeneratePdf });
            this.Size = new System.Drawing.Size(1024, 768);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLastUpdated;
        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnNavBack;
        private System.Windows.Forms.Button btnNavLogout;
        private System.Windows.Forms.ListView listFields;
        private System.Windows.Forms.ColumnHeader colField;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.Button btnUpdateField;
        private System.Windows.Forms.Button btnDeleteField;
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.Button btnGeneratePdf;
    }
}

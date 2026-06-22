namespace APT
{
    partial class ClientCasePanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);

            // Title Label
            this.lblTitle = new System.Windows.Forms.Label();

            // Navigation Bar Panel
            this.navPanel = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnNavBack = new System.Windows.Forms.Button();
            this.btnNavLogout = new System.Windows.Forms.Button();

            this.listCases = new System.Windows.Forms.ListView();
            this.colCaseId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClientId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReportingPeriod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCaseId = new System.Windows.Forms.Label();
            this.lblClientId = new System.Windows.Forms.Label();
            this.lblReportingPeriod = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCreatedDate = new System.Windows.Forms.Label();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.lblAssignedTo = new System.Windows.Forms.Label();
            this.txtCaseId = new System.Windows.Forms.TextBox();
            this.txtClientId = new System.Windows.Forms.TextBox();
            this.dtReportingPeriod = new System.Windows.Forms.DateTimePicker();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.dtCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.dtDueDate = new System.Windows.Forms.DateTimePicker();
            this.txtAssignedTo = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Navigation Bar Panel
            this.navPanel.BackColor = System.Drawing.Color.LightGray;
            this.navPanel.Location = new System.Drawing.Point(0, 0);
            this.navPanel.Size = new System.Drawing.Size(1024, 50);
            this.navPanel.Name = "navPanel";

            // Home Button
            this.btnHome.Location = new System.Drawing.Point(10, 10);
            this.btnHome.Size = new System.Drawing.Size(80, 30);
            this.btnHome.Text = "בית";
            this.btnHome.Name = "btnHome";
            this.btnHome.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            this.toolTip.SetToolTip(this.btnHome, "חזור לעמוד הבית");

            // Back Button
            this.btnNavBack.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnNavBack.Location = new System.Drawing.Point(934, 10);
            this.btnNavBack.Size = new System.Drawing.Size(80, 30);
            this.btnNavBack.Text = "חזור";
            this.btnNavBack.Name = "btnNavBack";
            this.btnNavBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnNavBack.ForeColor = System.Drawing.Color.White;
            this.btnNavBack.UseVisualStyleBackColor = false;
            this.btnNavBack.Click += new System.EventHandler(this.btnNavBack_Click);
            this.toolTip.SetToolTip(this.btnNavBack, "חזור לעמוד הבית");

            // Logout Button
            this.btnNavLogout.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnNavLogout.Location = new System.Drawing.Point(844, 10);
            this.btnNavLogout.Size = new System.Drawing.Size(80, 30);
            this.btnNavLogout.Text = "יציאה";
            this.btnNavLogout.Name = "btnNavLogout";
            this.btnNavLogout.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnNavLogout.ForeColor = System.Drawing.Color.White;
            this.btnNavLogout.UseVisualStyleBackColor = false;
            this.btnNavLogout.Click += new System.EventHandler(this.btnNavLogout_Click);
            this.toolTip.SetToolTip(this.btnNavLogout, "התנתק והחזר ללוגין");

            this.navPanel.Controls.Add(this.btnHome);
            this.navPanel.Controls.Add(this.btnNavBack);
            this.navPanel.Controls.Add(this.btnNavLogout);

            // Title Label
            this.lblTitle.AutoSize = false;
            this.lblTitle.Location = new System.Drawing.Point(20, 55);
            this.lblTitle.Size = new System.Drawing.Size(400, 25);
            this.lblTitle.Text = "ניהול תיקים - רשומות לקוחות";
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Name = "lblTitle";

            // listCases
            this.listCases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCaseId,
            this.colClientId,
            this.colReportingPeriod,
            this.colStatus});
            this.listCases.FullRowSelect = true;
            this.listCases.Location = new System.Drawing.Point(20, 85);
            this.listCases.Name = "listCases";
            this.listCases.Size = new System.Drawing.Size(400, 200);
            this.listCases.TabIndex = 0;
            this.listCases.UseCompatibleStateImageBehavior = false;
            this.listCases.View = System.Windows.Forms.View.Details;
            this.listCases.BackColor = System.Drawing.Color.White;
            this.listCases.SelectedIndexChanged += new System.EventHandler(this.listCases_SelectedIndexChanged);

            // colCaseId
            this.colCaseId.Text = "מזהה";
            this.colCaseId.Width = 50;

            // colClientId
            this.colClientId.Text = "לקוח";
            this.colClientId.Width = 50;

            // colReportingPeriod
            this.colReportingPeriod.Text = "תקופה";
            this.colReportingPeriod.Width = 100;

            // colStatus
            this.colStatus.Text = "סטטוס";
            this.colStatus.Width = 100;

            // lblCaseId
            this.lblCaseId.AutoSize = true;
            this.lblCaseId.Location = new System.Drawing.Point(700, 80);
            this.lblCaseId.Name = "lblCaseId";
            this.lblCaseId.Size = new System.Drawing.Size(30, 16);
            this.lblCaseId.TabIndex = 1;
            this.lblCaseId.Text = "מזהה";

            // lblClientId
            this.lblClientId.AutoSize = true;
            this.lblClientId.Location = new System.Drawing.Point(700, 50);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(40, 16);
            this.lblClientId.TabIndex = 2;
            this.lblClientId.Text = "לקוח";

            // lblReportingPeriod
            this.lblReportingPeriod.AutoSize = true;
            this.lblReportingPeriod.Location = new System.Drawing.Point(700, 80);
            this.lblReportingPeriod.Name = "lblReportingPeriod";
            this.lblReportingPeriod.Size = new System.Drawing.Size(60, 16);
            this.lblReportingPeriod.TabIndex = 3;
            this.lblReportingPeriod.Text = "תקופה";

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(700, 110);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(45, 16);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "סטטוס";

            // lblCreatedDate
            this.lblCreatedDate.AutoSize = true;
            this.lblCreatedDate.Location = new System.Drawing.Point(700, 140);
            this.lblCreatedDate.Name = "lblCreatedDate";
            this.lblCreatedDate.Size = new System.Drawing.Size(50, 16);
            this.lblCreatedDate.TabIndex = 5;
            this.lblCreatedDate.Text = "תאריך יצירה";

            // lblDueDate
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Location = new System.Drawing.Point(700, 170);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(45, 16);
            this.lblDueDate.TabIndex = 6;
            this.lblDueDate.Text = "תאריך יעד";

            // lblAssignedTo
            this.lblAssignedTo.AutoSize = true;
            this.lblAssignedTo.Location = new System.Drawing.Point(700, 200);
            this.lblAssignedTo.Name = "lblAssignedTo";
            this.lblAssignedTo.Size = new System.Drawing.Size(55, 16);
            this.lblAssignedTo.TabIndex = 7;
            this.lblAssignedTo.Text = "מוקצה ל";

            // txtCaseId
            this.txtCaseId.Location = new System.Drawing.Point(580, 20);
            this.txtCaseId.Name = "txtCaseId";
            this.txtCaseId.ReadOnly = true;
            this.txtCaseId.Size = new System.Drawing.Size(100, 22);
            this.txtCaseId.TabIndex = 8;

            // txtClientId
            this.txtClientId.Location = new System.Drawing.Point(580, 50);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Size = new System.Drawing.Size(100, 22);
            this.txtClientId.TabIndex = 9;

            // dtReportingPeriod
            this.dtReportingPeriod.Location = new System.Drawing.Point(580, 80);
            this.dtReportingPeriod.Name = "dtReportingPeriod";
            this.dtReportingPeriod.Size = new System.Drawing.Size(100, 22);
            this.dtReportingPeriod.TabIndex = 10;

            // cmbStatus
            this.cmbStatus.Items.AddRange(new object[] {
            "Open",
            "In Progress",
            "Under Review",
            "Completed"});
            this.cmbStatus.Location = new System.Drawing.Point(580, 110);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(100, 24);
            this.cmbStatus.TabIndex = 11;

            // dtCreatedDate
            this.dtCreatedDate.Location = new System.Drawing.Point(580, 140);
            this.dtCreatedDate.Name = "dtCreatedDate";
            this.dtCreatedDate.Size = new System.Drawing.Size(100, 22);
            this.dtCreatedDate.TabIndex = 12;

            // dtDueDate
            this.dtDueDate.Location = new System.Drawing.Point(580, 170);
            this.dtDueDate.Name = "dtDueDate";
            this.dtDueDate.Size = new System.Drawing.Size(100, 22);
            this.dtDueDate.TabIndex = 13;

            // txtAssignedTo
            this.txtAssignedTo.Location = new System.Drawing.Point(580, 200);
            this.txtAssignedTo.Name = "txtAssignedTo";
            this.txtAssignedTo.Size = new System.Drawing.Size(100, 22);
            this.txtAssignedTo.TabIndex = 14;

            // btnNew
            this.btnNew.Location = new System.Drawing.Point(20, 310);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 30);
            this.btnNew.TabIndex = 15;
            this.btnNew.Text = "חדש";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            this.toolTip.SetToolTip(this.btnNew, "צור תיק חדש");

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(110, 250);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "שמור";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.toolTip.SetToolTip(this.btnSave, "שמור תיק חדש");

            // btnUpdate
            this.btnUpdate.Location = new System.Drawing.Point(200, 250);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 30);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "עדכן";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.toolTip.SetToolTip(this.btnUpdate, "עדכן תיק קיים");

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(290, 250);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "מחק";
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.toolTip.SetToolTip(this.btnDelete, "מחק תיק");

            // btnBack
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(934, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 30);
            this.btnBack.TabIndex = 19;
            this.btnBack.Text = "חזור";
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            this.toolTip.SetToolTip(this.btnBack, "חזור לעמוד הבית");

            // ClientCasePanel
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navPanel);
            this.Controls.Add(this.btnBack);
            this.btnBack.BringToFront();
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.txtAssignedTo);
            this.Controls.Add(this.dtDueDate);
            this.Controls.Add(this.dtCreatedDate);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.dtReportingPeriod);
            this.Controls.Add(this.txtClientId);
            this.Controls.Add(this.txtCaseId);
            this.Controls.Add(this.lblAssignedTo);
            this.Controls.Add(this.lblDueDate);
            this.Controls.Add(this.lblCreatedDate);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblReportingPeriod);
            this.Controls.Add(this.lblClientId);
            this.Controls.Add(this.lblCaseId);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.navPanel);
            this.Controls.Add(this.listCases);
            this.Name = "ClientCasePanel";
            this.Size = new System.Drawing.Size(1024, 768);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ListView listCases;
        private System.Windows.Forms.ColumnHeader colCaseId;
        private System.Windows.Forms.ColumnHeader colClientId;
        private System.Windows.Forms.ColumnHeader colReportingPeriod;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Label lblCaseId;
        private System.Windows.Forms.Label lblClientId;
        private System.Windows.Forms.Label lblReportingPeriod;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCreatedDate;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Label lblAssignedTo;
        private System.Windows.Forms.TextBox txtCaseId;
        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.DateTimePicker dtReportingPeriod;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.DateTimePicker dtCreatedDate;
        private System.Windows.Forms.DateTimePicker dtDueDate;
        private System.Windows.Forms.TextBox txtAssignedTo;
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

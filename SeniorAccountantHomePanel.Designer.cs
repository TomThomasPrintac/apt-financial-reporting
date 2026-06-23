namespace APT
{
    partial class SeniorAccountantHomePanel
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnManageCases = new System.Windows.Forms.Button();
            this.btnUC05 = new System.Windows.Forms.Button();
            this.btnUC06 = new System.Windows.Forms.Button();
            this.btnUploadFiles = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblWelcome.Location = new System.Drawing.Point(400, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(200, 20);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "ברוכה הבאה";

            // btnManageCases
            this.btnManageCases.Location = new System.Drawing.Point(150, 100);
            this.btnManageCases.Name = "btnManageCases";
            this.btnManageCases.Size = new System.Drawing.Size(300, 50);
            this.btnManageCases.TabIndex = 1;
            this.btnManageCases.Text = "ניהול תיקים";
            this.btnManageCases.UseVisualStyleBackColor = true;
            this.btnManageCases.Click += new System.EventHandler(this.btnManageCases_Click);

            // btnUC05
            this.btnUC05.Location = new System.Drawing.Point(150, 160);
            this.btnUC05.Name = "btnUC05";
            this.btnUC05.Size = new System.Drawing.Size(300, 50);
            this.btnUC05.TabIndex = 2;
            this.btnUC05.Text = "UC-05: Generate Financial Reports";
            this.btnUC05.UseVisualStyleBackColor = true;
            this.btnUC05.Click += new System.EventHandler(this.btnUC05_Click);

            // btnUC06
            this.btnUC06.Location = new System.Drawing.Point(150, 220);
            this.btnUC06.Name = "btnUC06";
            this.btnUC06.Size = new System.Drawing.Size(300, 50);
            this.btnUC06.TabIndex = 3;
            this.btnUC06.Text = "UC-06: Manage Report Templates";
            this.btnUC06.UseVisualStyleBackColor = true;
            this.btnUC06.Click += new System.EventHandler(this.btnUC06_Click);

            // btnUploadFiles
            this.btnUploadFiles.Location = new System.Drawing.Point(150, 280);
            this.btnUploadFiles.Name = "btnUploadFiles";
            this.btnUploadFiles.Size = new System.Drawing.Size(300, 50);
            this.btnUploadFiles.TabIndex = 5;
            this.btnUploadFiles.Text = "העלאת קבצי מקור (UC-02)";
            this.btnUploadFiles.UseVisualStyleBackColor = true;
            this.btnUploadFiles.Click += new System.EventHandler(this.btnUploadFiles_Click);

            // btnLogout
            this.btnLogout.Location = new System.Drawing.Point(150, 680);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(300, 40);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "יציאה";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // SeniorAccountantHomePanel
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUploadFiles);
            this.Controls.Add(this.btnUC06);
            this.Controls.Add(this.btnUC05);
            this.Controls.Add(this.btnManageCases);
            this.Controls.Add(this.lblWelcome);
            this.Name = "SeniorAccountantHomePanel";
            this.Size = new System.Drawing.Size(1024, 768);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnManageCases;
        private System.Windows.Forms.Button btnUC05;
        private System.Windows.Forms.Button btnUC06;
        private System.Windows.Forms.Button btnUploadFiles;
        private System.Windows.Forms.Button btnLogout;
    }
}

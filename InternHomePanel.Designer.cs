namespace APT
{
    partial class InternHomePanel
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
            this.btnUC02 = new System.Windows.Forms.Button();
            this.btnUC03 = new System.Windows.Forms.Button();
            this.btnUC04 = new System.Windows.Forms.Button();
            this.btnUC05 = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLedger = new System.Windows.Forms.Button();
            this.btnPayroll = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblWelcome.Location = new System.Drawing.Point(400, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(200, 20);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "ברוכה הבאה";

            // btnUC02
            this.btnUC02.Location = new System.Drawing.Point(150, 100);
            this.btnUC02.Name = "btnUC02";
            this.btnUC02.Size = new System.Drawing.Size(300, 50);
            this.btnUC02.TabIndex = 1;
            this.btnUC02.Text = "UC-02: העלאת קבצי מקור";
            this.btnUC02.UseVisualStyleBackColor = true;
            this.btnUC02.Click += new System.EventHandler(this.btnUC02_Click);

            // btnUC03
            this.btnUC03.Location = new System.Drawing.Point(150, 160);
            this.btnUC03.Name = "btnUC03";
            this.btnUC03.Size = new System.Drawing.Size(300, 50);
            this.btnUC03.TabIndex = 2;
            this.btnUC03.Text = "UC-03: Process Cumulative Files";
            this.btnUC03.UseVisualStyleBackColor = true;
            this.btnUC03.Click += new System.EventHandler(this.btnUC03_Click);

            // btnUC04
            this.btnUC04.Location = new System.Drawing.Point(150, 220);
            this.btnUC04.Name = "btnUC04";
            this.btnUC04.Size = new System.Drawing.Size(300, 50);
            this.btnUC04.TabIndex = 3;
            this.btnUC04.Text = "UC-04: Cross-Match Payroll with Ledger";
            this.btnUC04.UseVisualStyleBackColor = true;
            this.btnUC04.Click += new System.EventHandler(this.btnUC04_Click);

            // btnUC05
            this.btnUC05.Location = new System.Drawing.Point(150, 280);
            this.btnUC05.Name = "btnUC05";
            this.btnUC05.Size = new System.Drawing.Size(300, 50);
            this.btnUC05.TabIndex = 4;
            this.btnUC05.Text = "UC-05: Generate Financial Reports";
            this.btnUC05.UseVisualStyleBackColor = true;
            this.btnUC05.Click += new System.EventHandler(this.btnUC05_Click);

            // btnLedger
            this.btnLedger.Location = new System.Drawing.Point(150, 340);
            this.btnLedger.Name = "btnLedger";
            this.btnLedger.Size = new System.Drawing.Size(300, 50);
            this.btnLedger.TabIndex = 5;
            this.btnLedger.Text = "ניהול תנועות יומן (כרטסת)";
            this.btnLedger.UseVisualStyleBackColor = true;
            this.btnLedger.Click += new System.EventHandler(this.btnLedger_Click);

            // btnPayroll
            this.btnPayroll.Location = new System.Drawing.Point(150, 400);
            this.btnPayroll.Name = "btnPayroll";
            this.btnPayroll.Size = new System.Drawing.Size(300, 50);
            this.btnPayroll.TabIndex = 6;
            this.btnPayroll.Text = "ניהול רשומות שכר";
            this.btnPayroll.UseVisualStyleBackColor = true;
            this.btnPayroll.Click += new System.EventHandler(this.btnPayroll_Click);

            // btnLogout
            this.btnLogout.Location = new System.Drawing.Point(150, 680);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(300, 40);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "יציאה";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // InternHomePanel
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnLedger);
            this.Controls.Add(this.btnPayroll);
            this.Controls.Add(this.btnUC05);
            this.Controls.Add(this.btnUC04);
            this.Controls.Add(this.btnUC03);
            this.Controls.Add(this.btnUC02);
            this.Controls.Add(this.lblWelcome);
            this.Name = "InternHomePanel";
            this.Size = new System.Drawing.Size(1024, 768);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnUC02;
        private System.Windows.Forms.Button btnUC03;
        private System.Windows.Forms.Button btnUC04;
        private System.Windows.Forms.Button btnUC05;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnLedger;
        private System.Windows.Forms.Button btnPayroll;
    }
}

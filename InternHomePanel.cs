using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace APT
{
    public partial class InternHomePanel : UserControl
    {
        private Intern currentUser;

        public InternHomePanel(Intern user)
        {
            InitializeComponent();
            UiCenter.Enable(this);
            this.currentUser = user;
            lblWelcome.Text = $"ברוכה הבאה, {currentUser.getEmail()}";
        }

        private void btnUC02_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new SourceFilePanel(currentUser));
        }

        private void btnUC03_Click(object sender, EventArgs e)
        {
            MessageBox.Show("UC-03: Process Cumulative Files\n(TODO)", "פונקציה לא מוגדרת");
        }

        private void btnUC04_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new PayrollLedgerMatchPanel(currentUser));
        }

        private void btnUC05_Click(object sender, EventArgs e)
        {
            MessageBox.Show("UC-05: Generate Financial Reports - ניהול דוחות", "לא זמין לאינטרן");
        }

        private void btnLedger_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new LedgerEntryPanel(currentUser));
        }

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new PayrollRecordPanel(currentUser));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new LoginPanel());
        }
    }
}

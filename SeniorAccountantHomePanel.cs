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
    public partial class SeniorAccountantHomePanel : UserControl
    {
        private SeniorAccountant currentUser;

        public SeniorAccountantHomePanel(SeniorAccountant user)
        {
            InitializeComponent();
            this.currentUser = user;
            lblWelcome.Text = $"ברוכה הבאה, {currentUser.getEmail()}";
        }

        private void btnManageCases_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new ClientCasePanel(currentUser));
        }

        private void btnUC05_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new FinancialReportPanel(currentUser));
        }

        private void btnUC06_Click(object sender, EventArgs e)
        {
            MessageBox.Show("UC-06: Manage Report Templates\n(TODO)", "פונקציה לא מוגדרת");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new LoginPanel());
        }
    }
}

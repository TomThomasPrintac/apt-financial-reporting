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
    public partial class SystemAdministratorHomePanel : UserControl
    {
        private SystemAdministrator currentUser;

        public SystemAdministratorHomePanel(SystemAdministrator user)
        {
            InitializeComponent();
            this.currentUser = user;
            lblWelcome.Text = $"ברוכה הבאה, {currentUser.getEmail()}";
            lblMessage.Text = "לא הוקצו תפקידים בגרסה זו של המערכת.";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new LoginPanel());
        }
    }
}

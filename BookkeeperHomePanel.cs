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
    public partial class BookkeeperHomePanel : UserControl
    {
        private Bookkeeper currentUser;

        public BookkeeperHomePanel(Bookkeeper user)
        {
            InitializeComponent();
            UiCenter.Enable(this);
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

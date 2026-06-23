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
    public partial class LoginPanel : UserControl
    {
        public LoginPanel()
        {
            InitializeComponent();
            LoadLogo();
        }

        // טעינת לוגו החברה ל-PictureBox (בלי לנעול את הקובץ)
        private void LoadLogo()
        {
            foreach (string p in new[] {
                System.IO.Path.Combine(System.AppContext.BaseDirectory, "logo.png"),
                System.IO.Path.Combine(System.Environment.CurrentDirectory, "logo.png") })
            {
                if (System.IO.File.Exists(p))
                {
                    picLogo.Image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(p)));
                    break;
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("אנא מלא דוא״ל וסיסמה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Try SeniorAccountant
            SeniorAccountant sa = seekUserByEmail<SeniorAccountant>(Program.SeniorAccountants, email, password);
            if (sa != null)
            {
                mainForm.showPanel(new SeniorAccountantHomePanel(sa));
                return;
            }

            // Try Intern
            Intern intern = seekUserByEmail<Intern>(Program.Interns, email, password);
            if (intern != null)
            {
                mainForm.showPanel(new InternHomePanel(intern));
                return;
            }

            // Try Bookkeeper
            Bookkeeper bookkeeper = seekUserByEmail<Bookkeeper>(Program.Bookkeepers, email, password);
            if (bookkeeper != null)
            {
                mainForm.showPanel(new BookkeeperHomePanel(bookkeeper));
                return;
            }

            // Try SystemAdministrator
            SystemAdministrator sysAdmin = seekUserByEmail<SystemAdministrator>(Program.SystemAdministrators, email, password);
            if (sysAdmin != null)
            {
                mainForm.showPanel(new SystemAdministratorHomePanel(sysAdmin));
                return;
            }

            // Try Client
            Client client = seekUserByEmail<Client>(Program.Clients, email, password);
            if (client != null)
            {
                mainForm.showPanel(new ClientHomePanel(client));
                return;
            }

            // No match found
            MessageBox.Show("דוא״ל או סיסמה שגויה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnDevLogin_Click(object sender, EventArgs e)
        {
            // קיצור מפתח — כניסה כאדוארדו (רואה חשבון בכיר), דרך אותו נתיב אימות
            // כאילו הוזנו שם המשתמש והסיסמה שלו במסך הכניסה.
            SeniorAccountant eduardo = seekUserByEmail<SeniorAccountant>(
                Program.SeniorAccountants, "eduardo.printek@printek.co.il", "password123");
            if (eduardo != null)
            {
                mainForm.showPanel(new SeniorAccountantHomePanel(eduardo));
            }
            else
            {
                MessageBox.Show("המשתמש אדוארדו לא נמצא בטעינה", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private T seekUserByEmail<T>(List<T> users, string email, string password) where T : User
        {
            if (users == null)
                return null;

            foreach (T user in users)
            {
                if (user.getEmail().Equals(email) && user.getPasswordHash().Equals(password))
                {
                    return user;
                }
            }
            return null;
        }
    }
}

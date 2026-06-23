using System;
using System.Windows.Forms;

namespace APT
{
    /// <summary>
    /// UC-06 — תפריט תבניות הדוחות (לפי סוג קובץ מקור: Payroll / TrialBalance / Depreciation / Ledger).
    /// מציג את התבניות הקיימות; בחירה ולחיצה על "ערוך תבנית" פותחת את עורך השדות.
    /// </summary>
    public partial class ReportTemplateMenuPanel : UserControl
    {
        private SeniorAccountant currentUser;
        private ReportTemplate selectedTemplate = null;

        public ReportTemplateMenuPanel(SeniorAccountant user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadTemplates();
        }

        private void LoadTemplates()
        {
            listTemplates.Items.Clear();
            if (Program.ReportTemplates != null)
            {
                foreach (ReportTemplate t in Program.ReportTemplates)
                {
                    ListViewItem item = new ListViewItem(t.getReportType());
                    item.SubItems.Add(t.getTemplateName());
                    item.SubItems.Add(t.getLastUpdated().ToString("dd/MM/yyyy HH:mm"));
                    item.Tag = t;
                    listTemplates.Items.Add(item);
                }
            }
        }

        private void listTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTemplates.SelectedItems.Count > 0)
                selectedTemplate = (ReportTemplate)listTemplates.SelectedItems[0].Tag;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedTemplate == null)
            {
                MessageBox.Show("אנא בחר תבנית לעריכה", "לא נבחרה תבנית", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            mainForm.showPanel(new ReportTemplateEditorPanel(currentUser, selectedTemplate));
        }

        private void listTemplates_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void btnHome_Click(object sender, EventArgs e) { mainForm.showPanel(new SeniorAccountantHomePanel(currentUser)); }
        private void btnNavBack_Click(object sender, EventArgs e) { mainForm.showPanel(new SeniorAccountantHomePanel(currentUser)); }
        private void btnNavLogout_Click(object sender, EventArgs e) { mainForm.showPanel(new LoginPanel()); }
    }
}

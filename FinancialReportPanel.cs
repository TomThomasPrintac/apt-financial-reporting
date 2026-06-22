using System;
using System.Windows.Forms;

namespace APT
{
    public partial class FinancialReportPanel : UserControl
    {
        private SeniorAccountant currentUser;
        private FinancialReport selectedReport = null;

        public FinancialReportPanel(SeniorAccountant user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadReportsList();
            // ערכים תואמים בדיוק לאילוצי ה-CHECK ב-DB (CK_FinancialReports_ReportType/ReportFormat/Status)
            cmbReportType.Items.AddRange(new object[] { "Balance Sheet", "P&L", "Cash Flow" });
            cmbReportFormat.Items.AddRange(new object[] { "PDF", "Excel" });
            cmbStatus.Items.AddRange(new object[] { "Draft", "Generated", "Signed", "Submitted" });
            // ברירות מחדל — מונע NRE בשמירה
            cmbReportType.SelectedIndex = 0;
            cmbReportFormat.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            // החותם הוא תמיד רואה החשבון המחובר
            txtSeniorAccountantId.Text = currentUser.getUserId().ToString();
        }

        private void LoadReportsList()
        {
            listReports.Items.Clear();
            if (Program.FinancialReports != null)
            {
                foreach (FinancialReport fr in Program.FinancialReports)
                {
                    ListViewItem item = new ListViewItem(fr.getReportId().ToString());
                    item.SubItems.Add(fr.getCaseId().ToString());
                    item.SubItems.Add(fr.getReportType());
                    item.SubItems.Add(fr.getIsSigned() ? "חתום" : "לא חתום");
                    item.Tag = fr;
                    listReports.Items.Add(item);
                }
            }
        }

        private void listReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listReports.SelectedItems.Count > 0)
            {
                selectedReport = (FinancialReport)listReports.SelectedItems[0].Tag;
                txtReportId.Text = selectedReport.getReportId().ToString();
                txtCaseId.Text = selectedReport.getCaseId().ToString();
                cmbReportType.SelectedItem = selectedReport.getReportType();
                cmbReportFormat.SelectedItem = selectedReport.getReportFormat();
                dtGeneratedDate.Value = selectedReport.getGeneratedDate();
                chkIsSigned.Checked = selectedReport.getIsSigned();
                if (selectedReport.getIsSigned())
                    dtSignedDate.Value = selectedReport.getSignedDate();
                cmbStatus.SelectedItem = selectedReport.getStatus();
                txtSeniorAccountantId.Text = selectedReport.getSeniorAccountantId().ToString();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtReportId.Text = "";
            txtCaseId.Text = "";
            cmbReportType.SelectedIndex = 0;
            cmbReportFormat.SelectedIndex = 0;
            dtGeneratedDate.Value = DateTime.Now;
            chkIsSigned.Checked = false;
            dtSignedDate.Value = DateTime.Now;
            cmbStatus.SelectedIndex = 0;
            txtSeniorAccountantId.Text = currentUser.getUserId().ToString();
            selectedReport = null;
            listReports.SelectedItems.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // --- ולידציה: תיק (חובה, מספר שלם חיובי, קיים) ---
                if (string.IsNullOrWhiteSpace(txtCaseId.Text))
                {
                    MessageBox.Show("אנא הזן מספר תיק", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCaseId.Focus();
                    return;
                }
                if (!int.TryParse(txtCaseId.Text.Trim(), out int caseId) || caseId <= 0)
                {
                    MessageBox.Show("מספר תיק חייב להיות מספר שלם חיובי", "ערך לא תקין", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCaseId.Focus();
                    return;
                }
                if (ClientCase.seekClientCase(caseId) == null)
                {
                    MessageBox.Show($"תיק מספר {caseId} לא קיים במערכת", "מזהה לא נמצא", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCaseId.Focus();
                    return;
                }
                // --- ולידציה: בחירות ComboBox ---
                if (cmbReportType.SelectedItem == null || cmbReportFormat.SelectedItem == null || cmbStatus.SelectedItem == null)
                {
                    MessageBox.Show("אנא בחר סוג דוח, פורמט וסטטוס", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nextId = FinancialReport.getNextReportId();
                int seniorId = currentUser.getUserId();   // החותם הוא תמיד רואה החשבון המחובר

                FinancialReport newReport = new FinancialReport(nextId, caseId, cmbReportType.SelectedItem.ToString(),
                    cmbReportFormat.SelectedItem.ToString(), dtGeneratedDate.Value, chkIsSigned.Checked,
                    dtSignedDate.Value, cmbStatus.SelectedItem.ToString(), seniorId, true);

                MessageBox.Show("הדוח נוצר בהצלחה", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadReportsList();
                btnNew_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בשמירה: " + ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedReport == null)
                {
                    MessageBox.Show("אנא בחר דוח לעדכון", "שגיאה");
                    return;
                }
                selectedReport.setStatus(cmbStatus.SelectedItem.ToString());
                selectedReport.updateFinancialReport();
                MessageBox.Show("הדוח עודכן בהצלחה", "הודעה");
                LoadReportsList();
                btnNew_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בעדכון: " + ex.Message, "שגיאה");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedReport == null) { MessageBox.Show("אנא בחר דוח למחיקה", "שגיאה"); return; }
                if (MessageBox.Show("האם אתה בטוח?", "אישור", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    selectedReport.deleteFinancialReport();
                    MessageBox.Show("הדוח נמחק בהצלחה", "הודעה");
                    LoadReportsList();
                    btnNew_Click(null, null);
                }
            }
            catch (Exception ex) { MessageBox.Show("שגיאה במחיקה: " + ex.Message, "שגיאה"); }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedReport == null) { MessageBox.Show("אנא בחר דוח לחתימה", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (selectedReport.getIsSigned())
                {
                    MessageBox.Show("הדוח כבר חתום", "פעולה לא נדרשת", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                selectedReport.sign();
                selectedReport.updateFinancialReport();   // שמירת החתימה ל-DB: isSigned / signedDate / status
                MessageBox.Show("הדוח נחתם בהצלחה", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadReportsList();
            }
            catch (Exception ex) { MessageBox.Show("שגיאה בחתימה: " + ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new SeniorAccountantHomePanel(currentUser));
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new SeniorAccountantHomePanel(currentUser));
        }

        private void btnNavBack_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new SeniorAccountantHomePanel(currentUser));
        }

        private void btnNavLogout_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new LoginPanel());
        }
    }
}

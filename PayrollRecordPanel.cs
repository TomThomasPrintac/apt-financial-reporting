using System;
using System.Windows.Forms;

namespace APT
{
    public partial class PayrollRecordPanel : UserControl
    {
        private Intern currentUser;
        private PayrollRecord selectedRecord = null;

        public PayrollRecordPanel(Intern user)
        {
            InitializeComponent();
            UiCenter.Enable(this);
            this.currentUser = user;
            LoadRecordsList();
        }

        private void LoadRecordsList()
        {
            listPayroll.Items.Clear();
            if (Program.PayrollRecords != null)
            {
                foreach (PayrollRecord pr in Program.PayrollRecords)
                {
                    ListViewItem item = new ListViewItem(pr.getPayrollId().ToString());
                    item.SubItems.Add(pr.getCaseId().ToString());
                    item.SubItems.Add(pr.getEmployeeId());
                    item.SubItems.Add(pr.getEmployeeName());
                    item.Tag = pr;
                    listPayroll.Items.Add(item);
                }
            }
        }

        private void listPayroll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listPayroll.SelectedItems.Count > 0)
            {
                selectedRecord = (PayrollRecord)listPayroll.SelectedItems[0].Tag;
                txtPayrollId.Text = selectedRecord.getPayrollId().ToString();
                txtCaseId.Text = selectedRecord.getCaseId().ToString();
                txtEmployeeId.Text = selectedRecord.getEmployeeId();
                txtEmployeeName.Text = selectedRecord.getEmployeeName();
                txtGrossAmount.Text = selectedRecord.getGrossAmount().ToString();
                txtSocial.Text = selectedRecord.getSocialContributions().ToString();
                txtEmployerCosts.Text = selectedRecord.getEmployerCosts().ToString();
                dtReportingPeriod.Value = selectedRecord.getReportingPeriod();
                chkIsNew.Checked = selectedRecord.getIsNew();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtPayrollId.Text = "";
            txtCaseId.Text = "";
            txtEmployeeId.Text = "";
            txtEmployeeName.Text = "";
            txtGrossAmount.Text = "";
            txtSocial.Text = "";
            txtEmployerCosts.Text = "";
            dtReportingPeriod.Value = DateTime.Now;
            chkIsNew.Checked = false;
            selectedRecord = null;
            listPayroll.SelectedItems.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int nextId = PayrollRecord.getNextPayrollId();
                int caseId = int.Parse(txtCaseId.Text);
                decimal gross = decimal.Parse(txtGrossAmount.Text);
                decimal social = decimal.Parse(txtSocial.Text);
                decimal employer = decimal.Parse(txtEmployerCosts.Text);

                PayrollRecord newRecord = new PayrollRecord(nextId, caseId, txtEmployeeId.Text, txtEmployeeName.Text,
                    gross, social, employer, dtReportingPeriod.Value, chkIsNew.Checked, true);

                MessageBox.Show("הרשומה נוצרה בהצלחה", "הודעה");
                LoadRecordsList();
                btnNew_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בשמירה: " + ex.Message, "שגיאה");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedRecord == null)
                {
                    MessageBox.Show("אנא בחר רשומה לעדכון", "שגיאה");
                    return;
                }
                selectedRecord.setGrossAmount(decimal.Parse(txtGrossAmount.Text));
                selectedRecord.updatePayrollRecord();
                MessageBox.Show("הרשומה עודכנה בהצלחה", "הודעה");
                LoadRecordsList();
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
                if (selectedRecord == null) { MessageBox.Show("אנא בחר רשומה למחיקה", "שגיאה"); return; }
                if (MessageBox.Show("האם אתה בטוח?", "אישור", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    selectedRecord.deletePayrollRecord();
                    MessageBox.Show("הרשומה נמחקה בהצלחה", "הודעה");
                    LoadRecordsList();
                    btnNew_Click(null, null);
                }
            }
            catch (Exception ex) { MessageBox.Show("שגיאה במחיקה: " + ex.Message, "שגיאה"); }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new InternHomePanel(currentUser));
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new InternHomePanel(currentUser));
        }

        private void btnNavBack_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new InternHomePanel(currentUser));
        }

        private void btnNavLogout_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new LoginPanel());
        }
    }
}

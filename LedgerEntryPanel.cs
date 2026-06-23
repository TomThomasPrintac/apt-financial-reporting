using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APT
{
    public partial class LedgerEntryPanel : UserControl
    {
        private Intern currentUser;
        private LedgerEntry selectedEntry = null;

        public LedgerEntryPanel(Intern user)
        {
            InitializeComponent();
            UiCenter.Enable(this);
            this.currentUser = user;
            LoadEntriesList();
            cmbAccountType.Items.AddRange(new object[] { "Asset", "Liability", "Equity", "Revenue", "Expense" });
        }

        private void LoadEntriesList()
        {
            listEntries.Items.Clear();
            if (Program.LedgerEntries != null)
            {
                foreach (LedgerEntry le in Program.LedgerEntries)
                {
                    ListViewItem item = new ListViewItem(le.getEntryId().ToString());
                    item.SubItems.Add(le.getCaseId().ToString());
                    item.SubItems.Add(le.getAccountCode());
                    item.SubItems.Add(le.getAccountType());
                    item.Tag = le;
                    listEntries.Items.Add(item);
                }
            }
        }

        private void listEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listEntries.SelectedItems.Count > 0)
            {
                selectedEntry = (LedgerEntry)listEntries.SelectedItems[0].Tag;
                txtEntryId.Text = selectedEntry.getEntryId().ToString();
                txtCaseId.Text = selectedEntry.getCaseId().ToString();
                txtAccountCode.Text = selectedEntry.getAccountCode();
                txtAccountName.Text = selectedEntry.getAccountName();
                cmbAccountType.SelectedItem = selectedEntry.getAccountType();
                txtAmount.Text = selectedEntry.getAmount().ToString();
                dtReportingPeriod.Value = selectedEntry.getReportingPeriod();
                chkIsNew.Checked = selectedEntry.getIsNew();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtEntryId.Text = "";
            txtCaseId.Text = "";
            txtAccountCode.Text = "";
            txtAccountName.Text = "";
            cmbAccountType.SelectedIndex = 0;
            txtAmount.Text = "";
            dtReportingPeriod.Value = DateTime.Now;
            chkIsNew.Checked = false;
            selectedEntry = null;
            listEntries.SelectedItems.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int nextId = LedgerEntry.getNextEntryId();
                int caseId = int.Parse(txtCaseId.Text);
                decimal amount = decimal.Parse(txtAmount.Text);

                LedgerEntry newEntry = new LedgerEntry(nextId, caseId, txtAccountCode.Text, txtAccountName.Text,
                    cmbAccountType.SelectedItem.ToString(), amount, dtReportingPeriod.Value, chkIsNew.Checked, true);

                MessageBox.Show("הערך נוצר בהצלחה", "הודעה");
                LoadEntriesList();
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
                if (selectedEntry == null)
                {
                    MessageBox.Show("אנא בחר ערך לעדכון", "שגיאה");
                    return;
                }
                selectedEntry.setAmount(decimal.Parse(txtAmount.Text));
                selectedEntry.updateLedgerEntry();
                MessageBox.Show("הערך עודכן בהצלחה", "הודעה");
                LoadEntriesList();
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
                if (selectedEntry == null) { MessageBox.Show("אנא בחר ערך למחיקה", "שגיאה"); return; }
                if (MessageBox.Show("האם אתה בטוח?", "אישור", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    selectedEntry.deleteLedgerEntry();
                    MessageBox.Show("הערך נמחק בהצלחה", "הודעה");
                    LoadEntriesList();
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

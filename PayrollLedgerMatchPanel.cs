using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APT
{
    public partial class PayrollLedgerMatchPanel : UserControl
    {
        private Intern currentUser;
        private PayrollLedgerMatch selectedMatch = null;
        private decimal varianceThreshold = 100m; // Default threshold

        public PayrollLedgerMatchPanel(Intern user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadMatchesList();
            cmbMatchStatus.Items.AddRange(new object[] { "Matched", "Flagged", "RequiresReview" });
            cmbMatchStatus.SelectedIndex = 0;  // ברירת מחדל — מונע NRE בשמירה
        }

        private void LoadMatchesList()
        {
            listMatches.Items.Clear();
            if (Program.PayrollLedgerMatches != null)
            {
                foreach (PayrollLedgerMatch plm in Program.PayrollLedgerMatches)
                {
                    ListViewItem item = new ListViewItem(plm.getMatchId().ToString());
                    item.SubItems.Add(plm.getPayrollId().ToString());
                    item.SubItems.Add(plm.getEntryId().ToString());
                    item.SubItems.Add(plm.getMatchStatus());
                    item.Tag = plm;
                    listMatches.Items.Add(item);
                }
            }
        }

        private void listMatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listMatches.SelectedItems.Count > 0)
            {
                selectedMatch = (PayrollLedgerMatch)listMatches.SelectedItems[0].Tag;
                txtMatchId.Text = selectedMatch.getMatchId().ToString();
                txtPayrollId.Text = selectedMatch.getPayrollId().ToString();
                txtEntryId.Text = selectedMatch.getEntryId().ToString();
                txtCaseId.Text = selectedMatch.getCaseId().ToString();
                cmbMatchStatus.SelectedItem = selectedMatch.getMatchStatus();
                txtVariance.Text = selectedMatch.getVarianceAmount().ToString();
                dtMatchedDate.Value = selectedMatch.getMatchedDate();
                txtNotes.Text = selectedMatch.getNotes();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtMatchId.Text = "";
            txtPayrollId.Text = "";
            txtEntryId.Text = "";
            txtCaseId.Text = "";
            cmbMatchStatus.SelectedIndex = 0;
            txtVariance.Text = "";
            dtMatchedDate.Value = DateTime.Now;
            txtNotes.Text = "";
            selectedMatch = null;
            listMatches.SelectedItems.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // --- ולידציה: משכורת (חובה, מספר שלם חיובי, קיים) ---
                if (!int.TryParse(txtPayrollId.Text.Trim(), out int payrollId) || payrollId <= 0)
                {
                    MessageBox.Show("מספר משכורת חייב להיות מספר שלם חיובי", "ערך לא תקין", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPayrollId.Focus();
                    return;
                }
                if (PayrollRecord.seekPayrollRecord(payrollId) == null)
                {
                    MessageBox.Show($"רשומת משכורת מספר {payrollId} לא קיימת", "מזהה לא נמצא", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPayrollId.Focus();
                    return;
                }
                // --- ולידציה: ערך מאזן (חובה, מספר שלם חיובי, קיים) ---
                if (!int.TryParse(txtEntryId.Text.Trim(), out int entryId) || entryId <= 0)
                {
                    MessageBox.Show("מספר ערך מאזן חייב להיות מספר שלם חיובי", "ערך לא תקין", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEntryId.Focus();
                    return;
                }
                if (LedgerEntry.seekLedgerEntry(entryId) == null)
                {
                    MessageBox.Show($"ערך מאזן מספר {entryId} לא קיים", "מזהה לא נמצא", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEntryId.Focus();
                    return;
                }
                // --- ולידציה: תיק (חובה, מספר שלם חיובי, קיים) ---
                if (!int.TryParse(txtCaseId.Text.Trim(), out int caseId) || caseId <= 0)
                {
                    MessageBox.Show("מספר תיק חייב להיות מספר שלם חיובי", "ערך לא תקין", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCaseId.Focus();
                    return;
                }
                if (ClientCase.seekClientCase(caseId) == null)
                {
                    MessageBox.Show($"תיק מספר {caseId} לא קיים", "מזהה לא נמצא", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCaseId.Focus();
                    return;
                }
                // --- ולידציה: סטיה (חובה, מספרי) ---
                if (!decimal.TryParse(txtVariance.Text.Trim(), out decimal variance))
                {
                    MessageBox.Show("סכום הסטיה חייב להיות מספר", "ערך לא תקין", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVariance.Focus();
                    return;
                }
                if (cmbMatchStatus.SelectedItem == null)
                {
                    MessageBox.Show("אנא בחר סטטוס התאמה", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nextId = PayrollLedgerMatch.getNextMatchId();
                PayrollLedgerMatch newMatch = new PayrollLedgerMatch(nextId, payrollId, entryId, caseId,
                    cmbMatchStatus.SelectedItem.ToString(), variance, dtMatchedDate.Value, txtNotes.Text, true);

                MessageBox.Show("ההתאמה נוצרה בהצלחה", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMatchesList();
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
                if (selectedMatch == null)
                {
                    MessageBox.Show("אנא בחר התאמה לעדכון", "שגיאה");
                    return;
                }
                selectedMatch.setMatchStatus(cmbMatchStatus.SelectedItem.ToString());
                selectedMatch.setNotes(txtNotes.Text);
                selectedMatch.updatePayrollLedgerMatch();
                MessageBox.Show("ההתאמה עודכנה בהצלחה", "הודעה");
                LoadMatchesList();
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
                if (selectedMatch == null) { MessageBox.Show("אנא בחר התאמה למחיקה", "שגיאה"); return; }
                if (MessageBox.Show("האם אתה בטוח?", "אישור", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    selectedMatch.deletePayrollLedgerMatch();
                    MessageBox.Show("ההתאמה נמחקה בהצלחה", "הודעה");
                    LoadMatchesList();
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

        private void btnRunCrossMatch_Click(object sender, EventArgs e)
        {
            try
            {
                // --- ולידציה: תיק (חובה, מספר שלם חיובי, קיים) ---
                if (string.IsNullOrWhiteSpace(txtCaseId.Text))
                {
                    MessageBox.Show("אנא הזן מספר תיק להרצת ההתאמה", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show($"תיק מספר {caseId} לא קיים", "מזהה לא נמצא", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCaseId.Focus();
                    return;
                }
                DateTime reportingPeriod = dtReportingPeriod.Value;
                // --- ולידציה: סף סטיה (חובה, מספרי, אי-שלילי) ---
                if (!decimal.TryParse(txtVarianceThreshold.Text.Trim(), out decimal threshold) || threshold < 0)
                {
                    MessageBox.Show("סף הסטיה חייב להיות מספר אי-שלילי", "ערך לא תקין", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVarianceThreshold.Focus();
                    return;
                }

                // Get payroll records for this case and period
                var payrollRecords = new List<PayrollRecord>();
                if (Program.PayrollRecords != null)
                {
                    foreach (PayrollRecord pr in Program.PayrollRecords)
                    {
                        if (pr.getCaseId() == caseId && pr.getReportingPeriod().Date == reportingPeriod.Date)
                        {
                            payrollRecords.Add(pr);
                        }
                    }
                }

                // Get ledger entries for this case and period
                var ledgerEntries = new List<LedgerEntry>();
                if (Program.LedgerEntries != null)
                {
                    foreach (LedgerEntry le in Program.LedgerEntries)
                    {
                        if (le.getCaseId() == caseId && le.getReportingPeriod().Date == reportingPeriod.Date)
                        {
                            ledgerEntries.Add(le);
                        }
                    }
                }

                if (payrollRecords.Count == 0 || ledgerEntries.Count == 0)
                {
                    MessageBox.Show("לא נמצאו רשומות משכורה או ערכי חשבונות לתיק זה בתקופה זו", "אזהרה");
                    return;
                }

                // Calculate total payroll amount
                decimal totalPayroll = 0;
                foreach (var pr in payrollRecords)
                {
                    totalPayroll += pr.getTotalCost();
                }

                // Calculate total ledger amount
                decimal totalLedger = 0;
                foreach (var le in ledgerEntries)
                {
                    totalLedger += le.getAmount();
                }

                // Calculate variance
                decimal variance = Math.Abs(totalPayroll - totalLedger);
                string matchStatus = (variance <= threshold) ? "Matched" : "RequiresReview";

                // Create match record
                int nextId = PayrollLedgerMatch.getNextMatchId();
                int payrollId = payrollRecords[0].getPayrollId();
                int entryId = ledgerEntries[0].getEntryId();

                PayrollLedgerMatch newMatch = new PayrollLedgerMatch(nextId, payrollId, entryId, caseId,
                    matchStatus, variance, DateTime.Now, $"Auto-matched: Payroll={totalPayroll}, Ledger={totalLedger}", true);

                // Update summary
                lblVarianceSummary.Text = $"סה\"כ סטיה: ₪{variance:F2} | סטטוס: {matchStatus}";

                MessageBox.Show(
                    $"התאמה הושלמה בהצלחה\n\n" +
                    $"סה\"כ משכורה: ₪{totalPayroll:F2}\n" +
                    $"סה\"כ חשבונות: ₪{totalLedger:F2}\n" +
                    $"סטיה: ₪{variance:F2}\n" +
                    $"סטטוס: {matchStatus}",
                    "תוצאה");

                LoadMatchesList();
                btnNew_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בהרצת התאמה צולבת: " + ex.Message, "שגיאה");
            }
        }

        private void btnUpdateThreshold_Click(object sender, EventArgs e)
        {
            try
            {
                varianceThreshold = decimal.Parse(txtVarianceThreshold.Text);
                MessageBox.Show($"סף הסטיה עודכן ל: ₪{varianceThreshold:F2}", "הודעה");
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בעדכון סף הסטיה: " + ex.Message, "שגיאה");
            }
        }
    }
}

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
            UiCenter.Enable(this);
            this.currentUser = user;
            LoadMatchesList();
            cmbMatchStatus.Items.AddRange(new object[] { "Matched", "Unmatched", "Variance", "Requires Review" });
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

                // --- צד השכר: סך השכר ברוטו של כל העובדים בתיק ובתקופה ---
                decimal totalGross = 0;
                foreach (var pr in payrollRecords)
                    totalGross += pr.getGrossAmount();

                if (totalGross == 0)
                {
                    MessageBox.Show("סך השכר ברוטו לתיק/תקופה הוא 0 — לא ניתן להצליב", "אזהרה");
                    return;
                }

                // --- צד הכרטסת: רק חשבונות שכר/הוצאות שכר (לא כלל החשבונות) ---
                // התאמה כלכלית: סך השכר ברוטו אמור להירשם בחשבון 'הוצאות שכר' בכרטסת.
                var salaryAccounts = new List<LedgerEntry>();
                foreach (var le in ledgerEntries)
                {
                    string name = (le.getAccountName() ?? "").ToLowerInvariant();
                    if (name.Contains("salar") || name.Contains("wage") || name.Contains("payroll") || name.Contains("שכר"))
                        salaryAccounts.Add(le);
                }
                // נפילה לאחור: אם אין חשבון ששמו 'שכר', נשתמש בכל חשבונות ההוצאות
                if (salaryAccounts.Count == 0)
                    foreach (var le in ledgerEntries)
                        if (string.Equals(le.getAccountType(), "Expense", StringComparison.OrdinalIgnoreCase))
                            salaryAccounts.Add(le);
                if (salaryAccounts.Count == 0)
                {
                    MessageBox.Show("לא נמצא חשבון הוצאות שכר בכרטסת לתיק/תקופה זו", "אזהרה");
                    return;
                }

                decimal ledgerSalaries = 0;
                foreach (var le in salaryAccounts)
                    ledgerSalaries += le.getAmount();

                // --- סטיה כוללת והכרעת סטטוס מול הסף (ערכים תקפים מול אילוץ ה-CHECK ב-DB) ---
                decimal totalVariance = Math.Abs(totalGross - ledgerSalaries);
                string matchStatus = (totalVariance <= threshold) ? "Matched" : "Requires Review";

                // חשבון השכר הראשי — ה-FK האמיתי של צד הכרטסת בכל שורות ההתאמה
                LedgerEntry salariesEntry = salaryAccounts[0];

                // הסרת התאמות קודמות של חשבון השכר לתיק זה — מאפשר הרצה חוזרת בלי להפר את UQ_PayrollLedgerMatches_Pair (payroll_id, entry_id)
                var toRemove = new List<PayrollLedgerMatch>();
                foreach (var m in Program.PayrollLedgerMatches)
                    if (m.getCaseId() == caseId && m.getEntryId() == salariesEntry.getEntryId())
                        toRemove.Add(m);
                foreach (var m in toRemove)
                    m.deletePayrollLedgerMatch();

                // --- שורת התאמה לכל עובד עם FK-ים אמיתיים (עובד <-> חשבון שכר) ---
                // הסטיה לעובד = ההפרש בין שכרו בפועל לחלקו היחסי הצפוי בחשבון השכר;
                // כשהסכומים מתאזנים הסטיה לכל עובד 0, וסכום הסטיות = הסטיה הכוללת.
                int matchedCount = 0;
                foreach (var pr in payrollRecords)
                {
                    decimal expectedShare = ledgerSalaries * (pr.getGrossAmount() / totalGross);
                    decimal empVariance = Math.Abs(pr.getGrossAmount() - expectedShare);

                    int nextId = PayrollLedgerMatch.getNextMatchId();
                    PayrollLedgerMatch m = new PayrollLedgerMatch(nextId, pr.getPayrollId(), salariesEntry.getEntryId(),
                        caseId, matchStatus, empVariance, DateTime.Now,
                        $"{pr.getEmployeeName()}: ברוטו ₪{pr.getGrossAmount():F2} מול חשבון '{salariesEntry.getAccountName()}'", true);
                    m.setPayrollRecord(pr);
                    m.setLedgerEntry(salariesEntry);
                    matchedCount++;
                }

                // --- סיכום ---
                lblVarianceSummary.Text = $"סטיה כוללת: ₪{totalVariance:F2} | סטטוס: {matchStatus} | {matchedCount} עובדים";

                MessageBox.Show(
                    $"הצלבה הושלמה\n\n" +
                    $"סך שכר ברוטו: ₪{totalGross:F2}\n" +
                    $"סך חשבונות שכר בכרטסת: ₪{ledgerSalaries:F2} ({salaryAccounts.Count} חשבונות)\n" +
                    $"סטיה כוללת: ₪{totalVariance:F2}  (סף: ₪{threshold:F2})\n" +
                    $"סטטוס: {matchStatus}\n" +
                    $"נוצרו {matchedCount} שורות התאמה — אחת לכל עובד",
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

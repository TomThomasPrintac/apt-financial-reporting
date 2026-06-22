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
    public partial class ClientCasePanel : UserControl
    {
        private SeniorAccountant currentUser;
        private ClientCase selectedCase = null;

        public ClientCasePanel(SeniorAccountant user)
        {
            InitializeComponent();
            this.currentUser = user;
            LoadCasesList();
            if (cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0;  // ברירת מחדל — מונע NRE בשמירה
        }

        private void LoadCasesList()
        {
            listCases.Items.Clear();
            if (Program.ClientCases != null)
            {
                foreach (ClientCase cc in Program.ClientCases)
                {
                    ListViewItem item = new ListViewItem(cc.getCaseId().ToString());
                    item.SubItems.Add(cc.getClientId().ToString());
                    item.SubItems.Add(cc.getReportingPeriod().ToShortDateString());
                    item.SubItems.Add(cc.getStatus());
                    item.Tag = cc;
                    listCases.Items.Add(item);
                }
            }
        }

        private void listCases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listCases.SelectedItems.Count > 0)
            {
                selectedCase = (ClientCase)listCases.SelectedItems[0].Tag;
                PopulateFields(selectedCase);
            }
        }

        private void PopulateFields(ClientCase cc)
        {
            txtCaseId.Text = cc.getCaseId().ToString();
            txtClientId.Text = cc.getClientId().ToString();
            dtReportingPeriod.Value = cc.getReportingPeriod();
            cmbStatus.SelectedItem = cc.getStatus();
            dtCreatedDate.Value = cc.getCreatedDate();
            dtDueDate.Value = cc.getDueDate();
            txtAssignedTo.Text = cc.getAssignedTo() > 0 ? cc.getAssignedTo().ToString() : "";
        }

        private void ClearFields()
        {
            txtCaseId.Text = "";
            txtClientId.Text = "";
            dtReportingPeriod.Value = DateTime.Now;
            cmbStatus.SelectedIndex = 0;
            dtCreatedDate.Value = DateTime.Now;
            dtDueDate.Value = DateTime.Now;
            txtAssignedTo.Text = "";
            selectedCase = null;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearFields();
            listCases.SelectedItems.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // --- ולידציה: מספר לקוח (חובה, מספר שלם חיובי, קיים) ---
                if (string.IsNullOrWhiteSpace(txtClientId.Text))
                {
                    MessageBox.Show("אנא הזן מספר לקוח", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClientId.Focus();
                    return;
                }
                if (!int.TryParse(txtClientId.Text.Trim(), out int clientId) || clientId <= 0)
                {
                    MessageBox.Show("מספר לקוח חייב להיות מספר שלם חיובי", "ערך לא תקין", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClientId.Focus();
                    return;
                }
                if (Client.seekClient(clientId) == null)
                {
                    MessageBox.Show($"לקוח מספר {clientId} לא קיים במערכת", "מזהה לא נמצא", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClientId.Focus();
                    return;
                }
                // --- ולידציה: סטטוס (בחירה) ---
                if (cmbStatus.SelectedItem == null)
                {
                    MessageBox.Show("אנא בחר סטטוס", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // --- ולידציה: 'מוקצה ל' (אופציונלי; אם הוזן — חייב להיות משתמש קיים) ---
                int assignedTo = 0;
                if (!string.IsNullOrWhiteSpace(txtAssignedTo.Text))
                {
                    if (!int.TryParse(txtAssignedTo.Text.Trim(), out assignedTo) || assignedTo <= 0)
                    {
                        MessageBox.Show("השדה 'מוקצה ל' חייב להיות מספר משתמש תקין (או ריק)", "ערך לא תקין", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAssignedTo.Focus();
                        return;
                    }
                    if (User.seekUser(assignedTo) == null)
                    {
                        MessageBox.Show($"משתמש מספר {assignedTo} לא קיים במערכת", "מזהה לא נמצא", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAssignedTo.Focus();
                        return;
                    }
                }
                // --- מקרה קצה: תיק כפול לאותו לקוח ולאותה תקופה (אילוץ ייחודיות UQ_ClientCases_ClientPeriod) ---
                foreach (ClientCase existing in Program.ClientCases)
                {
                    if (existing.getClientId() == clientId &&
                        existing.getReportingPeriod().Date == dtReportingPeriod.Value.Date)
                    {
                        MessageBox.Show($"כבר קיים תיק ללקוח {clientId} עבור התקופה {dtReportingPeriod.Value:dd/MM/yyyy}", "כפילות", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                int nextId = ClientCase.getNextCaseId();
                ClientCase newCase = new ClientCase(nextId, clientId, dtReportingPeriod.Value, cmbStatus.SelectedItem.ToString(),
                                                    dtCreatedDate.Value, dtDueDate.Value, assignedTo, true);

                MessageBox.Show("התיק נוצר בהצלחה", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCasesList();
                ClearFields();
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
                if (selectedCase == null)
                {
                    MessageBox.Show("אנא בחר תיק לעדכון", "לא נבחר תיק", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // 'מוקצה ל' (אופציונלי; אם הוזן — חייב להיות מספר משתמש קיים)
                int assignedTo = 0;
                if (!string.IsNullOrWhiteSpace(txtAssignedTo.Text))
                {
                    if (!int.TryParse(txtAssignedTo.Text.Trim(), out assignedTo) || assignedTo <= 0)
                    {
                        MessageBox.Show("השדה 'מוקצה ל' חייב להיות מספר משתמש תקין (או ריק)", "ערך לא תקין", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAssignedTo.Focus();
                        return;
                    }
                    if (User.seekUser(assignedTo) == null)
                    {
                        MessageBox.Show($"משתמש מספר {assignedTo} לא קיים במערכת", "מזהה לא נמצא", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAssignedTo.Focus();
                        return;
                    }
                }

                selectedCase.setStatus(cmbStatus.SelectedItem.ToString());
                selectedCase.setDueDate(dtDueDate.Value);
                selectedCase.setAssignedTo(assignedTo);

                selectedCase.updateClientCase();
                MessageBox.Show("התיק עודכן בהצלחה", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCasesList();
                ClearFields();
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
                if (selectedCase == null)
                {
                    MessageBox.Show("אנא בחר תיק למחיקה", "שגיאה");
                    return;
                }

                DialogResult result = MessageBox.Show("האם אתה בטוח שברצונך למחוק את התיק?", "אישור",
                                                      MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    selectedCase.deleteClientCase();
                    MessageBox.Show("התיק נמחק בהצלחה", "הודעה");
                    LoadCasesList();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה במחיקה: " + ex.Message, "שגיאה");
            }
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

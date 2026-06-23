using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APT
{
    /// <summary>
    /// UC-06 — עורך תבנית: מציג את שדות התבנית ("קטגוריית השדות"), ומאפשר
    /// לבחור שדה לעריכה, להוסיף שדה חדש, למחוק שדה קיים, ולשמור את התבנית
    /// תוך עדכון יום ושעת העדכון האחרון.
    /// </summary>
    public partial class ReportTemplateEditorPanel : UserControl
    {
        private SeniorAccountant currentUser;
        private ReportTemplate template;
        private TemplateField selectedField = null;

        public ReportTemplateEditorPanel(SeniorAccountant user, ReportTemplate template)
        {
            InitializeComponent();
            UiCenter.Enable(this);
            this.currentUser = user;
            this.template = template;
            lblTitle.Text = "עריכת תבנית: " + template.getTemplateName() + "  (" + template.getReportType() + ")";
            RefreshLastUpdated();
            LoadFields();
        }

        private void RefreshLastUpdated()
        {
            lblLastUpdated.Text = "עודכן לאחרונה: " + template.getLastUpdated().ToString("dd/MM/yyyy HH:mm");
        }

        private void LoadFields()
        {
            listFields.Items.Clear();
            if (Program.TemplateFields != null)
            {
                foreach (TemplateField f in Program.TemplateFields)
                {
                    if (f.getTemplateId() == template.getTemplateId())
                    {
                        ListViewItem item = new ListViewItem(f.getFieldName());
                        item.Tag = f;
                        listFields.Items.Add(item);
                    }
                }
            }
        }

        private void listFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFields.SelectedItems.Count > 0)
            {
                selectedField = (TemplateField)listFields.SelectedItems[0].Tag;
                txtFieldName.Text = selectedField.getFieldName();
            }
        }

        // הוספת שדה חדש לבסיס הנתונים
        private void btnAddField_Click(object sender, EventArgs e)
        {
            string name = txtFieldName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("אנא הזן שם שדה", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFieldName.Focus();
                return;
            }
            // מקרה קצה: שדה כפול באותה תבנית
            foreach (TemplateField f in Program.TemplateFields)
            {
                if (f.getTemplateId() == template.getTemplateId() && f.getFieldName() == name)
                {
                    MessageBox.Show("שדה בשם '" + name + "' כבר קיים בתבנית", "כפילות", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            int nextId = TemplateField.getNextFieldId();
            new TemplateField(nextId, template.getTemplateId(), name, true);
            MessageBox.Show("השדה נוסף בהצלחה", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtFieldName.Text = "";
            selectedField = null;
            LoadFields();
        }

        // עדכון שם השדה הנבחר
        private void btnUpdateField_Click(object sender, EventArgs e)
        {
            if (selectedField == null)
            {
                MessageBox.Show("אנא בחר שדה לעדכון", "לא נבחר שדה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string name = txtFieldName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("אנא הזן שם שדה", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFieldName.Focus();
                return;
            }
            selectedField.setFieldName(name);
            selectedField.updateTemplateField();
            MessageBox.Show("השדה עודכן בהצלחה", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadFields();
        }

        // מחיקת השדה הנבחר
        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            if (selectedField == null)
            {
                MessageBox.Show("אנא בחר שדה למחיקה", "לא נבחר שדה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("למחוק את השדה '" + selectedField.getFieldName() + "'?", "אישור מחיקה",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                selectedField.deleteTemplateField();
                MessageBox.Show("השדה נמחק בהצלחה", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                selectedField = null;
                txtFieldName.Text = "";
                LoadFields();
            }
        }

        // שמירת התבנית — מעדכן את יום ושעת העדכון האחרון
        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            template.setLastUpdated(DateTime.Now);
            template.updateReportTemplate();
            RefreshLastUpdated();
            MessageBox.Show("התבנית נשמרה בהצלחה.\nמועד עדכון: " + template.getLastUpdated().ToString("dd/MM/yyyy HH:mm"),
                            "נשמר", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // יצירת PDF ויזואלי של התבנית הריקה והצגתו בצופה ברירת המחדל
        private void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            try
            {
                List<TemplateField> fields = new List<TemplateField>();
                if (Program.TemplateFields != null)
                    foreach (TemplateField f in Program.TemplateFields)
                        if (f.getTemplateId() == template.getTemplateId())
                            fields.Add(f);

                string safeName = string.Concat(template.getReportType().Split(System.IO.Path.GetInvalidFileNameChars()));
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "APT_Template_" + safeName + ".pdf");

                TemplatePdfGenerator.Generate(template, fields, path);

                // הצגת ה-PDF בצופה ברירת המחדל של המחשב
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה ביצירת ה-PDF: " + ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHome_Click(object sender, EventArgs e) { mainForm.showPanel(new SeniorAccountantHomePanel(currentUser)); }
        private void btnNavBack_Click(object sender, EventArgs e) { mainForm.showPanel(new ReportTemplateMenuPanel(currentUser)); }
        private void btnNavLogout_Click(object sender, EventArgs e) { mainForm.showPanel(new LoginPanel()); }
    }
}

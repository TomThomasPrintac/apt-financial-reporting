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
    public partial class SourceFilePanel : UserControl
    {
        private User currentUser;
        private SourceFile selectedFile = null;

        public SourceFilePanel(User user)
        {
            InitializeComponent();
            UiCenter.Enable(this);
            this.currentUser = user;
            LoadFilesList();
            InitializeComboBoxes();
            PopulateCaseCombo();
        }

        // ניווט חזרה לתפריט הבית המתאים לתפקיד המשתמש (אינטרן או רואה חשבון בכיר)
        private void GoToHome()
        {
            if (currentUser is SeniorAccountant sa)
                mainForm.showPanel(new SeniorAccountantHomePanel(sa));
            else if (currentUser is Intern intern)
                mainForm.showPanel(new InternHomePanel(intern));
            else
                mainForm.showPanel(new LoginPanel());
        }

        // מילוי רשימת התיקים הקיימים — בחירה מרשימה מונעת מצב של "תיק לא קיים"
        private void PopulateCaseCombo()
        {
            cmbCaseId.Items.Clear();
            if (Program.ClientCases != null)
                foreach (ClientCase cc in Program.ClientCases)
                    cmbCaseId.Items.Add(cc.getCaseId());
            if (cmbCaseId.Items.Count > 0) cmbCaseId.SelectedIndex = 0;
        }

        private void InitializeComboBoxes()
        {
            cmbFileType.Items.AddRange(new object[] { "Payroll", "TrialBalance", "Depreciation", "Ledger" });
            cmbStatus.Items.AddRange(new object[] { "Uploaded", "Processing", "Processed" });
            cmbDataFormat.Items.AddRange(new object[] { "CSV", "Excel", "XML" });
            // ברירת מחדל לכל ComboBox — מונע NullReferenceException אם שומרים בלי לבחור
            cmbFileType.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbDataFormat.SelectedIndex = 0;
        }

        private void LoadFilesList()
        {
            listFiles.Items.Clear();
            if (Program.SourceFiles != null)
            {
                foreach (SourceFile sf in Program.SourceFiles)
                {
                    ListViewItem item = new ListViewItem(sf.getFileId().ToString());
                    item.SubItems.Add(sf.getCaseId().ToString());
                    item.SubItems.Add(sf.getFileName());
                    item.SubItems.Add(sf.getStatus());
                    item.Tag = sf;
                    listFiles.Items.Add(item);
                }
            }
        }

        private void listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count > 0)
            {
                selectedFile = (SourceFile)listFiles.SelectedItems[0].Tag;
                PopulateFields(selectedFile);
            }
        }

        private void PopulateFields(SourceFile sf)
        {
            txtFileId.Text = sf.getFileId().ToString();
            cmbCaseId.SelectedItem = sf.getCaseId();
            txtFileName.Text = sf.getFileName();
            cmbFileType.SelectedItem = sf.getFileType();
            dtUploadDate.Value = sf.getUploadDate();
            cmbStatus.SelectedItem = sf.getStatus();
            cmbDataFormat.SelectedItem = sf.getDataFormat();
        }

        private void ClearFields()
        {
            txtFileId.Text = "";
            if (cmbCaseId.Items.Count > 0) cmbCaseId.SelectedIndex = 0;
            txtFileName.Text = "";
            cmbFileType.SelectedIndex = 0;
            dtUploadDate.Value = DateTime.Now;
            cmbStatus.SelectedIndex = 0;
            cmbDataFormat.SelectedIndex = 0;
            selectedFile = null;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearFields();
            listFiles.SelectedItems.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // --- ולידציה: שם קובץ (חובה) ---
                if (string.IsNullOrWhiteSpace(txtFileName.Text))
                {
                    MessageBox.Show("אנא הזן שם קובץ", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFileName.Focus();
                    return;
                }
                // --- ולידציה: תיק (בחירה מרשימת התיקים הקיימים) ---
                if (cmbCaseId.SelectedItem == null)
                {
                    MessageBox.Show("אנא בחר תיק מהרשימה (אם הרשימה ריקה — צור תיק תחילה במסך 'ניהול תיקים')", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int caseId = (int)cmbCaseId.SelectedItem;
                // --- ולידציה: בחירות ComboBox ---
                if (cmbFileType.SelectedItem == null || cmbStatus.SelectedItem == null || cmbDataFormat.SelectedItem == null)
                {
                    MessageBox.Show("אנא בחר סוג קובץ, סטטוס ופורמט", "שדה חסר", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nextId = SourceFile.getNextFileId();
                SourceFile newFile = new SourceFile(nextId, caseId, txtFileName.Text.Trim(),
                                                    cmbFileType.SelectedItem.ToString(), dtUploadDate.Value,
                                                    cmbStatus.SelectedItem.ToString(), cmbDataFormat.SelectedItem.ToString(), true);

                MessageBox.Show("הקובץ נוסף בהצלחה כרשומה חדשה", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadFilesList();
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
                if (selectedFile == null)
                {
                    MessageBox.Show("אנא בחר קובץ לעדכון", "שגיאה");
                    return;
                }

                selectedFile.setStatus(cmbStatus.SelectedItem.ToString());
                selectedFile.updateSourceFile();
                MessageBox.Show("הקובץ עודכן בהצלחה", "הודעה");
                LoadFilesList();
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
                if (selectedFile == null)
                {
                    MessageBox.Show("אנא בחר קובץ למחיקה", "שגיאה");
                    return;
                }

                DialogResult result = MessageBox.Show("האם אתה בטוח שברצונך למחוק את הקובץ?", "אישור",
                                                      MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    selectedFile.deleteSourceFile();
                    MessageBox.Show("הקובץ נמחק בהצלחה", "הודעה");
                    LoadFilesList();
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
            GoToHome();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            GoToHome();
        }

        private void btnNavBack_Click(object sender, EventArgs e)
        {
            GoToHome();
        }

        // --- צירוף קובץ מהמחשב (UC-02) ---

        // עיון: פתיחת חלון בחירת קובץ סטנדרטי
        private void btnAttach_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "בחר קובץ מקור לצירוף";
                ofd.Filter = "קבצי מקור (Excel/CSV/XML)|*.xlsx;*.xls;*.csv;*.xml|כל הקבצים|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                    ApplyPickedFile(ofd.FileName);
            }
        }

        // גרירה: אישור שגוררים קובץ
        private void dropZone_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        // גרירה: שחרור הקובץ באזור הגרירה
        private void dropZone_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
                ApplyPickedFile(files[0]);
        }

        // צירוף קובץ: מתחיל תמיד רשומה חדשה לצד הקבצים הקיימים — לא מחליף רשומה שנבחרה.
        // מבטל את הבחירה הנוכחית, מנקה את המזהה, וקובע ערכי ברירת מחדל להעלאה חדשה.
        // התיק שנבחר נשמר כדי שאפשר יהיה לצרף כמה קבצים לאותו תיק בזה אחר זה.
        private void ApplyPickedFile(string path)
        {
            listFiles.SelectedItems.Clear();
            selectedFile = null;
            txtFileId.Text = "";
            cmbStatus.SelectedItem = "Uploaded";
            dtUploadDate.Value = DateTime.Now;

            string name = System.IO.Path.GetFileName(path);
            txtFileName.Text = name;
            string ext = System.IO.Path.GetExtension(path).ToLowerInvariant();
            if (ext == ".csv") cmbDataFormat.SelectedItem = "CSV";
            else if (ext == ".xlsx" || ext == ".xls") cmbDataFormat.SelectedItem = "Excel";
            else if (ext == ".xml") cmbDataFormat.SelectedItem = "XML";
            lblAttached.Text = "צורף: " + name + " (יתווסף כקובץ חדש)";
            lblAttached.ForeColor = System.Drawing.Color.Green;
        }

        private void btnNavLogout_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new LoginPanel());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APT
{
    static class Program
    {
        // =====================================================================
        // Static lists — in-memory storage for all entities
        // Load order: base entities first, then entities with FK dependencies
        // =====================================================================

        // Base class + subtypes
        public static List<User> Users;
        public static List<SeniorAccountant> SeniorAccountants;
        public static List<Intern> Interns;
        public static List<Bookkeeper> Bookkeepers;
        public static List<SystemAdministrator> SystemAdministrators;
        public static List<Client> Clients;

        // Domain entities
        public static List<ClientCase> ClientCases;
        public static List<SourceFile> SourceFiles;
        public static List<LedgerEntry> LedgerEntries;
        public static List<PayrollRecord> PayrollRecords;
        public static List<PayrollLedgerMatch> PayrollLedgerMatches;
        public static List<FinancialReport> FinancialReports;

        // UC-06 — תבניות דוחות (לפי סוג קובץ מקור) + השדות שלהן
        public static List<ReportTemplate> ReportTemplates;
        public static List<TemplateField> TemplateFields;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            // מטפלי שגיאות גלובליים — כדי שתקלה לא צפויה לא תפיל את האפליקציה בלי הסבר
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
                MessageBox.Show("שגיאה לא צפויה: " + e.Exception.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                MessageBox.Show("שגיאה קריטית: " + (e.ExceptionObject as Exception)?.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // טעינת הנתונים מה-DB עטופה ב-try/catch — כשל טעינה לא ימנע פתיחת מסך הכניסה
            try
            {
                initLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show("טעינת הנתונים נכשלה: " + ex.Message + "\nהמערכת תיפתח עם נתונים חלקיים.",
                    "אזהרה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Application.Run(new mainForm());
        }

        /// <summary>
        /// Load all entities from database into in-memory lists.
        /// Strict load order: base entities first, then entities with FK dependencies.
        /// </summary>
        public static void initLists()
        {
            // 1. Load base User class
            User.initUsers();

            // 2. Load User subtypes (depend on Users being loaded)
            SeniorAccountant.initSeniorAccountants();
            Intern.initInterns();
            Bookkeeper.initBookkeepers();
            SystemAdministrator.initSystemAdministrators();
            Client.initClients();

            // 3. Load ClientCase (no FK dependencies within domain entities)
            ClientCase.initClientCases();

            // 4. Load transactional entities (reference ClientCase)
            SourceFile.initSourceFiles();
            LedgerEntry.initLedgerEntries();
            PayrollRecord.initPayrollRecords();

            // 5. Load association class (references PayrollRecord and LedgerEntry)
            PayrollLedgerMatch.initPayrollLedgerMatches();

            // 6. Load FinancialReport (references ClientCase and SeniorAccountant)
            FinancialReport.initFinancialReports();

            // 7. Load UC-06 templates: ReportTemplate (base), then TemplateField (references ReportTemplate)
            ReportTemplate.initReportTemplates();
            TemplateField.initTemplateFields();
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class FinancialReport
    {
        private int reportId;
        private int caseId;
        private string reportType;
        private string reportFormat;
        private DateTime generatedDate;
        private bool isSigned;
        private DateTime signedDate;
        private string status;
        private int seniorAccountantId;

        public FinancialReport(int reportId, int caseId, string reportType, string reportFormat,
                              DateTime generatedDate, bool isSigned, DateTime signedDate,
                              string status, int seniorAccountantId, bool is_new)
        {
            this.reportId = reportId;
            this.caseId = caseId;
            this.reportType = reportType;
            this.reportFormat = reportFormat;
            this.generatedDate = generatedDate;
            this.isSigned = isSigned;
            this.signedDate = signedDate;
            this.status = status;
            this.seniorAccountantId = seniorAccountantId;
            if (is_new)
            {
                createFinancialReport();
                Program.FinancialReports.Add(this);
            }
        }

        public int getReportId() { return this.reportId; }
        public int getCaseId() { return this.caseId; }
        public string getReportType() { return this.reportType; }
        public string getReportFormat() { return this.reportFormat; }
        public DateTime getGeneratedDate() { return this.generatedDate; }
        public bool getIsSigned() { return this.isSigned; }
        public DateTime getSignedDate() { return this.signedDate; }
        public string getStatus() { return this.status; }
        public int getSeniorAccountantId() { return this.seniorAccountantId; }

        public void setIsSigned(bool isSigned) { this.isSigned = isSigned; }
        public void setSignedDate(DateTime signedDate) { this.signedDate = signedDate; }
        public void setStatus(string status) { this.status = status; }
        public void setSeniorAccountantId(int seniorAccountantId) { this.seniorAccountantId = seniorAccountantId; }

        public void sign()
        {
            this.isSigned = true;
            this.signedDate = DateTime.Now;
            this.status = "Signed";
        }

        public void createFinancialReport()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_FinancialReports_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@report_id", this.reportId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@reportType", this.reportType);
            cmd.Parameters.AddWithValue("@reportFormat", this.reportFormat);
            cmd.Parameters.AddWithValue("@generatedDate", this.generatedDate);
            cmd.Parameters.AddWithValue("@isSigned", this.isSigned);
            cmd.Parameters.AddWithValue("@signedDate", this.signedDate);
            cmd.Parameters.AddWithValue("@status", this.status);
            cmd.Parameters.AddWithValue("@senior_accountant_id", this.seniorAccountantId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateFinancialReport()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_FinancialReports_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@report_id", this.reportId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@reportType", this.reportType);
            cmd.Parameters.AddWithValue("@reportFormat", this.reportFormat);
            cmd.Parameters.AddWithValue("@generatedDate", this.generatedDate);
            cmd.Parameters.AddWithValue("@isSigned", this.isSigned);
            cmd.Parameters.AddWithValue("@signedDate", this.signedDate);
            cmd.Parameters.AddWithValue("@status", this.status);
            cmd.Parameters.AddWithValue("@senior_accountant_id", this.seniorAccountantId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteFinancialReport()
        {
            Program.FinancialReports.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_FinancialReports_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@report_id", this.reportId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextReportId()
        {
            if (Program.FinancialReports == null || Program.FinancialReports.Count == 0)
                return 1;
            return Program.FinancialReports[Program.FinancialReports.Count - 1].getReportId() + 1;
        }

        public static void initFinancialReports()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_FinancialReports_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.FinancialReports = new List<FinancialReport>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int reportId = rdr.GetInt32(0);
                int caseId = rdr.GetInt32(1);
                string reportType = rdr.GetString(2);
                string reportFormat = rdr.GetString(3);
                DateTime generatedDate = rdr.GetDateTime(4);
                bool isSigned = rdr.GetBoolean(5);
                DateTime signedDate = rdr.IsDBNull(6) ? DateTime.MinValue : rdr.GetDateTime(6);
                string status = rdr.GetString(7);
                int seniorAccountantId = rdr.IsDBNull(8) ? 0 : rdr.GetInt32(8);

                FinancialReport fr = new FinancialReport(reportId, caseId, reportType, reportFormat,
                                                        generatedDate, isSigned, signedDate,
                                                        status, seniorAccountantId, false);
                Program.FinancialReports.Add(fr);
            }
            rdr.Close();
        }

        public static FinancialReport seekFinancialReport(int id)
        {
            foreach (FinancialReport fr in Program.FinancialReports)
            {
                if (fr.getReportId() == id)
                    return fr;
            }
            return null;
        }
    }
}

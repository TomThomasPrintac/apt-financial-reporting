using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    /// <summary>
    /// תבנית דו"ח (UC-06) — מגדירה את מבנה השדות של דו"ח כספי (מאזן / רווח-והפסד / תזרים).
    /// כל תבנית מכילה אוסף TemplateField, ושומרת את מועד העדכון האחרון (lastUpdated).
    /// </summary>
    public class ReportTemplate
    {
        private int templateId;
        private string reportType;
        private string templateName;
        private DateTime lastUpdated;

        public ReportTemplate(int templateId, string reportType, string templateName, DateTime lastUpdated, bool is_new)
        {
            this.templateId = templateId;
            this.reportType = reportType;
            this.templateName = templateName;
            this.lastUpdated = lastUpdated;
            if (is_new)
            {
                createReportTemplate();
                Program.ReportTemplates.Add(this);
            }
        }

        public int getTemplateId() { return this.templateId; }
        public string getReportType() { return this.reportType; }
        public string getTemplateName() { return this.templateName; }
        public DateTime getLastUpdated() { return this.lastUpdated; }

        public void setReportType(string reportType) { this.reportType = reportType; }
        public void setTemplateName(string templateName) { this.templateName = templateName; }
        public void setLastUpdated(DateTime lastUpdated) { this.lastUpdated = lastUpdated; }

        public void createReportTemplate()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ReportTemplates_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@template_id", this.templateId);
            cmd.Parameters.AddWithValue("@reportType", this.reportType);
            cmd.Parameters.AddWithValue("@templateName", this.templateName);
            cmd.Parameters.AddWithValue("@lastUpdated", this.lastUpdated);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateReportTemplate()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ReportTemplates_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@template_id", this.templateId);
            cmd.Parameters.AddWithValue("@reportType", this.reportType);
            cmd.Parameters.AddWithValue("@templateName", this.templateName);
            cmd.Parameters.AddWithValue("@lastUpdated", this.lastUpdated);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteReportTemplate()
        {
            Program.ReportTemplates.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ReportTemplates_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@template_id", this.templateId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextReportTemplateId()
        {
            if (Program.ReportTemplates == null || Program.ReportTemplates.Count == 0)
                return 1;
            return Program.ReportTemplates[Program.ReportTemplates.Count - 1].getTemplateId() + 1;
        }

        public static void initReportTemplates()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ReportTemplates_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.ReportTemplates = new List<ReportTemplate>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int templateId = rdr.GetInt32(0);
                string reportType = rdr.GetString(1);
                string templateName = rdr.GetString(2);
                DateTime lastUpdated = rdr.GetDateTime(3);

                ReportTemplate t = new ReportTemplate(templateId, reportType, templateName, lastUpdated, false);
                Program.ReportTemplates.Add(t);
            }
            rdr.Close();
        }

        public static ReportTemplate seekReportTemplate(int id)
        {
            foreach (ReportTemplate t in Program.ReportTemplates)
            {
                if (t.getTemplateId() == id)
                    return t;
            }
            return null;
        }
    }
}

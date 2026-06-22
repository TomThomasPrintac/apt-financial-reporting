using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class ClientCase
    {
        private int caseId;
        private int clientId;
        private DateTime reportingPeriod;
        private string status;
        private DateTime createdDate;
        private DateTime dueDate;
        private int assignedTo;

        public ClientCase(int caseId, int clientId, DateTime reportingPeriod, string status,
                         DateTime createdDate, DateTime dueDate, int assignedTo, bool is_new)
        {
            this.caseId = caseId;
            this.clientId = clientId;
            this.reportingPeriod = reportingPeriod;
            this.status = status;
            this.createdDate = createdDate;
            this.dueDate = dueDate;
            this.assignedTo = assignedTo;
            if (is_new)
            {
                createClientCase();
                Program.ClientCases.Add(this);
            }
        }

        public int getCaseId() { return this.caseId; }
        public int getClientId() { return this.clientId; }
        public DateTime getReportingPeriod() { return this.reportingPeriod; }
        public string getStatus() { return this.status; }
        public DateTime getCreatedDate() { return this.createdDate; }
        public DateTime getDueDate() { return this.dueDate; }
        public int getAssignedTo() { return this.assignedTo; }

        public void setStatus(string status) { this.status = status; }
        public void setDueDate(DateTime dueDate) { this.dueDate = dueDate; }
        public void setAssignedTo(int assignedTo) { this.assignedTo = assignedTo; }

        public void createClientCase()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ClientCases_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@clientId", this.clientId);
            cmd.Parameters.AddWithValue("@reportingPeriod", this.reportingPeriod);
            cmd.Parameters.AddWithValue("@status", this.status);
            cmd.Parameters.AddWithValue("@createdDate", this.createdDate);
            cmd.Parameters.AddWithValue("@dueDate", this.dueDate);
            cmd.Parameters.AddWithValue("@assignedTo", this.assignedTo);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateClientCase()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ClientCases_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@clientId", this.clientId);
            cmd.Parameters.AddWithValue("@reportingPeriod", this.reportingPeriod);
            cmd.Parameters.AddWithValue("@status", this.status);
            cmd.Parameters.AddWithValue("@createdDate", this.createdDate);
            cmd.Parameters.AddWithValue("@dueDate", this.dueDate);
            cmd.Parameters.AddWithValue("@assignedTo", this.assignedTo);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteClientCase()
        {
            Program.ClientCases.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ClientCases_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextCaseId()
        {
            if (Program.ClientCases == null || Program.ClientCases.Count == 0)
                return 1;
            return Program.ClientCases[Program.ClientCases.Count - 1].getCaseId() + 1;
        }

        public static void initClientCases()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ClientCases_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.ClientCases = new List<ClientCase>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int caseId = rdr.GetInt32(0);
                int clientId = rdr.GetInt32(1);
                DateTime reportingPeriod = rdr.GetDateTime(2);
                string status = rdr.GetString(3);
                DateTime createdDate = rdr.GetDateTime(4);
                DateTime dueDate = rdr.GetDateTime(5);
                int assignedTo = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6);

                ClientCase cc = new ClientCase(caseId, clientId, reportingPeriod, status,
                                               createdDate, dueDate, assignedTo, false);
                Program.ClientCases.Add(cc);
            }
            rdr.Close();
        }

        public static ClientCase seekClientCase(int id)
        {
            foreach (ClientCase cc in Program.ClientCases)
            {
                if (cc.getCaseId() == id)
                    return cc;
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class LedgerEntry
    {
        private int entryId;
        private int caseId;
        private string accountCode;
        private string accountName;
        private string accountType;
        private decimal amount;
        private DateTime reportingPeriod;
        private bool isNew;

        public LedgerEntry(int entryId, int caseId, string accountCode, string accountName,
                          string accountType, decimal amount, DateTime reportingPeriod, bool isNewFlag, bool is_new)
        {
            this.entryId = entryId;
            this.caseId = caseId;
            this.accountCode = accountCode;
            this.accountName = accountName;
            this.accountType = accountType;
            this.amount = amount;
            this.reportingPeriod = reportingPeriod;
            this.isNew = isNewFlag;
            if (is_new)
            {
                createLedgerEntry();
                Program.LedgerEntries.Add(this);
            }
        }

        public int getEntryId() { return this.entryId; }
        public int getCaseId() { return this.caseId; }
        public string getAccountCode() { return this.accountCode; }
        public string getAccountName() { return this.accountName; }
        public string getAccountType() { return this.accountType; }
        public decimal getAmount() { return this.amount; }
        public DateTime getReportingPeriod() { return this.reportingPeriod; }
        public bool getIsNew() { return this.isNew; }

        public void setAmount(decimal amount) { this.amount = amount; }

        public void createLedgerEntry()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_LedgerEntries_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@entry_id", this.entryId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@accountCode", this.accountCode);
            cmd.Parameters.AddWithValue("@accountName", this.accountName);
            cmd.Parameters.AddWithValue("@accountType", this.accountType);
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@reportingPeriod", this.reportingPeriod);
            cmd.Parameters.AddWithValue("@isNew", this.isNew);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateLedgerEntry()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_LedgerEntries_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@entry_id", this.entryId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@accountCode", this.accountCode);
            cmd.Parameters.AddWithValue("@accountName", this.accountName);
            cmd.Parameters.AddWithValue("@accountType", this.accountType);
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@reportingPeriod", this.reportingPeriod);
            cmd.Parameters.AddWithValue("@isNew", this.isNew);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteLedgerEntry()
        {
            Program.LedgerEntries.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_LedgerEntries_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@entry_id", this.entryId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextEntryId()
        {
            if (Program.LedgerEntries == null || Program.LedgerEntries.Count == 0)
                return 1;
            return Program.LedgerEntries[Program.LedgerEntries.Count - 1].getEntryId() + 1;
        }

        public static void initLedgerEntries()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_LedgerEntries_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.LedgerEntries = new List<LedgerEntry>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int entryId = rdr.GetInt32(0);
                int caseId = rdr.GetInt32(1);
                string accountCode = rdr.GetString(2);
                string accountName = rdr.GetString(3);
                string accountType = rdr.GetString(4);
                decimal amount = rdr.GetDecimal(5);
                DateTime reportingPeriod = rdr.GetDateTime(6);
                bool isNewFlag = rdr.GetBoolean(7);

                LedgerEntry le = new LedgerEntry(entryId, caseId, accountCode, accountName,
                                                 accountType, amount, reportingPeriod, isNewFlag, false);
                Program.LedgerEntries.Add(le);
            }
            rdr.Close();
        }

        public static LedgerEntry seekLedgerEntry(int id)
        {
            foreach (LedgerEntry le in Program.LedgerEntries)
            {
                if (le.getEntryId() == id)
                    return le;
            }
            return null;
        }
    }
}

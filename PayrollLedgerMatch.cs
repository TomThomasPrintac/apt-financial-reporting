using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class PayrollLedgerMatch
    {
        private int matchId;
        private int payrollId;
        private int entryId;
        private int caseId;
        private string matchStatus;
        private decimal varianceAmount;
        private DateTime matchedDate;
        private string notes;
        private PayrollRecord payrollRecord;
        private LedgerEntry ledgerEntry;

        public PayrollLedgerMatch(int matchId, int payrollId, int entryId, int caseId,
                                string matchStatus, decimal varianceAmount, DateTime matchedDate,
                                string notes, bool is_new)
        {
            this.matchId = matchId;
            this.payrollId = payrollId;
            this.entryId = entryId;
            this.caseId = caseId;
            this.matchStatus = matchStatus;
            this.varianceAmount = varianceAmount;
            this.matchedDate = matchedDate;
            this.notes = notes;
            this.payrollRecord = null;
            this.ledgerEntry = null;
            if (is_new)
            {
                createPayrollLedgerMatch();
                Program.PayrollLedgerMatches.Add(this);
            }
        }

        public int getMatchId() { return this.matchId; }
        public int getPayrollId() { return this.payrollId; }
        public int getEntryId() { return this.entryId; }
        public int getCaseId() { return this.caseId; }
        public string getMatchStatus() { return this.matchStatus; }
        public decimal getVarianceAmount() { return this.varianceAmount; }
        public DateTime getMatchedDate() { return this.matchedDate; }
        public string getNotes() { return this.notes; }
        public PayrollRecord getPayrollRecord() { return this.payrollRecord; }
        public LedgerEntry getLedgerEntry() { return this.ledgerEntry; }

        public void setMatchStatus(string matchStatus) { this.matchStatus = matchStatus; }
        public void setVarianceAmount(decimal varianceAmount) { this.varianceAmount = varianceAmount; }
        public void setNotes(string notes) { this.notes = notes; }

        public void setPayrollRecord(PayrollRecord pr) { this.payrollRecord = pr; }
        public void setLedgerEntry(LedgerEntry le) { this.ledgerEntry = le; }

        public decimal calculateVariance()
        {
            if (this.payrollRecord != null && this.ledgerEntry != null)
            {
                return this.payrollRecord.getGrossAmount() - this.ledgerEntry.getAmount();
            }
            return 0;
        }

        public void flag()
        {
            if (this.varianceAmount != 0)
                this.matchStatus = "Requires Review";
        }

        public void createPayrollLedgerMatch()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_PayrollLedgerMatches_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@match_id", this.matchId);
            cmd.Parameters.AddWithValue("@payroll_id", this.payrollId);
            cmd.Parameters.AddWithValue("@entry_id", this.entryId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@matchStatus", this.matchStatus);
            cmd.Parameters.AddWithValue("@varianceAmount", this.varianceAmount);
            cmd.Parameters.AddWithValue("@matchedDate", this.matchedDate);
            cmd.Parameters.AddWithValue("@notes", this.notes ?? "");
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updatePayrollLedgerMatch()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_PayrollLedgerMatches_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@match_id", this.matchId);
            cmd.Parameters.AddWithValue("@payroll_id", this.payrollId);
            cmd.Parameters.AddWithValue("@entry_id", this.entryId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@matchStatus", this.matchStatus);
            cmd.Parameters.AddWithValue("@varianceAmount", this.varianceAmount);
            cmd.Parameters.AddWithValue("@matchedDate", this.matchedDate);
            cmd.Parameters.AddWithValue("@notes", this.notes ?? "");
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deletePayrollLedgerMatch()
        {
            Program.PayrollLedgerMatches.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_PayrollLedgerMatches_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@match_id", this.matchId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextMatchId()
        {
            if (Program.PayrollLedgerMatches == null || Program.PayrollLedgerMatches.Count == 0)
                return 1;
            return Program.PayrollLedgerMatches[Program.PayrollLedgerMatches.Count - 1].getMatchId() + 1;
        }

        public static void initPayrollLedgerMatches()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_PayrollLedgerMatches_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.PayrollLedgerMatches = new List<PayrollLedgerMatch>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int matchId = rdr.GetInt32(0);
                int payrollId = rdr.GetInt32(1);
                int entryId = rdr.GetInt32(2);
                int caseId = rdr.GetInt32(3);
                string matchStatus = rdr.GetString(4);
                decimal varianceAmount = rdr.GetDecimal(5);
                DateTime matchedDate = rdr.GetDateTime(6);
                string notes = rdr.IsDBNull(7) ? "" : rdr.GetString(7);

                PayrollLedgerMatch plm = new PayrollLedgerMatch(matchId, payrollId, entryId, caseId,
                                                                matchStatus, varianceAmount, matchedDate,
                                                                notes, false);

                plm.setPayrollRecord(PayrollRecord.seekPayrollRecord(payrollId));
                plm.setLedgerEntry(LedgerEntry.seekLedgerEntry(entryId));

                Program.PayrollLedgerMatches.Add(plm);
            }
            rdr.Close();
        }

        public static PayrollLedgerMatch seekPayrollLedgerMatch(int id)
        {
            foreach (PayrollLedgerMatch plm in Program.PayrollLedgerMatches)
            {
                if (plm.getMatchId() == id)
                    return plm;
            }
            return null;
        }
    }
}

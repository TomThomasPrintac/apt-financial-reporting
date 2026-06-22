using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class PayrollRecord
    {
        private int payrollId;
        private int caseId;
        private string employeeId;
        private string employeeName;
        private decimal grossAmount;
        private decimal socialContributions;
        private decimal employerCosts;
        private DateTime reportingPeriod;
        private bool isNew;

        public PayrollRecord(int payrollId, int caseId, string employeeId, string employeeName,
                            decimal grossAmount, decimal socialContributions, decimal employerCosts,
                            DateTime reportingPeriod, bool isNewFlag, bool is_new)
        {
            this.payrollId = payrollId;
            this.caseId = caseId;
            this.employeeId = employeeId;
            this.employeeName = employeeName;
            this.grossAmount = grossAmount;
            this.socialContributions = socialContributions;
            this.employerCosts = employerCosts;
            this.reportingPeriod = reportingPeriod;
            this.isNew = isNewFlag;
            if (is_new)
            {
                createPayrollRecord();
                Program.PayrollRecords.Add(this);
            }
        }

        public int getPayrollId() { return this.payrollId; }
        public int getCaseId() { return this.caseId; }
        public string getEmployeeId() { return this.employeeId; }
        public string getEmployeeName() { return this.employeeName; }
        public decimal getGrossAmount() { return this.grossAmount; }
        public decimal getSocialContributions() { return this.socialContributions; }
        public decimal getEmployerCosts() { return this.employerCosts; }
        public DateTime getReportingPeriod() { return this.reportingPeriod; }
        public bool getIsNew() { return this.isNew; }

        public decimal getTotalCost() { return this.grossAmount + this.employerCosts; }

        public void setGrossAmount(decimal grossAmount) { this.grossAmount = grossAmount; }
        public void setSocialContributions(decimal socialContributions) { this.socialContributions = socialContributions; }
        public void setEmployerCosts(decimal employerCosts) { this.employerCosts = employerCosts; }

        public void createPayrollRecord()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_PayrollRecords_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@payroll_id", this.payrollId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@employeeId", this.employeeId);
            cmd.Parameters.AddWithValue("@employeeName", this.employeeName);
            cmd.Parameters.AddWithValue("@grossAmount", this.grossAmount);
            cmd.Parameters.AddWithValue("@socialContributions", this.socialContributions);
            cmd.Parameters.AddWithValue("@employerCosts", this.employerCosts);
            cmd.Parameters.AddWithValue("@reportingPeriod", this.reportingPeriod);
            cmd.Parameters.AddWithValue("@isNew", this.isNew);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updatePayrollRecord()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_PayrollRecords_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@payroll_id", this.payrollId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@employeeId", this.employeeId);
            cmd.Parameters.AddWithValue("@employeeName", this.employeeName);
            cmd.Parameters.AddWithValue("@grossAmount", this.grossAmount);
            cmd.Parameters.AddWithValue("@socialContributions", this.socialContributions);
            cmd.Parameters.AddWithValue("@employerCosts", this.employerCosts);
            cmd.Parameters.AddWithValue("@reportingPeriod", this.reportingPeriod);
            cmd.Parameters.AddWithValue("@isNew", this.isNew);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deletePayrollRecord()
        {
            Program.PayrollRecords.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_PayrollRecords_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@payroll_id", this.payrollId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextPayrollId()
        {
            if (Program.PayrollRecords == null || Program.PayrollRecords.Count == 0)
                return 1;
            return Program.PayrollRecords[Program.PayrollRecords.Count - 1].getPayrollId() + 1;
        }

        public static void initPayrollRecords()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_PayrollRecords_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.PayrollRecords = new List<PayrollRecord>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int payrollId = rdr.GetInt32(0);
                int caseId = rdr.GetInt32(1);
                string employeeId = rdr.GetString(2);
                string employeeName = rdr.GetString(3);
                decimal grossAmount = rdr.GetDecimal(4);
                decimal socialContributions = rdr.GetDecimal(5);
                decimal employerCosts = rdr.GetDecimal(6);
                DateTime reportingPeriod = rdr.GetDateTime(7);
                bool isNewFlag = rdr.GetBoolean(8);

                PayrollRecord pr = new PayrollRecord(payrollId, caseId, employeeId, employeeName,
                                                     grossAmount, socialContributions, employerCosts,
                                                     reportingPeriod, isNewFlag, false);
                Program.PayrollRecords.Add(pr);
            }
            rdr.Close();
        }

        public static PayrollRecord seekPayrollRecord(int id)
        {
            foreach (PayrollRecord pr in Program.PayrollRecords)
            {
                if (pr.getPayrollId() == id)
                    return pr;
            }
            return null;
        }
    }
}

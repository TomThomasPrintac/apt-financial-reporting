using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class SeniorAccountant : User
    {
        private int seniorAccountantId;
        private string certificationNumber;
        private string department;

        public SeniorAccountant(int userId, string email, string passwordHash, string role,
                               int seniorAccountantId, string certificationNumber, string department, bool is_new)
            : base(userId, email, passwordHash, role, false)
        {
            this.seniorAccountantId = seniorAccountantId;
            this.certificationNumber = certificationNumber;
            this.department = department;
            if (is_new)
            {
                createSeniorAccountant();
                Program.SeniorAccountants.Add(this);
            }
        }

        public int getSeniorAccountantId() { return this.seniorAccountantId; }
        public string getCertificationNumber() { return this.certificationNumber; }
        public string getDepartment() { return this.department; }

        public void setCertificationNumber(string certificationNumber) { this.certificationNumber = certificationNumber; }
        public void setDepartment(string department) { this.department = department; }

        public void createSeniorAccountant()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SeniorAccountants_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@senior_accountant_id", this.seniorAccountantId);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@certificationNumber", this.certificationNumber);
            cmd.Parameters.AddWithValue("@department", this.department);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateSeniorAccountant()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SeniorAccountants_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@senior_accountant_id", this.seniorAccountantId);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@certificationNumber", this.certificationNumber);
            cmd.Parameters.AddWithValue("@department", this.department);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteSeniorAccountant()
        {
            Program.SeniorAccountants.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SeniorAccountants_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@senior_accountant_id", this.seniorAccountantId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextSeniorAccountantId()
        {
            if (Program.SeniorAccountants == null || Program.SeniorAccountants.Count == 0)
                return 1;
            return Program.SeniorAccountants[Program.SeniorAccountants.Count - 1].getSeniorAccountantId() + 1;
        }

        public static void initSeniorAccountants()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SeniorAccountants_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.SeniorAccountants = new List<SeniorAccountant>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int seniorAccountantId = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);
                string certificationNumber = rdr.GetString(2);
                string department = rdr.GetString(3);

                User parentUser = seekUserForSubtype(userId);
                if (parentUser != null)
                {
                    SeniorAccountant sa = new SeniorAccountant(
                        parentUser.getUserId(),
                        parentUser.getEmail(),
                        parentUser.getPasswordHash(),
                        parentUser.getRole(),
                        seniorAccountantId,
                        certificationNumber,
                        department,
                        false
                    );
                    Program.SeniorAccountants.Add(sa);
                }
            }
            rdr.Close();
        }

        public static SeniorAccountant seekSeniorAccountant(int id)
        {
            foreach (SeniorAccountant sa in Program.SeniorAccountants)
            {
                if (sa.getSeniorAccountantId() == id)
                    return sa;
            }
            return null;
        }

        private static User seekUserForSubtype(int userId)
        {
            foreach (User u in Program.Users)
            {
                if (u.getUserId() == userId)
                    return u;
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class Intern : User
    {
        private int internId;
        private int supervisorId;
        private DateTime startDate;

        public Intern(int userId, string email, string passwordHash, string role,
                     int internId, int supervisorId, DateTime startDate, bool is_new)
            : base(userId, email, passwordHash, role, false)
        {
            this.internId = internId;
            this.supervisorId = supervisorId;
            this.startDate = startDate;
            if (is_new)
            {
                createIntern();
                Program.Interns.Add(this);
            }
        }

        public int getInternId() { return this.internId; }
        public int getSupervisorId() { return this.supervisorId; }
        public DateTime getStartDate() { return this.startDate; }

        public void setSupervisorId(int supervisorId) { this.supervisorId = supervisorId; }
        public void setStartDate(DateTime startDate) { this.startDate = startDate; }

        public void createIntern()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Interns_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@intern_id", this.internId);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@supervisorId", this.supervisorId);
            cmd.Parameters.AddWithValue("@startDate", this.startDate);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateIntern()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Interns_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@intern_id", this.internId);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@supervisorId", this.supervisorId);
            cmd.Parameters.AddWithValue("@startDate", this.startDate);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteIntern()
        {
            Program.Interns.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Interns_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@intern_id", this.internId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextInternId()
        {
            if (Program.Interns == null || Program.Interns.Count == 0)
                return 1;
            return Program.Interns[Program.Interns.Count - 1].getInternId() + 1;
        }

        public static void initInterns()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Interns_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Interns = new List<Intern>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int internId = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);
                int supervisorId = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2);
                DateTime startDate = rdr.GetDateTime(3);

                User parentUser = seekUserForSubtype(userId);
                if (parentUser != null)
                {
                    Intern i = new Intern(
                        parentUser.getUserId(),
                        parentUser.getEmail(),
                        parentUser.getPasswordHash(),
                        parentUser.getRole(),
                        internId,
                        supervisorId,
                        startDate,
                        false
                    );
                    Program.Interns.Add(i);
                }
            }
            rdr.Close();
        }

        public static Intern seekIntern(int id)
        {
            foreach (Intern i in Program.Interns)
            {
                if (i.getInternId() == id)
                    return i;
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

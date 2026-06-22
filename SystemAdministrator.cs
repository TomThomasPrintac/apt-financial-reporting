using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class SystemAdministrator : User
    {
        private int systemAdministratorId;

        public SystemAdministrator(int userId, string email, string passwordHash, string role,
                                 int systemAdministratorId, bool is_new)
            : base(userId, email, passwordHash, role, false)
        {
            this.systemAdministratorId = systemAdministratorId;
            if (is_new)
            {
                createSystemAdministrator();
                Program.SystemAdministrators.Add(this);
            }
        }

        public int getSystemAdministratorId() { return this.systemAdministratorId; }

        public void createSystemAdministrator()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SystemAdministrators_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@system_administrator_id", this.systemAdministratorId);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateSystemAdministrator()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SystemAdministrators_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@system_administrator_id", this.systemAdministratorId);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteSystemAdministrator()
        {
            Program.SystemAdministrators.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SystemAdministrators_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@system_administrator_id", this.systemAdministratorId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextSystemAdministratorId()
        {
            if (Program.SystemAdministrators == null || Program.SystemAdministrators.Count == 0)
                return 1;
            return Program.SystemAdministrators[Program.SystemAdministrators.Count - 1].getSystemAdministratorId() + 1;
        }

        public static void initSystemAdministrators()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SystemAdministrators_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.SystemAdministrators = new List<SystemAdministrator>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int systemAdministratorId = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);

                User parentUser = seekUserForSubtype(userId);
                if (parentUser != null)
                {
                    SystemAdministrator sa = new SystemAdministrator(
                        parentUser.getUserId(),
                        parentUser.getEmail(),
                        parentUser.getPasswordHash(),
                        parentUser.getRole(),
                        systemAdministratorId,
                        false
                    );
                    Program.SystemAdministrators.Add(sa);
                }
            }
            rdr.Close();
        }

        public static SystemAdministrator seekSystemAdministrator(int id)
        {
            foreach (SystemAdministrator sa in Program.SystemAdministrators)
            {
                if (sa.getSystemAdministratorId() == id)
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

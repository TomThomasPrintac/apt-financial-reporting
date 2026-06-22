using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class User
    {
        protected int userId;
        protected string email;
        protected string passwordHash;
        protected string role;

        public User(int userId, string email, string passwordHash, string role, bool is_new)
        {
            this.userId = userId;
            this.email = email;
            this.passwordHash = passwordHash;
            this.role = role;
            if (is_new)
            {
                createUser();
                Program.Users.Add(this);
            }
        }

        public int getUserId() { return this.userId; }
        public string getEmail() { return this.email; }
        public string getPasswordHash() { return this.passwordHash; }
        public string getRole() { return this.role; }

        public void setEmail(string email) { this.email = email; }
        public void setPasswordHash(string passwordHash) { this.passwordHash = passwordHash; }
        public void setRole(string role) { this.role = role; }

        public void createUser()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Users_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@email", this.email);
            cmd.Parameters.AddWithValue("@passwordHash", this.passwordHash);
            cmd.Parameters.AddWithValue("@role", this.role);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateUser()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Users_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@email", this.email);
            cmd.Parameters.AddWithValue("@passwordHash", this.passwordHash);
            cmd.Parameters.AddWithValue("@role", this.role);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteUser()
        {
            Program.Users.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Users_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextUserId()
        {
            if (Program.Users == null || Program.Users.Count == 0)
                return 1;
            return Program.Users[Program.Users.Count - 1].getUserId() + 1;
        }

        public static void initUsers()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Users_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Users = new List<User>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int userId = rdr.GetInt32(0);
                string email = rdr.GetString(1);
                string passwordHash = rdr.GetString(2);
                string role = rdr.GetString(3);

                User u = new User(userId, email, passwordHash, role, false);
                Program.Users.Add(u);
            }
            rdr.Close();
        }

        public static User seekUser(int id)
        {
            foreach (User u in Program.Users)
            {
                if (u.getUserId() == id)
                    return u;
            }
            return null;
        }
    }
}

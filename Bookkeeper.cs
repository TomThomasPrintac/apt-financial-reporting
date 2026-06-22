using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class Bookkeeper : User
    {
        private int bookkeeperID;
        private string certifications;

        public Bookkeeper(int userId, string email, string passwordHash, string role,
                         int bookkeeperID, string certifications, bool is_new)
            : base(userId, email, passwordHash, role, false)
        {
            this.bookkeeperID = bookkeeperID;
            this.certifications = certifications;
            if (is_new)
            {
                createBookkeeper();
                Program.Bookkeepers.Add(this);
            }
        }

        public int getBookkeeperID() { return this.bookkeeperID; }
        public string getCertifications() { return this.certifications; }

        public void setCertifications(string certifications) { this.certifications = certifications; }

        public void createBookkeeper()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Bookkeepers_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookkeeper_id", this.bookkeeperID);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@certifications", this.certifications);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateBookkeeper()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Bookkeepers_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookkeeper_id", this.bookkeeperID);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@certifications", this.certifications);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteBookkeeper()
        {
            Program.Bookkeepers.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Bookkeepers_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookkeeper_id", this.bookkeeperID);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextBookkeeperID()
        {
            if (Program.Bookkeepers == null || Program.Bookkeepers.Count == 0)
                return 1;
            return Program.Bookkeepers[Program.Bookkeepers.Count - 1].getBookkeeperID() + 1;
        }

        public static void initBookkeepers()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Bookkeepers_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Bookkeepers = new List<Bookkeeper>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int bookkeeperID = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);
                string certifications = rdr.IsDBNull(2) ? "" : rdr.GetString(2);

                User parentUser = seekUserForSubtype(userId);
                if (parentUser != null)
                {
                    Bookkeeper b = new Bookkeeper(
                        parentUser.getUserId(),
                        parentUser.getEmail(),
                        parentUser.getPasswordHash(),
                        parentUser.getRole(),
                        bookkeeperID,
                        certifications,
                        false
                    );
                    Program.Bookkeepers.Add(b);
                }
            }
            rdr.Close();
        }

        public static Bookkeeper seekBookkeeper(int id)
        {
            foreach (Bookkeeper b in Program.Bookkeepers)
            {
                if (b.getBookkeeperID() == id)
                    return b;
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

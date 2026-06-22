using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class Client : User
    {
        private int clientId;
        private string companyName;
        private string taxId;
        private int accountManager;

        public Client(int userId, string email, string passwordHash, string role,
                     int clientId, string companyName, string taxId, int accountManager, bool is_new)
            : base(userId, email, passwordHash, role, false)
        {
            this.clientId = clientId;
            this.companyName = companyName;
            this.taxId = taxId;
            this.accountManager = accountManager;
            if (is_new)
            {
                createClient();
                Program.Clients.Add(this);
            }
        }

        public int getClientId() { return this.clientId; }
        public string getCompanyName() { return this.companyName; }
        public string getTaxId() { return this.taxId; }
        public int getAccountManager() { return this.accountManager; }

        public void setCompanyName(string companyName) { this.companyName = companyName; }
        public void setTaxId(string taxId) { this.taxId = taxId; }
        public void setAccountManager(int accountManager) { this.accountManager = accountManager; }

        public void createClient()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Clients_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@client_id", this.clientId);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@companyName", this.companyName);
            cmd.Parameters.AddWithValue("@taxId", this.taxId);
            cmd.Parameters.AddWithValue("@accountManager", this.accountManager);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateClient()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Clients_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@client_id", this.clientId);
            cmd.Parameters.AddWithValue("@user_id", this.userId);
            cmd.Parameters.AddWithValue("@companyName", this.companyName);
            cmd.Parameters.AddWithValue("@taxId", this.taxId);
            cmd.Parameters.AddWithValue("@accountManager", this.accountManager);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteClient()
        {
            Program.Clients.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Clients_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@client_id", this.clientId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextClientId()
        {
            if (Program.Clients == null || Program.Clients.Count == 0)
                return 1;
            return Program.Clients[Program.Clients.Count - 1].getClientId() + 1;
        }

        public static void initClients()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_Clients_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Clients = new List<Client>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);
                string companyName = rdr.GetString(2);
                string taxId = rdr.GetString(3);
                int accountManager = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4);

                User parentUser = seekUserForSubtype(userId);
                if (parentUser != null)
                {
                    Client c = new Client(
                        parentUser.getUserId(),
                        parentUser.getEmail(),
                        parentUser.getPasswordHash(),
                        parentUser.getRole(),
                        clientId,
                        companyName,
                        taxId,
                        accountManager,
                        false
                    );
                    Program.Clients.Add(c);
                }
            }
            rdr.Close();
        }

        public static Client seekClient(int id)
        {
            foreach (Client c in Program.Clients)
            {
                if (c.getClientId() == id)
                    return c;
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

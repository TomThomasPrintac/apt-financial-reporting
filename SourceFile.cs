using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    public class SourceFile
    {
        private int fileId;
        private int caseId;
        private string fileName;
        private string fileType;
        private DateTime uploadDate;
        private string status;
        private string dataFormat;

        public SourceFile(int fileId, int caseId, string fileName, string fileType,
                         DateTime uploadDate, string status, string dataFormat, bool is_new)
        {
            this.fileId = fileId;
            this.caseId = caseId;
            this.fileName = fileName;
            this.fileType = fileType;
            this.uploadDate = uploadDate;
            this.status = status;
            this.dataFormat = dataFormat;
            if (is_new)
            {
                createSourceFile();
                Program.SourceFiles.Add(this);
            }
        }

        public int getFileId() { return this.fileId; }
        public int getCaseId() { return this.caseId; }
        public string getFileName() { return this.fileName; }
        public string getFileType() { return this.fileType; }
        public DateTime getUploadDate() { return this.uploadDate; }
        public string getStatus() { return this.status; }
        public string getDataFormat() { return this.dataFormat; }

        public void setStatus(string status) { this.status = status; }

        public void createSourceFile()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SourceFiles_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@file_id", this.fileId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@fileName", this.fileName);
            cmd.Parameters.AddWithValue("@fileType", this.fileType);
            cmd.Parameters.AddWithValue("@uploadDate", this.uploadDate);
            cmd.Parameters.AddWithValue("@status", this.status);
            cmd.Parameters.AddWithValue("@dataFormat", this.dataFormat);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateSourceFile()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SourceFiles_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@file_id", this.fileId);
            cmd.Parameters.AddWithValue("@case_id", this.caseId);
            cmd.Parameters.AddWithValue("@fileName", this.fileName);
            cmd.Parameters.AddWithValue("@fileType", this.fileType);
            cmd.Parameters.AddWithValue("@uploadDate", this.uploadDate);
            cmd.Parameters.AddWithValue("@status", this.status);
            cmd.Parameters.AddWithValue("@dataFormat", this.dataFormat);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteSourceFile()
        {
            Program.SourceFiles.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SourceFiles_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@file_id", this.fileId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextFileId()
        {
            if (Program.SourceFiles == null || Program.SourceFiles.Count == 0)
                return 1;
            // max(id) + 1 — כל קובץ חדש מתווסף לצד הקיימים בלי התנגשות מזהים,
            // גם אחרי מחיקות (לא מסתמכים על האיבר האחרון ברשימה)
            int max = 0;
            foreach (SourceFile sf in Program.SourceFiles)
                if (sf.getFileId() > max) max = sf.getFileId();
            return max + 1;
        }

        public static void initSourceFiles()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_SourceFiles_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.SourceFiles = new List<SourceFile>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int fileId = rdr.GetInt32(0);
                int caseId = rdr.GetInt32(1);
                string fileName = rdr.GetString(2);
                string fileType = rdr.GetString(3);
                DateTime uploadDate = rdr.GetDateTime(4);
                string status = rdr.GetString(5);
                string dataFormat = rdr.GetString(6);

                SourceFile sf = new SourceFile(fileId, caseId, fileName, fileType,
                                               uploadDate, status, dataFormat, false);
                Program.SourceFiles.Add(sf);
            }
            rdr.Close();
        }

        public static SourceFile seekSourceFile(int id)
        {
            foreach (SourceFile sf in Program.SourceFiles)
            {
                if (sf.getFileId() == id)
                    return sf;
            }
            return null;
        }
    }
}

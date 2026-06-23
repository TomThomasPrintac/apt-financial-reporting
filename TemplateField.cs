using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace APT
{
    /// <summary>
    /// שדה בודד בתוך תבנית דו"ח (UC-06) — שייך ל-ReportTemplate אחד.
    /// מייצג שדה/עמודה במבנה של סוג קובץ מקור (שכר / מאזן בוחן / פחת / כרטסת).
    /// </summary>
    public class TemplateField
    {
        private int fieldId;
        private int templateId;
        private string fieldName;

        public TemplateField(int fieldId, int templateId, string fieldName, bool is_new)
        {
            this.fieldId = fieldId;
            this.templateId = templateId;
            this.fieldName = fieldName;
            if (is_new)
            {
                createTemplateField();
                Program.TemplateFields.Add(this);
            }
        }

        public int getFieldId() { return this.fieldId; }
        public int getTemplateId() { return this.templateId; }
        public string getFieldName() { return this.fieldName; }

        public void setTemplateId(int templateId) { this.templateId = templateId; }
        public void setFieldName(string fieldName) { this.fieldName = fieldName; }

        public void createTemplateField()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_TemplateFields_create";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@field_id", this.fieldId);
            cmd.Parameters.AddWithValue("@template_id", this.templateId);
            cmd.Parameters.AddWithValue("@fieldName", this.fieldName);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateTemplateField()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_TemplateFields_update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@field_id", this.fieldId);
            cmd.Parameters.AddWithValue("@template_id", this.templateId);
            cmd.Parameters.AddWithValue("@fieldName", this.fieldName);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteTemplateField()
        {
            Program.TemplateFields.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_TemplateFields_delete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@field_id", this.fieldId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static int getNextFieldId()
        {
            if (Program.TemplateFields == null || Program.TemplateFields.Count == 0)
                return 1;
            return Program.TemplateFields[Program.TemplateFields.Count - 1].getFieldId() + 1;
        }

        public static void initTemplateFields()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_TemplateFields_get_all";
            cmd.CommandType = CommandType.StoredProcedure;
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.TemplateFields = new List<TemplateField>();

            // הגנה: אם החיבור/הפרוצדורה נכשלו, execute_query מחזיר null — לא מתרסקים, נשארים עם רשימה ריקה
            if (rdr == null) return;

            while (rdr.Read())
            {
                int fieldId = rdr.GetInt32(0);
                int templateId = rdr.GetInt32(1);
                string fieldName = rdr.GetString(2);

                TemplateField f = new TemplateField(fieldId, templateId, fieldName, false);
                Program.TemplateFields.Add(f);
            }
            rdr.Close();
        }

        public static TemplateField seekTemplateField(int id)
        {
            foreach (TemplateField f in Program.TemplateFields)
            {
                if (f.getFieldId() == id)
                    return f;
            }
            return null;
        }
    }
}

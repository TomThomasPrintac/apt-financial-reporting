using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Configuration;

namespace APT
{
    /// <summary>
    /// מחלקה שאחראית על החיבור לבסיס הנתונים וביצוע שאילתות.
    /// כל פעולה מול בסיס הנתונים עוברת דרך מחלקה זו.
    ///
    /// שתי סוגי פעולות:
    /// 1. execute_non_query - פעולות שמשנות נתונים (INSERT, UPDATE, DELETE)
    /// 2. execute_query    - פעולות שמחזירות נתונים (SELECT)
    /// </summary>
    class SQL_CON
    {
        SqlConnection conn;

        public SQL_CON()
        {
            // קריאת connection string מ-app.config
            string connectionString = ConfigurationManager.ConnectionStrings["APT.Properties.Settings.aptConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);
        }

        /// <summary>
        /// ביצוע פעולה שמשנה נתונים בבסיס הנתונים (INSERT, UPDATE, DELETE).
        /// הפעולה לא מחזירה נתונים - רק מבצעת שינוי.
        ///
        /// הזרימה:
        /// 1. פתיחת חיבור לבסיס הנתונים
        /// 2. קישור הפקודה לחיבור
        /// 3. ביצוע הפקודה
        /// 4. סגירת החיבור (תמיד! גם אם הייתה שגיאה)
        /// </summary>
        /// <param name="cmd">פקודת SQL מוכנה עם פרמטרים</param>
        public void execute_non_query(SqlCommand cmd)
        {
            try
            {
                conn.Open();              // שלב 1: פתיחת חיבור
                cmd.Connection = conn;    // שלב 2: קישור הפקודה לחיבור
                cmd.ExecuteNonQuery();     // שלב 3: ביצוע (INSERT/UPDATE/DELETE)
                // הודעת ההצלחה מוצגת ברמת הפאנל (ספציפית לפעולה) — לא בשכבת הנתונים, כדי למנוע הודעה כפולה
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בביצוע הפעולה: " + ex.Message, "שגיאה", MessageBoxButtons.OK);
            }
            finally
            {
                // שלב 4: סגירת החיבור - חייבת לקרות תמיד!
                // finally מתבצע גם אם הייתה שגיאה וגם אם לא
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// ביצוע שאילתה שמחזירה נתונים מבסיס הנתונים (SELECT).
        /// מחזירה SqlDataReader - אובייקט שמאפשר לקרוא את התוצאות שורה אחרי שורה.
        ///
        /// הזרימה:
        /// 1. פתיחת חיבור לבסיס הנתונים
        /// 2. קישור הפקודה לחיבור
        /// 3. ביצוע השאילתה וקבלת Reader
        /// 4. החזרת ה-Reader לשימוש הקוד הקורא
        ///
        /// שימו לב: החיבור לא נסגר כאן! הוא נשאר פתוח כי ה-Reader צריך אותו
        /// כדי לקרוא את השורות. החיבור ייסגר כשה-Reader יסיים.
        /// </summary>
        /// <param name="cmd">פקודת SQL מוכנה עם פרמטרים</param>
        /// <returns>SqlDataReader לקריאת התוצאות, או null אם הייתה שגיאה</returns>
        public SqlDataReader execute_query(SqlCommand cmd)
        {
            try
            {
                conn.Open();              // שלב 1: פתיחת חיבור
                cmd.Connection = conn;    // שלב 2: קישור הפקודה לחיבור
                SqlDataReader reader = cmd.ExecuteReader();  // שלב 3: ביצוע SELECT
                return reader;            // שלב 4: החזרת התוצאות
            }
            catch (Exception ex)
            {
                // לא מציגים MessageBox חוסם בשכבת הנתונים (במיוחד בזמן הטעינה הראשונית מ-initLists).
                // רושמים את השגיאה ל-error.log ומחזירים null; הקוד הקורא אחראי לבדוק null.
                logError(ex);
                return null;
            }
        }

        /// <summary>
        /// רישום שגיאה לקובץ error.log בלי להפיל את האפליקציה.
        /// </summary>
        private static void logError(Exception ex)
        {
            try
            {
                string line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + ex.Message + Environment.NewLine;
                System.IO.File.AppendAllText("error.log", line);
            }
            catch { /* לוגינג לעולם לא יפיל את האפליקציה */ }
        }
    }
}

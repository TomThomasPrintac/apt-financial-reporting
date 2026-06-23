using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace APT
{
    /// <summary>
    /// מרכוז תוכן המסכים.
    /// שומר את גוש התוכן של הפאנל ממורכז (אופקית ואנכית) בתוך השטח הזמין —
    /// גם כשהחלון משתנה גודל או ממוקסם — בלי לשנות את הפריסה היחסית בין הפקדים.
    ///
    /// סרגל ניווט עליון (פאנל דק ברוחב מלא בראש המסך) מזוהה אוטומטית, נמתח לרוחב
    /// מלא ונשאר למעלה; הוא והפקדים שחופפים אותו (כפתורי ניווט) אינם ממורכזים.
    ///
    /// שימוש: בבנאי של כל פאנל, מיד אחרי InitializeComponent():
    ///     UiCenter.Enable(this);
    /// </summary>
    public static class UiCenter
    {
        // שוליים מינימליים שנשמרים מקצוות המסך כשהתוכן ממורכז
        private const int Margin = 12;

        public static void Enable(UserControl panel)
        {
            if (panel == null) return;

            // סרגל עליון רוחב-מלא (אם קיים) — נמתח ונשאר למעלה, לא משתתף במרכוז
            Panel topBar = DetectTopBar(panel);
            int barBottom = (topBar != null) ? topBar.Bottom : 0;

            // הפקדים שאותם ממרכזים: ילדים ישירים, לא הסרגל, לא מודבקים (Dock).
            // פקדי ניווט (למשל כפתור "חזור" כפול) מעוגנים לימין/לתחתית — אותם משאירים
            // צמודים לקצה ולא ממרכזים; תוכן רגיל מעוגן Top|Left ולכן נכלל.
            // שומרים את המיקום המקורי (מהמעצב) בלבד; את הגודל קוראים חי בכל פריסה,
            // כדי לתפוס תוויות AutoSize שהטקסט שלהן נקבע אחרי Enable.
            var origin = new Dictionary<Control, Point>();
            foreach (Control c in panel.Controls)
            {
                if (c == topBar) continue;
                if (c.Dock != DockStyle.None) continue;
                if ((c.Anchor & (AnchorStyles.Right | AnchorStyles.Bottom)) != 0) continue;
                origin[c] = c.Location;
                // מנטרלים עיגון אוטומטי כדי שלא יתנגש במרכוז הידני
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }

            if (origin.Count == 0)
            {
                // אין תוכן למרכז — לפחות נמתח את הסרגל העליון
                EventHandler stretchOnly = (s, e) => StretchTopBar(panel, topBar);
                panel.SizeChanged += stretchOnly;
                panel.HandleCreated += stretchOnly;
                StretchTopBar(panel, topBar);
                return;
            }

            EventHandler relayout = (s, e) =>
            {
                StretchTopBar(panel, topBar);

                // תיבת הגבול מחושבת מחדש בכל פריסה: מיקום מקורי + גודל חי של כל פקד,
                // כך שתוויות AutoSize שהתרחבו אחרי קביעת הטקסט נספרות נכון.
                int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
                foreach (KeyValuePair<Control, Point> kv in origin)
                {
                    Point p = kv.Value;
                    Size sz = kv.Key.Size;
                    if (p.X < minX) minX = p.X;
                    if (p.Y < minY) minY = p.Y;
                    if (p.X + sz.Width > maxX) maxX = p.X + sz.Width;
                    if (p.Y + sz.Height > maxY) maxY = p.Y + sz.Height;
                }
                int contentW = maxX - minX;
                int contentH = maxY - minY;

                int clientW = panel.ClientSize.Width;
                int clientH = panel.ClientSize.Height;

                // היסט אופקי: ממרכז את הגוש; אם רחב מהמסך — מצמיד לשוליים
                int desiredX = (clientW - contentW) / 2;
                if (desiredX < Margin) desiredX = Margin;
                int offsetX = desiredX - minX;

                // היסט אנכי: ממרכז בשטח שמתחת לסרגל, בלי לדחוף את התוכן אל תוך הסרגל
                int availH = clientH - barBottom;
                int desiredY = barBottom + (availH - contentH) / 2;
                if (desiredY < barBottom + Margin) desiredY = barBottom + Margin;
                int offsetY = desiredY - minY;

                panel.SuspendLayout();
                foreach (KeyValuePair<Control, Point> kv in origin)
                    kv.Key.Location = new Point(kv.Value.X + offsetX, kv.Value.Y + offsetY);
                panel.ResumeLayout(true);
            };

            panel.SizeChanged += relayout;
            // הרצה כשהפאנל מקבל את גודלו האמיתי (אחרי Dock=Fill בתוך הטופס)
            panel.HandleCreated += relayout;
            relayout(panel, EventArgs.Empty);
        }

        // מותח את הסרגל העליון לרוחב מלא ומקבע אותו לראש המסך
        private static void StretchTopBar(UserControl panel, Panel topBar)
        {
            if (topBar == null) return;
            topBar.Left = 0;
            topBar.Top = 0;
            topBar.Width = panel.ClientSize.Width;
        }

        // זיהוי סרגל עליון: פאנל ילד דק (גובה עד 80), צמוד לראש, ברוחב 80%+ מהמסך
        private static Panel DetectTopBar(UserControl panel)
        {
            foreach (Control c in panel.Controls)
            {
                if (c is Panel p && p.Top <= 2 && p.Height <= 80 && p.Width >= panel.Width * 0.8)
                    return p;
            }
            return null;
        }
    }
}

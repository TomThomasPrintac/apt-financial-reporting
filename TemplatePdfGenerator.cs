using System;
using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace APT
{
    /// <summary>
    /// UC-06 — מייצר PDF ויזואלי של תבנית ריקה: שם התבנית, סוג הקובץ, מועד עדכון,
    /// ורשימת השדות כטבלה ריקה (עמודת "שדה" מול עמודת "ערך" ריקה).
    /// משתמש ב-QuestPDF (SkiaSharp) שמטפל נכון בעברית RTL.
    /// </summary>
    public static class TemplatePdfGenerator
    {
        public static void Generate(ReportTemplate template, List<TemplateField> fields, string path)
        {
            // איתור קובץ הלוגו (logo.png) — בתיקיית הריצה או בתיקיית העבודה
            byte[] logoBytes = null;
            foreach (string p in new[] {
                System.IO.Path.Combine(System.AppContext.BaseDirectory, "logo.png"),
                System.IO.Path.Combine(System.Environment.CurrentDirectory, "logo.png") })
            {
                if (System.IO.File.Exists(p)) { logoBytes = System.IO.File.ReadAllBytes(p); break; }
            }

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(12).DirectionFromRightToLeft());

                    page.Header().Column(col =>
                    {
                        if (logoBytes != null)
                            col.Item().PaddingBottom(8).AlignCenter().Width(140).Image(logoBytes);
                        col.Item().Text(template.getTemplateName()).FontSize(22).Bold().FontColor(Colors.Blue.Darken2);
                        col.Item().Text("סוג דוח: " + template.getReportType());
                        col.Item().Text("תבנית ריקה").FontColor(Colors.Grey.Darken1);
                        col.Item().Text("עודכן לאחרונה: " + template.getLastUpdated().ToString("dd/MM/yyyy HH:mm"))
                                  .FontSize(10).FontColor(Colors.Grey.Medium);
                    });

                    page.Content().PaddingVertical(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Grey.Lighten2).Border(1).BorderColor(Colors.Grey.Medium).Padding(6).Text("שדה").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Border(1).BorderColor(Colors.Grey.Medium).Padding(6).Text("ערך").Bold();
                        });

                        foreach (TemplateField f in fields)
                        {
                            table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).Padding(6).MinHeight(26).Text(f.getFieldName());
                            table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).Padding(6).MinHeight(26).Text(" ");
                        }
                    });

                    page.Footer().AlignCenter()
                        .Text("APT — נוצר אוטומטית " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            })
            .GeneratePdf(path);
        }
    }
}

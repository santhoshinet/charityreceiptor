using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;

namespace pdf_test_app
{
    internal class Program
    {
        private static void Main()
        {
            const string path = @"C:\Users\Santhosh\AppData\test.pdf";
            if (File.Exists(path))
                File.Delete(path);
            var doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            // Metadata
            doc.AddCreator("");
            doc.AddTitle("General Receipt");
            // Add content
            doc.Open();

            ReceiptId(doc, "3495o8dskjfhsdjfh");

            NewLine(doc);

            var baseColor = new BaseColor(Color.Gray);
            iTextSharp.text.Font arial = FontFactory.GetFont("Arial", 10f, baseColor);

            for (int i = 0; i < 78; i++)
                doc.Add(new Anchor("A", arial));

            Header(doc, "Regular Receipt");

            NewLine(doc);

            TwoField(doc, "field1", "value1", "field2", "value2");
            NewLine(doc);
            TwoField(doc, "field1", "value1", "field2", "value2");
            NewLine(doc);

            /*doc.Add(new Anchor("some text"));
            doc.Add(new Anchor("some text") { Leading = 100, Capacity = 100 });
            doc.Add(new Paragraph("Hello World") { FirstLineIndent = 225 });
            doc.Add(new Paragraph("Hello World") { FirstLineIndent = 225 }); */
            doc.Close();
        }

        private static void TwoField(Document document, string field1, string value1, string field2, string value2)
        {
            var baseColor = new BaseColor(Color.Gray);
            iTextSharp.text.Font arial = FontFactory.GetFont("Arial", 9f, baseColor);
            document.Add(new Anchor(field1, arial));
            arial = FontFactory.GetFont("Arial", 9f, baseColor);
            document.Add(new Anchor(value1, arial));

            //spaces
            for (int i = 0; i < 50; i++)
                document.Add(new Anchor(" ", arial));

            arial = FontFactory.GetFont("Arial", 9f, baseColor);
            document.Add(new Anchor(field2, arial));
            arial = FontFactory.GetFont("Arial", 9f, baseColor);
            document.Add(new Anchor(value2, arial));
        }

        private static void NewLine(Document document)
        {
            document.Add(new Phrase("\n"));
        }

        private static void Header(Document document, string title)
        {
            var baseColor = new BaseColor(Color.Black);
            iTextSharp.text.Font arial = FontFactory.GetFont("Arial", 11f, baseColor);
            document.Add(new Paragraph(title, arial) { IndentationLeft = 220 });
        }

        private static void ReceiptId(Document document, string receiptId)
        {
            var arial = new Font(Font.FontFamily.TIMES_ROMAN, 10f, Font.BOLD, new BaseColor(163, 21, 21)); //FontFactory.GetFont("Arial", 10f, baseColor);
            document.Add(new Anchor("Receipt #:", arial));
            document.Add(new Anchor("  " + receiptId, arial));
        }
    }
}
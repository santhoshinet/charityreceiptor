using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

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

            PdfContentByte cb = writer.DirectContent;

            cb.SetLineWidth(0.1f);
            cb.Rectangle(50f, 300f, 500f, 70f);
            cb.Stroke();

            doc.Close();
        }
    }
}
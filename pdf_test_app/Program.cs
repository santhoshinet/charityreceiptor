using System.IO;
using iTextSharp.text;

namespace pdf_test_app
{
    internal class Program
    {
        private static void Main()
        {
            string path = @"C:\Users\Santhosh\AppData\test.pdf";
            if (File.Exists(path))
                File.Delete(path);
            var doc = new Document();
            //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            // Metadata
            doc.AddCreator("");
            doc.AddTitle("General Receipt");
            // Add content
            doc.Open();

            doc.Add(new Paragraph("Hello World") { FirstLineIndent = 225 });
            doc.Add(new Paragraph("Hello World") { FirstLineIndent = 225 });
            doc.Close();
        }
    }
}
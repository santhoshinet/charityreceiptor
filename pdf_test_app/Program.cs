using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace pdf_test_app
{
    internal class Program
    {
        private static void Main()
        {
            const string newFile = @"C:\Users\Santhosh\AppData\test.pdf";
            var doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(newFile, FileMode.Create));
            // Metadata
            doc.AddCreator("PDF Printer by Joachim Tesznar");
            doc.AddTitle("New PDF Document");
            // Add content
            doc.Open();
            doc.Add(new Paragraph("Hello World"));
            doc.Close();
        }
    }
}
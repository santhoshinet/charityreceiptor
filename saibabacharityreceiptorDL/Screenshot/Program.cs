using System;
using System.Drawing;
using System.Windows.Forms;

namespace Screenshot
{
    class Program
    {
        private static WebBrowser _wb;

        [STAThread]
        static void Main(string[] args)
        {
            _wb = new WebBrowser { ScrollBarsEnabled = false, ScriptErrorsSuppressed = true };
            _wb.Navigate("http://www.shirdisaibabaaz.org/Reports/RegularReceipts");
            while (_wb.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            System.Threading.Thread.Sleep(1000);
            if (_wb.Document != null)
            {
                if (_wb.Document.Body != null)
                {
                    int width = _wb.Document.Body.ScrollRectangle.Width;
                    int height = _wb.Document.Body.ScrollRectangle.Height;
                    _wb.Width = width;
                    _wb.Height = height;
                    var bmp = new Bitmap(width, height);
                    _wb.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                    bmp.Save(@"C:\Users\Santhosh\Desktop\test.bmp");
                }
            }
        }
    }
}
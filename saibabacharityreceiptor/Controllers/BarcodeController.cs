using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using BarcodeGenerator;

namespace saibabacharityreceiptor.Controllers
{
    public class BarcodeController : Controller
    {
        //
        // GET: /Barcode/
        public FileStreamResult Index(string recpId)
        {
            try
            {
                const string fontString = "Arial";
                const int thickness = 30;
                const string code = "Code 128";
                const int scale = 1;

                var font = new BCGFont(new Font(fontString, 1));
                var colorBlack = new BCGColor(Color.Black);
                var colorWhite = new BCGColor(Color.White);
                Type codeType = (from kvp in Utilities.CodeType where kvp.Value == code select kvp.Key).FirstOrDefault();
                var temporaryBarcode = (BarCode.Barcode)Activator.CreateInstance(codeType);
                var codebar = (BCGBarcode1D)Activator.CreateInstance(temporaryBarcode.Code);
                codebar.setThickness(thickness);
                codebar.setScale(scale);
                MethodInfo method = temporaryBarcode.Code.GetMethod("setStart");
                if (method != null)
                {
                    method.Invoke(codebar, new object[] { "A" });
                }
                codebar.setBackgroundColor(colorWhite);
                codebar.setForegroundColor(colorBlack);
                codebar.setFont(font);
                codebar.parse(Utilities.Encrypt(recpId));
                var drawing = new BCGDrawing(colorWhite);
                drawing.setBarcode(codebar);
                drawing.draw();
                Response.ContentType = "image/jpeg";
                drawing.finish(ImageFormat.Jpeg, Response.OutputStream);
            }
            catch (Exception exception)
            {
                Response.ContentType = "image/png";
                /*
                                using (var fs = new FileStream("error.png", FileMode.Open))
                                {
                                    using (var br = new BinaryReader(fs))
                                    {
                                        var buffer = new byte[fs.Length];
                                        br.Read(buffer, 0, (int)fs.Length);

                                        using (var ms = new MemoryStream(buffer))
                                        {
                                            ms.WriteTo(Response.OutputStream);
                                        }
                                    }
                                }*/
                // Here, check for the exception.
                System.Diagnostics.Debug.Write(exception);
            }
            return null;
        }
    }
}
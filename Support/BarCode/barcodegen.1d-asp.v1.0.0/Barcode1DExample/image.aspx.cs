/**
 *--------------------------------------------------------------------
 *
 * File to create barcode used by Default.aspx
 *
 *--------------------------------------------------------------------
 * Revision History
 * v1.0.0	12 apr	2009	Jean-Sébastien Goupil	New version
 *--------------------------------------------------------------------
 * $Id: image.aspx.cs,v 1.1 2009/05/03 20:44:24 jsgoupil Exp $
 *--------------------------------------------------------------------
 * Copyright (C) Jean-Sebastien Goupil
 * http://www.barcodeasp.com
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using BarcodeGenerator;

namespace Barcode
{
    // ReSharper disable InconsistentNaming
    public partial class image : System.Web.UI.Page
    // ReSharper restore InconsistentNaming
    {
        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            try
            {
                string fontString = Request.QueryString["f1"];
                BCGFont font = null;
                if (!string.IsNullOrEmpty(fontString))
                {
                    float fontSize;
                    float.TryParse(Request.QueryString["f2"], out fontSize);
                    font = new BCGFont(new Font(Request.QueryString["f1"], fontSize));
                }

                var colorBlack = new BCGColor(Color.Black);
                var colorWhite = new BCGColor(Color.White);

                string code = Request.QueryString["code"];
                Type codeType = (from kvp in Utility.CodeType where kvp.Value == code select kvp.Key).FirstOrDefault();

                if (codeType == null)
                {
                    throw new Exception("You need to specify a type of barcode");
                }

                var temporaryBarcode = (code.Barcode)Activator.CreateInstance(codeType);
                var codebar = (BCGBarcode1D)Activator.CreateInstance(temporaryBarcode.Code);

                int thickness;
                int.TryParse(Request.QueryString["t"], out thickness);
                codebar.setThickness(thickness);

                int scale;
                int.TryParse(Request.QueryString["r"], out scale);
                codebar.setScale(scale);

                // Special
                int a1;
                if (int.TryParse(Request.QueryString["a1"], out a1) && a1 == 1)
                {
                    MethodInfo method = temporaryBarcode.Code.GetMethod("setChecksum", new[] { typeof(bool) });
                    if (method != null)
                    {
                        method.Invoke(codebar, new object[] { true });
                    }
                    method = temporaryBarcode.Code.GetMethod("setChecksum", new[] { typeof(int) });
                    if (method != null)
                    {
                        method.Invoke(codebar, new object[] { 1 });
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["a2"]))
                {
                    MethodInfo method = temporaryBarcode.Code.GetMethod("setStart");
                    if (method != null)
                    {
                        method.Invoke(codebar, new object[] { Request.QueryString["a2"] });
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["a3"]))
                {
                    MethodInfo method = temporaryBarcode.Code.GetMethod("setLabel");
                    if (method != null)
                    {
                        method.Invoke(codebar, new object[] { Request.QueryString["a3"] });
                    }
                }

                codebar.setBackgroundColor(colorWhite);
                codebar.setForegroundColor(colorBlack);
                codebar.setFont(font);
                codebar.parse(Request.QueryString["text"]);

                var drawing = new BCGDrawing(colorWhite);
                drawing.setBarcode(codebar);
                drawing.draw();

                // Draw (or save) the image into the right format.
                string format = Request.QueryString["o"];
                ImageFormat imageFormat = null;
                switch (format)
                {
                    case "1":
                        Response.ContentType = "image/bmp";
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case "2":
                        Response.ContentType = "image/png";
                        imageFormat = ImageFormat.Png;
                        break;
                    case "3":
                        Response.ContentType = "image/jpeg";
                        imageFormat = ImageFormat.Jpeg;
                        break;
                    case "4":
                        Response.ContentType = "image/gif";
                        imageFormat = ImageFormat.Gif;
                        break;
                }

                if (imageFormat != null)
                {
                    drawing.finish(imageFormat, Response.OutputStream);
                }
            }
            catch (Exception exception)
            {
                Response.ContentType = "image/png";

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
                }

                // Here, check for the exception.
                System.Diagnostics.Debug.Write(exception);
            }
        }
    }
}
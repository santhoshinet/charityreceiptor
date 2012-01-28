/**
 *--------------------------------------------------------------------
 *
 * Page to create barcodes with C#
 *
 *--------------------------------------------------------------------
 * Revision History
 * v1.0.0	12 apr	2009	Jean-Sébastien Goupil	New version
 *--------------------------------------------------------------------
 * $Id: TestCode.aspx.cs,v 1.1 2009/05/03 20:44:23 jsgoupil Exp $
 *--------------------------------------------------------------------
 * Copyright (C) Jean-Sebastien Goupil
 * http://www.barcodeasp.com
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using BarcodeGenerator;

namespace Barcode
{
    public partial class TestCode : System.Web.UI.Page
    {
        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            var font = new BCGFont(new Font("Arial", 11, FontStyle.Regular));
            var colorBlack = new BCGColor(0, 0, 0);
            var colorWhite = new BCGColor(255, 255, 255);

            BCGBarcode1D code = new BCGcode39();
            code.setScale(2); // Resolution
            code.setThickness(30); // Thickness
            code.setForegroundColor(colorBlack); // Color of bars
            code.setBackgroundColor(colorWhite); // Color of spaces
            code.setFont(font); // Font
            code.parse("HELLO"); // Text

            /* Here is the list of the arguments
            1 - Filename (empty : display on screen)
            2 - Background color */
            var drawing = new BCGDrawing(colorWhite);
            drawing.setBarcode(code);
            drawing.draw();

            // Draw (or save) the image into PNG format.
            drawing.finish(ImageFormat.Png, this.Context.Response.OutputStream);
        }
    }
}
using System;
using Barcode;

namespace saibabacharityreceiptor.BarCode
{
    public class Upcext2 : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGupcext2);
            }
        }

        public override string Description
        {
            get
            {
                return "";
            }
        }

        public override string Title
        {
            get
            {
                return "UPC Extension 2 Digits";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Extension for UPC-A, UPC-E, EAN-13 and EAN-8.</li><li>Used for encode additional information for newspaper, books...</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utilities.KeyCodeNumber.ToArray();
            }
        }
    }
}
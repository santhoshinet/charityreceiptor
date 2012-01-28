using System;

namespace Barcode.code
{
    public class Upce : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGupce);
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
                return "UPC-E";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Short version of UPC symbol, 8 characters.</li><li>It is a conversion of an UPC-A for small package.</li><li>You can provide directly an UPC-A (11 chars) or UPC-E (6 chars) code.</li><li>UPC-E contain a system number and a check digit.</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utility.KeyCodeNumber.ToArray();
            }
        }
    }
}
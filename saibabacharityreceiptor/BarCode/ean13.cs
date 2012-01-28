using System;
using Barcode;

namespace saibabacharityreceiptor.BarCode
{
    public class Ean13 : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGean13);
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
                return "EAN-13";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>EAN means Internal Article Numbering.</li><li>It is an extension of UPC-A to include the country information.</li><li>Used with consumer products internationally.</li><li>Composed by 2 number system, 5 manufacturer code, 5 product code and 1 check digit.</li></ul>";
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
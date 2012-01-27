using System;
using Barcode;

namespace saibabacharityreceiptor.BarCode
{
    public class Ean8 : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGean8);
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
                return "EAN-8";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>EAN-8 is a short version of EAN-13.</li><li>Composed by 7 digits and 1 check digit.</li><li>There is no conversion available between EAN-8 and EAN-13.</li></ul>";
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
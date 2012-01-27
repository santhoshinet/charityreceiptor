using System;
using Barcode;

namespace saibabacharityreceiptor.BarCode
{
    public class Code93 : saibabacharityreceiptor.BarCode.Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGcode93);
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
                return "Code 93";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Known also as USS Code 93.</li><li>Code 93 was designed to provide a higher density and data security enhancement to Code39.</li><li>Used primarily by Canadian postal office to encode supplementary delivery information.</li><li>Similar to Code 39, Code 93 has the same 43 characters plus 5 special ones to encode the ASCII 0 to 127.</li><li>This symbology composed of 2 check digits (\"C\" and \"K\").</li><li>Your browser may not be able to write the special characters (NUL, SOH, etc.) but you can write them with the code.</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utilities.KeyCodeFull.ToArray();
            }
        }

        public override int SizeKeys
        {
            get
            {
                return 37;
            }
        }
    }
}
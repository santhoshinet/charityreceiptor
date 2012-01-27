using System;

namespace Barcode.code
{
    public class Code11 : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGcode11);
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
                return "Code 11";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Known also as USD-8.</li><li>Code 11 was developed in 1977 as a high-density numeric symbology.</li><li>Used to identify telecommunications equipment.</li><li>Code 11 is a numeric symbology and its character set consists of 10 digital characters and the dash symbol (-).</li><li>There is a \"C\" check digit. If the length of encoded message is greater thant 10, a \"K\" check digit may be used.</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utility.MergeKeyCode(Utility.KeyCodeNumber, new KeyCode('-'));
            }
        }
    }
}
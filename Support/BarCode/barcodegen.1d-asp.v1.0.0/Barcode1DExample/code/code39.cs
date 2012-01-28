using System;
using System.Web.UI.HtmlControls;

namespace Barcode.code
{
    public class Code39 : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGcode39);
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
                return "Code 39";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Known also as USS Code 39, 3 of 9.</li><li>Code 39 can encode alphanumeric characters.</li><li>The symbology is used in non-retail environment.</li><li>Code 39 is designed to encode 26 upper case letters, 10 digits and 7 special characters.</li><li>Code 39 has a checksum but it's rarely used.</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utility.MergeKeyCode(Utility.KeyCodeNumber, Utility.KeyCodeLettersCapital, new KeyCode('-'), new KeyCode('.'), new KeyCode(' '), new KeyCode('$'), new KeyCode('/'), new KeyCode('+'), new KeyCode('%'));
            }
        }

        public override string SpecificText
        {
            get
            {
                return "Checksum";
            }
        }

        public override HtmlControl SpecificValue
        {
            get
            {
                HtmlInputCheckBox checkBox = new HtmlInputCheckBox();
                checkBox.Name = "a1";
                checkBox.Value = "1";

                return checkBox;
            }
        }
    }
}
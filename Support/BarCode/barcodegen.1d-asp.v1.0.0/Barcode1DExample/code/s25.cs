using System;
using System.Web.UI.HtmlControls;

namespace Barcode.code
{
    public class S25 : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGs25);
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
                return "Standard 2 of 5";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Known also as Industrial 2 of 5.</li><li>Standard 2 of 5 is a low-density numeric symbology that has been with us since the 1960s.</li><li>There is an optional checksum.</li><li>Note: Standard 2 of 5 is REALLY tough to read !</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utility.KeyCodeNumber.ToArray();
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
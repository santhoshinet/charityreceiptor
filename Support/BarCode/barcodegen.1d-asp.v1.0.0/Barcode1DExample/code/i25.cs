using System;
using System.Web.UI.HtmlControls;

namespace Barcode.code
{
    public class I25 : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGi25);
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
                return "Interleaved 2 of 5";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Interleaved 2 of 5 is based on Standard 2 of 5 symbology.</li><li>There is an optional checksum.</li></ul>";
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
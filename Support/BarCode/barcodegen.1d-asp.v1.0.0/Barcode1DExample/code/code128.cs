using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Barcode.code
{
    public class Code128 : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGcode128);
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
                return "Code 128";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Code 128 is a high-density alphanumeric symbology.</li><li>Used extensively worldwide.</li><li>Code 128 is designed to encode 128 full ASCII characters.</li><li>The symbology includes a checksum digit.</li><li>Code 128A handles capital letters<br />Code 128B handles capital letters and lowercase<br />Code 128C handles group of 2 numbers</li><li>Your browser may not be able to write the special characters (NUL, SOH, etc.) but you can write them with the code.</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utility.KeyCodeFull.ToArray();
            }
        }

        public override int SizeKeys
        {
            get
            {
                return 37;
            }
        }

        public override string SpecificText
        {
            get
            {
                return "Start";
            }
        }

        public override HtmlControl SpecificValue
        {
            get
            {
                HtmlSelect selectBox = new HtmlSelect();
                selectBox.Name = "a2";
                selectBox.Size = 1;
                selectBox.Items.Add(new ListItem("Code 128-A", "A", true));
                selectBox.Items.Add(new ListItem("Code 128-B", "B", true));
                selectBox.Items.Add(new ListItem("Code 128-C", "C", true));

                return selectBox;
            }
        }
    }
}
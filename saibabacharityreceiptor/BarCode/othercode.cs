using System;
using System.Web.UI.HtmlControls;
using Barcode;

namespace saibabacharityreceiptor.BarCode
{
    public class Othercode : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGothercode);
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
                return "Other Barcode";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Enter width of each bars with one characters. Begin by a bar.</li><li>10523 : Will do 2px bar, 1px space, 6px bar, 3px space, 4px bar.</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return null;
            }
        }

        public override string SpecificText
        {
            get
            {
                return "Text Label";
            }
        }

        public override HtmlControl SpecificValue
        {
            get
            {
                var textBox = new HtmlInputText { Name = "a3" };
                return textBox;
            }
        }
    }
}
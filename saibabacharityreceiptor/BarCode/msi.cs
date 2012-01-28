using System;
using System.Web.UI.HtmlControls;
using Barcode;

namespace saibabacharityreceiptor.BarCode
{
    public class Msi : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGmsi);
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
                return "MSI Plessey";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Developed by the MSI Data Corporation.</li><li>Used primarily to mark retail shelves for inventory control.</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utilities.KeyCodeNumber.ToArray();
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
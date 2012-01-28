using System;

namespace Barcode.code
{
    public class Postnet : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGpostnet);
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
                return "Postnet";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Used to encode enveloppe in USA.</li><li>You can provide<br />5 digits (ZIP Code)<br />9 digits (ZIP+4 code)<br />11 digits (ZIP+4 code+2 digits)<br />(Those 2 digits are taken from your address. If your address is 6453, the code will be 53.)</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utility.KeyCodeNumber.ToArray();
            }
        }
    }
}
using System;
using Barcode;

namespace saibabacharityreceiptor.BarCode
{
    public class Upca : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGupca);
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
                return "UPC-A";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Encoded as EAN-13.</li><li>Most common and well-known in the USA.</li><li>There is 1 number system (NS), 5 manufacturer code, 5 product code and 1 check digit.</li><li>NS Description :<br />0 = Regular UPC Code<br />2 = Weight Items<br />3 = Drug/Health Items<br />4 = In-Store Use on Non-Food Items<br />5 = Coupons<br />7 = Regular UPC Code<br />And other are Reserved.</li></ul>";
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
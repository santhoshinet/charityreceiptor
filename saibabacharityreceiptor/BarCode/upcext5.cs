using System;
using Barcode;

namespace saibabacharityreceiptor.BarCode
{
    public class Upcext5 : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGupcext5);
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
                return "UPC Extension 5 Digits";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Extension for UPC-A, UPC-E, EAN-13 and EAN-8.</li><li>Used to encode suggested retail price.</li><li>If the first number is a 0, the price xx.xx is expressed in British Pounds. If it is a 5, it is expressed in US dollars.</li><li>Special Code Description :<br />90000 : No suggested retail price<br />99991 : The item is a complementary of another one. Normally free<br />99990 : Used bh National Association of College Stores to mark \"used book\".<br />90001 to 98999 : Internal purposes for some publishers.</li></ul>";
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
using System;

namespace Barcode.code
{
    public class Isbn : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGisbn);
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
                return "ISBN-10 / ISBN-13";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>ISBN stands for International Standard Book Number.</li><li>ISBN type is based on EAN-13.</li><li>Previously, all ISBN were in EAN-10 format. EAN-13 uses the same encoding but may contain different data in the ISBN number.</li><li>Composed by a GS1 prefix (for ISBN-13), a group identifier, a publisher code, an item number and a check digit.</li></ul>";
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
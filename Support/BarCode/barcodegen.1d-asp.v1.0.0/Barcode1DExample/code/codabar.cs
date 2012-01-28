using System;

namespace Barcode.code
{
    public class Codabar : Barcode
    {
        public override Type Code
        {
            get
            {
                return typeof(BarcodeGenerator.BCGcodabar);
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
                return "Codabar";
            }
        }

        public override string Explanation
        {
            get
            {
                return "<ul style=\"margin: 0px; padding-left: 25px;\"><li>Known also as Ames Code, NW-7, Monarch, 2 of 7, Rationalized Codabar.</li><li>Codabar was developed in 1972 by Pitney Bowes, Inc.</li><li>This symbology is useful to encode digital information. It is a self-checking code, there is no check digit.</li><li>Codabar is used by blood bank, photo labs, library, FedEx...</li><li>Coding can be with an unspecified length composed by numbers, plus and minus sign, colon, slash, dot, dollar.</li></ul>";
            }
        }

        public override KeyCode[] Keys
        {
            get
            {
                return Utility.MergeKeyCode(Utility.KeyCodeNumber, new KeyCode('-'), new KeyCode('$'), new KeyCode(':'), new KeyCode('/'), new KeyCode('.'), new KeyCode('+'), new KeyCode('A'), new KeyCode('B'), new KeyCode('C'), new KeyCode('D'));
            }
        }
    }
}
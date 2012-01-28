using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Barcode;
using saibabacharityreceiptor.BarCode;
using Enumerable = System.Linq.Enumerable;

namespace saibabacharityreceiptor
{
    public class Utilities
    {
        public static string GenerateReceiptId()
        {
            var currentUtcTime = DateTime.Now.ToUniversalTime();
            return "SAI" + currentUtcTime.ToString("yyyy") +
                   string.Concat(Enumerable.Repeat('0', 3 - currentUtcTime.DayOfYear.ToString().Length)) +
                   currentUtcTime.DayOfYear +
                   string.Concat(Enumerable.Repeat('0', 2 - currentUtcTime.Hour.ToString().Length)) +
                   currentUtcTime.Hour +
                   string.Concat(Enumerable.Repeat('0', 2 - currentUtcTime.Minute.ToString().Length)) +
                   currentUtcTime.Minute +
                   string.Concat(Enumerable.Repeat('0', 2 - currentUtcTime.Second.ToString().Length)) +
                   currentUtcTime.Second;
        }

        static Utilities()
        {
            CodeType.Add(typeof(Codabar), "Codabar");
            CodeType.Add(typeof(Code11), "Code 11");
            CodeType.Add(typeof(Code39), "Code 39");
            CodeType.Add(typeof(Code39Extended), "Code 39 Extended");
            CodeType.Add(typeof(Code93), "Code 93");
            CodeType.Add(typeof(Code128), "Code 128");
            CodeType.Add(typeof(Ean8), "EAN-8");
            CodeType.Add(typeof(Ean13), "EAN-13");
            CodeType.Add(typeof(Isbn), "ISBN-10 / ISBN-13");
            CodeType.Add(typeof(I25), "Interleaved 2 of 5");
            CodeType.Add(typeof(S25), "Standard 2 of 5");
            CodeType.Add(typeof(Msi), "MSI Plessey");
            CodeType.Add(typeof(Upca), "UPC-A");
            CodeType.Add(typeof(Upce), "UPC-E");
            CodeType.Add(typeof(Upcext2), "UPC Extension 2 Digits");
            CodeType.Add(typeof(Upcext5), "UPC Extension 5 Digits");
            CodeType.Add(typeof(Postnet), "Postnet");
            CodeType.Add(typeof(Othercode), "Other Barcode");

            OutputType.Add(1, "Bitmap (BMP)");
            OutputType.Add(2, "Portable Network Graphics (PNG)");
            OutputType.Add(3, "Joint Photographic Experts Group (JPEG)");
            OutputType.Add(4, "Graphics Interchange Format (GIF)");

            FontType.Add("Arial");
            FontType.Add("Courier New");

            KeyCodeNumber.Add(new KeyCode('0'));
            KeyCodeNumber.Add(new KeyCode('1'));
            KeyCodeNumber.Add(new KeyCode('2'));
            KeyCodeNumber.Add(new KeyCode('3'));
            KeyCodeNumber.Add(new KeyCode('4'));
            KeyCodeNumber.Add(new KeyCode('5'));
            KeyCodeNumber.Add(new KeyCode('6'));
            KeyCodeNumber.Add(new KeyCode('7'));
            KeyCodeNumber.Add(new KeyCode('8'));
            KeyCodeNumber.Add(new KeyCode('9'));

            KeyCodeLettersCapital.Add(new KeyCode('A'));
            KeyCodeLettersCapital.Add(new KeyCode('B'));
            KeyCodeLettersCapital.Add(new KeyCode('C'));
            KeyCodeLettersCapital.Add(new KeyCode('D'));
            KeyCodeLettersCapital.Add(new KeyCode('E'));
            KeyCodeLettersCapital.Add(new KeyCode('F'));
            KeyCodeLettersCapital.Add(new KeyCode('G'));
            KeyCodeLettersCapital.Add(new KeyCode('H'));
            KeyCodeLettersCapital.Add(new KeyCode('I'));
            KeyCodeLettersCapital.Add(new KeyCode('J'));
            KeyCodeLettersCapital.Add(new KeyCode('K'));
            KeyCodeLettersCapital.Add(new KeyCode('L'));
            KeyCodeLettersCapital.Add(new KeyCode('M'));
            KeyCodeLettersCapital.Add(new KeyCode('N'));
            KeyCodeLettersCapital.Add(new KeyCode('O'));
            KeyCodeLettersCapital.Add(new KeyCode('P'));
            KeyCodeLettersCapital.Add(new KeyCode('Q'));
            KeyCodeLettersCapital.Add(new KeyCode('R'));
            KeyCodeLettersCapital.Add(new KeyCode('S'));
            KeyCodeLettersCapital.Add(new KeyCode('T'));
            KeyCodeLettersCapital.Add(new KeyCode('U'));
            KeyCodeLettersCapital.Add(new KeyCode('V'));
            KeyCodeLettersCapital.Add(new KeyCode('W'));
            KeyCodeLettersCapital.Add(new KeyCode('X'));
            KeyCodeLettersCapital.Add(new KeyCode('Y'));
            KeyCodeLettersCapital.Add(new KeyCode('Z'));

            KeyCodeLettersLower.Add(new KeyCode('a'));
            KeyCodeLettersLower.Add(new KeyCode('b'));
            KeyCodeLettersLower.Add(new KeyCode('c'));
            KeyCodeLettersLower.Add(new KeyCode('d'));
            KeyCodeLettersLower.Add(new KeyCode('e'));
            KeyCodeLettersLower.Add(new KeyCode('f'));
            KeyCodeLettersLower.Add(new KeyCode('g'));
            KeyCodeLettersLower.Add(new KeyCode('h'));
            KeyCodeLettersLower.Add(new KeyCode('i'));
            KeyCodeLettersLower.Add(new KeyCode('j'));
            KeyCodeLettersLower.Add(new KeyCode('k'));
            KeyCodeLettersLower.Add(new KeyCode('l'));
            KeyCodeLettersLower.Add(new KeyCode('m'));
            KeyCodeLettersLower.Add(new KeyCode('n'));
            KeyCodeLettersLower.Add(new KeyCode('o'));
            KeyCodeLettersLower.Add(new KeyCode('p'));
            KeyCodeLettersLower.Add(new KeyCode('q'));
            KeyCodeLettersLower.Add(new KeyCode('r'));
            KeyCodeLettersLower.Add(new KeyCode('s'));
            KeyCodeLettersLower.Add(new KeyCode('t'));
            KeyCodeLettersLower.Add(new KeyCode('u'));
            KeyCodeLettersLower.Add(new KeyCode('v'));
            KeyCodeLettersLower.Add(new KeyCode('w'));
            KeyCodeLettersLower.Add(new KeyCode('x'));
            KeyCodeLettersLower.Add(new KeyCode('y'));
            KeyCodeLettersLower.Add(new KeyCode('z'));

            KeyCodeFull.Add(new KeyCode((char)0, "NUL"));
            KeyCodeFull.Add(new KeyCode((char)1, "SOH"));
            KeyCodeFull.Add(new KeyCode((char)2, "STX"));
            KeyCodeFull.Add(new KeyCode((char)3, "ETX"));
            KeyCodeFull.Add(new KeyCode((char)4, "EOT"));
            KeyCodeFull.Add(new KeyCode((char)5, "ENQ"));
            KeyCodeFull.Add(new KeyCode((char)6, "ACK"));
            KeyCodeFull.Add(new KeyCode((char)7, "BEL"));
            KeyCodeFull.Add(new KeyCode((char)8, "BS"));
            KeyCodeFull.Add(new KeyCode((char)9, "TAB"));
            KeyCodeFull.Add(new KeyCode((char)10, "LF"));
            KeyCodeFull.Add(new KeyCode((char)11, "VT"));
            KeyCodeFull.Add(new KeyCode((char)12, "FF"));
            KeyCodeFull.Add(new KeyCode((char)13, "CR"));
            KeyCodeFull.Add(new KeyCode((char)14, "SO"));
            KeyCodeFull.Add(new KeyCode((char)15, "SI"));
            KeyCodeFull.Add(new KeyCode((char)16, "DLE"));
            KeyCodeFull.Add(new KeyCode((char)17, "DC1"));
            KeyCodeFull.Add(new KeyCode((char)18, "DC2"));
            KeyCodeFull.Add(new KeyCode((char)19, "DC3"));
            KeyCodeFull.Add(new KeyCode((char)20, "DC4"));
            KeyCodeFull.Add(new KeyCode((char)21, "NAK"));
            KeyCodeFull.Add(new KeyCode((char)22, "SYN"));
            KeyCodeFull.Add(new KeyCode((char)23, "ETB"));
            KeyCodeFull.Add(new KeyCode((char)24, "CAN"));
            KeyCodeFull.Add(new KeyCode((char)25, "EM"));
            KeyCodeFull.Add(new KeyCode((char)26, "SUB"));
            KeyCodeFull.Add(new KeyCode((char)27, "ESC"));
            KeyCodeFull.Add(new KeyCode((char)28, "FS"));
            KeyCodeFull.Add(new KeyCode((char)29, "GS"));
            KeyCodeFull.Add(new KeyCode((char)30, "RS"));
            KeyCodeFull.Add(new KeyCode((char)31, "US"));
            KeyCodeFull.Add(new KeyCode(' '));
            KeyCodeFull.Add(new KeyCode('!'));
            KeyCodeFull.Add(new KeyCode('"'));
            KeyCodeFull.Add(new KeyCode('#'));
            KeyCodeFull.Add(new KeyCode('$'));
            KeyCodeFull.Add(new KeyCode('%'));
            KeyCodeFull.Add(new KeyCode('&'));
            KeyCodeFull.Add(new KeyCode('\''));
            KeyCodeFull.Add(new KeyCode('('));
            KeyCodeFull.Add(new KeyCode(')'));
            KeyCodeFull.Add(new KeyCode('*'));
            KeyCodeFull.Add(new KeyCode('+'));
            KeyCodeFull.Add(new KeyCode(','));
            KeyCodeFull.Add(new KeyCode('-'));
            KeyCodeFull.Add(new KeyCode('.'));
            KeyCodeFull.Add(new KeyCode('/'));
            KeyCodeFull.AddRange(KeyCodeNumber);
            KeyCodeFull.Add(new KeyCode(':'));
            KeyCodeFull.Add(new KeyCode(';'));
            KeyCodeFull.Add(new KeyCode('<'));
            KeyCodeFull.Add(new KeyCode('='));
            KeyCodeFull.Add(new KeyCode('>'));
            KeyCodeFull.Add(new KeyCode('?'));
            KeyCodeFull.Add(new KeyCode('@'));
            KeyCodeFull.AddRange(KeyCodeLettersCapital);
            KeyCodeFull.Add(new KeyCode('['));
            KeyCodeFull.Add(new KeyCode('\\'));
            KeyCodeFull.Add(new KeyCode(']'));
            KeyCodeFull.Add(new KeyCode('^'));
            KeyCodeFull.Add(new KeyCode('_'));
            KeyCodeFull.Add(new KeyCode('`'));
            KeyCodeFull.AddRange(KeyCodeLettersLower);
            KeyCodeFull.Add(new KeyCode('{'));
            KeyCodeFull.Add(new KeyCode('|'));
            KeyCodeFull.Add(new KeyCode('}'));
            KeyCodeFull.Add(new KeyCode('~'));
            KeyCodeFull.Add(new KeyCode((char)127, "DEL"));
        }

        public static KeyCode[] MergeKeyCode(params List<KeyCode>[] listKeyCode)
        {
            var list = new List<KeyCode>();
            foreach (List<KeyCode> keyCodes in listKeyCode)
            {
                list.AddRange(keyCodes);
            }

            return list.ToArray();
        }

        public static KeyCode[] MergeKeyCode(params object[] keyCode)
        {
            Type type1 = typeof(List<KeyCode>);
            Type type2 = typeof(KeyCode);

            var list = new List<KeyCode>();
            foreach (object obj in keyCode)
            {
                Type typeDyn = obj.GetType();
                if (typeDyn == type1)
                {
                    ((List<KeyCode>)obj).AddRange(list);
                }
                else if (typeDyn == type2)
                {
                    list.Add(obj as KeyCode);
                }
            }

            return list.ToArray();
        }

        public static Dictionary<Type, string> CodeType = new Dictionary<Type, string>();

        public static Dictionary<int, string> OutputType = new Dictionary<int, string>();

        public static List<string> FontType = new List<string>();

        public static List<KeyCode> KeyCodeNumber = new List<KeyCode>();

        public static List<KeyCode> KeyCodeLettersCapital = new List<KeyCode>();

        public static List<KeyCode> KeyCodeLettersLower = new List<KeyCode>();

        public static List<KeyCode> KeyCodeFull = new List<KeyCode>();

        public static string Encrypt(string text)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(text);
            const string key = "shridisaibabatemplearizona";
            var hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            var tdes = new TripleDESCryptoServiceProvider
                           {
                               Key = keyArray,
                               Mode = CipherMode.ECB,
                               Padding = PaddingMode.PKCS7
                           };
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string text)
        {
            byte[] toEncryptArray = Convert.FromBase64String(text);
            const string key = "shridisaibabatemplearizona";
            var hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
            var tdes = new TripleDESCryptoServiceProvider
                           {
                               Key = keyArray,
                               Mode = CipherMode.ECB,
                               Padding = PaddingMode.PKCS7
                           };
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
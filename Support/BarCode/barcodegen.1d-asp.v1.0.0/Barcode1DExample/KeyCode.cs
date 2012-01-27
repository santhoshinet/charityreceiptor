namespace Barcode
{
    public class KeyCode
    {
        private char _code;

        public KeyCode(char code)
        {
            Init(code, code.ToString());
        }

        public KeyCode(char code, string text)
        {
            Init(code, text);
        }

        public void Init(char code, string text)
        {
            _code = code;
            Text = text;
        }

        public string Code
        {
            get
            {
                return string.Format("%{0:x}", (int)_code);
            }
        }

        public string Text { get; private set; }
    }
}
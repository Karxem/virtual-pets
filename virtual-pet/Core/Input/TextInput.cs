using System.Text.RegularExpressions;

namespace virtual_pet.Core.Input {
    public class TextInput : IInputListener {

        public delegate void OnSubmit(object sender, string text);
        public event OnSubmit onSubmit;


        public const int WHITESPACE = 1;
        public const int LOWERCASE = 2;
        public const int UPPERCASE = 4;
        public const int DIGITS = 8;
        public const int SYMBOLS = 16;
        public const int CUSTOMREGEX = 32;
        public const int LETTERS = LOWERCASE | UPPERCASE;

        private string customRegex = "";

        public string CustomRegex { 
            get { return customRegex; }
            set { customRegex = value; flags |= CUSTOMREGEX; }
        }

        private string regex = "";
        private int flags = 0;
        //public const int DIGITS = 2;


        public bool AllowWhitespace {
            get { return ((flags & WHITESPACE) == WHITESPACE); }
            set { if (value) flags |= WHITESPACE; else flags &= ~WHITESPACE; UpdateRegex(); }
        }
        public bool AllowLowercase {
            get { return ((flags & LOWERCASE) == LOWERCASE); }
            set { if (value) flags |= LOWERCASE; else flags &= ~LOWERCASE; UpdateRegex(); }
        }
        public bool AllowUppercase {
            get { return ((flags & UPPERCASE) == UPPERCASE); }
            set { if (value) flags |= UPPERCASE; else flags &= ~UPPERCASE; UpdateRegex(); }
        }
        public bool AllowLetters { 
            get {  return ((flags  & LETTERS) == LETTERS); }
            set { if (value) flags |= LETTERS; else flags &= ~LETTERS; UpdateRegex(); }
        }
        public bool AllowDigits {
            get { return ((flags & DIGITS) == DIGITS); }
            set { if (value) flags |= DIGITS; else flags &= ~DIGITS; UpdateRegex(); }
        }
        public bool AllowSymbols {
            get { return ((flags & SYMBOLS) == SYMBOLS); }
            set { if (value) flags |= SYMBOLS; else flags &= ~SYMBOLS; UpdateRegex(); }
        }
        public bool AllowCustomRegex {
            get { return ((flags & CUSTOMREGEX) == CUSTOMREGEX); }
            set { if (value) flags |= CUSTOMREGEX; else flags &= ~CUSTOMREGEX; }
        }

        public void UpdateRegex() {
            //regex with named groups
            //(?<lower>[a-z])|(?<upper>[A-Z])|(?<number>[0-9])|(?<symbols>[!-/:-@\[-`{-~])
            //regex no groups
            //[a-zA-Z0-9!-/:-@\[-`{-~]
            
            string s = "";
            s += AllowLowercase ?   "(?<lower>[a-z])|" : "";
            s += AllowUppercase ?   "(?<upper>[A-Z])|" : "";
            s += AllowDigits ?      "(?<number>[0-9])|" : "";
            s += AllowSymbols ?     "(?<symbol>[!-/:-@\\[-`{-~])|" : "";//the last | is for consistency
            s += AllowWhitespace ?  "?<whitespace>[\\s])|" : "";
            if(string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
                regex = "";
                return;
            }
            regex = s.Substring(0, s.Length-1);
        }

        

        public bool IsReady = false;
        string text = "";

        public TextInput(OnSubmit onSubmit) {
            this.onSubmit = onSubmit;
            this.flags = LETTERS | DIGITS | SYMBOLS;
            UpdateRegex();
        }

        public TextInput(OnSubmit onSubmit, int flags) {
            this.onSubmit = onSubmit;
            this.flags = flags;
            UpdateRegex();
        }

        public TextInput(OnSubmit onSubmit, bool lower, bool upper, bool digits, bool symbols, bool whitespace) {
            this.onSubmit = onSubmit;
            this.AllowUppercase = upper;
            this.AllowLowercase = lower;
            this.AllowDigits = digits;
            this.AllowSymbols = symbols;
            this.AllowWhitespace = whitespace;
        }

        public TextInput(OnSubmit onSubmit, string customRegex, int flags = 0) {
            this.CustomRegex = customRegex;
            this.flags = flags;
            UpdateRegex();
        }

        public static TextInput GetTextInput(OnSubmit onSubmit, bool whitespace = false) {
            return new TextInput(onSubmit, LETTERS | (whitespace ? WHITESPACE : 0));
        }

        public static TextInput GetNumberInput(OnSubmit onSubmit) {
            return new TextInput(onSubmit, DIGITS);
        }

        public static TextInput GetTextNumberInput(OnSubmit onSubmit, bool whitespace = false) {
            return new TextInput(onSubmit ,LETTERS | DIGITS | (whitespace ? WHITESPACE : 0));
        }

        public static TextInput GetInput(OnSubmit onSubmit, int flags) {
            return new TextInput(onSubmit, flags);
        }

        public static TextInput GetCustomInput(OnSubmit onSubmit, string customRegex, int flags = 0) {
            return new TextInput(onSubmit, customRegex, flags);
        }

        public void KeyPressed(ConsoleKeyInfo key) {
            switch(key.Key)
            {
                case ConsoleKey.Enter:
                    IsReady = true;
                    return;

                default:
                    if(!AllowCustomRegex)
                    {
                        if (Regex.IsMatch(key.KeyChar.ToString(), regex))
                            text += key.KeyChar;
                        break;
                    }
                    if (Regex.IsMatch(key.KeyChar.ToString(), customRegex))
                        text += key.KeyChar;
                    else if (Regex.IsMatch(key.KeyChar.ToString(), regex))
                        text += key.KeyChar;
                    break;
            }
        }
    }
}

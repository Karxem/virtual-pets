using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using virtual_pet.Core.Render;

namespace virtual_pet.Core.Input {
    public class OptionStrip : IDisplayable{
        public List<OptionStripItem> Items { get; } = new List<OptionStripItem>(); 


        public ConsoleColor Background = ConsoleColor.DarkGray;
        public ConsoleColor Foreground = ConsoleColor.Black;
        public ConsoleColor DisabledForeground = ConsoleColor.Gray;

        //public Render.Buffer Buffer { get; set; }

        public OptionStrip(params OptionStripItem[] items) {
            //this.Buffer = buffer;
            Items.AddRange(items);
        }

        public void Display() {
            
        }

        public void Display(Render.Buffer buffer) {
            string s = "";
            int i = 0;
            buffer.BackgroundColor = Background;
            buffer.ForegroundColor = Foreground;
            for (; i < Items.Count - 1; i++)
            {
                if (buffer.Cell + Items[i].ToString().Length > buffer.Width)
                {
                    buffer.WriteLine();
                    if (buffer.Line > buffer.Height)
                        return;
                }
                if (Items[i].Enabled)
                    buffer.Write(Items[i].ToString() + ", ");
                else
                    buffer.Write(Items[i].ToString() + ", ", Background, DisabledForeground);
            }
            if (buffer.Cell + Items[i].ToString().Length <= buffer.Width)
            {
                if (Items[i].Enabled)
                    buffer.Write(Items[i].ToString());
                else
                    buffer.Write(Items[i].ToString(), Background, DisabledForeground);
            }
        }

        

    }
    public class OptionStripItem {
        
        public string Name { get; set; }
        public ConsoleKeyInfo Key { get; set; }
        public bool Enabled { get; set; } = true;

        public OptionStripItem(string name, ConsoleKeyInfo key) {
            Name = name;
            Key = key;
        }
        public OptionStripItem(string name, char character, ConsoleKey key) {
            Name = name;
            Key = new ConsoleKeyInfo(character, key, false, false, false);
        }
        public OptionStripItem(string name, char character, ConsoleKey key, bool shift, bool alt, bool control) {
            Name = name;
            Key = new ConsoleKeyInfo(character, key, shift, alt, control);
        }

        private string ModString() {
            string ret = "";
            ret += (Key.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control ? "Ctrl + " : "";
            ret += (Key.Modifiers & ConsoleModifiers.Shift) == ConsoleModifiers.Shift ? "Shift + " : "";
            ret += (Key.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt ? "Alt + " : "";
            return ret;
        }

        public override string ToString() {
            return $"[{ModString()}{Key.KeyChar}]:" + Name;
        }

    }
}

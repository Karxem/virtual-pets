using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using virtual_pet.Core.Input;

namespace virtual_pet.Core.Utils {
    public class TextInputItem : MenuItemBase {

        public TextInput Input { get; set; }

        public TextInputItem(int id, string text, TextInput input) : base(id, text) { 
            this.Input = input;
        }

        public override void Display(Render.Buffer buffer) {
            ConsoleColor fg = this.IsSeletected ? this.SelectedForeground : this.Foreground;
            ConsoleColor bg = this.IsSeletected ? this.SelectedBackground : this.Background;

            buffer.Write(Text, bg, fg);
            for (int i = Text.Length; i < 24; i++)
                buffer.Write(" ");
            
            Input.Display(buffer);
        }
    }
}

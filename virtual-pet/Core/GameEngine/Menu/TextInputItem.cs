using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using virtual_pet.Core.GameEngine.Common;
using virtual_pet.Core.GameEngine.Input;

namespace virtual_pet.Core.GameEngine.Menu
{
    public class TextInputItem : MenuItemBase
    {

        public TextInput Input { get; set; }

        public TextInputItem(int id, string text, TextInput input) : base(id, text)
        {
            Input = input;
        }

        public override void Display(Render.Buffer buffer)
        {
            ConsoleColor fg = IsSeletected ? SelectedForeground : Foreground;
            ConsoleColor bg = IsSeletected ? SelectedBackground : Background;

            buffer.Write(Text, bg, fg);
            for (int i = Text.Length; i < 24; i++)
                buffer.Write(" ");

            Input.Display(buffer);
        }
    }
}

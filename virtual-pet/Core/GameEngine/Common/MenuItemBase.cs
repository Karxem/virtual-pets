using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using virtual_pet.Core.GameEngine.Render;

namespace virtual_pet.Core.GameEngine.Common
{
    public abstract class MenuItemBase : IDisplayable
    {
        public string Text { get; set; } = "undefined";
        public int Id { get; set; } = -1;

        public bool IsSeletected { get; set; } = false;

        public ConsoleColor SelectedForeground = ConsoleColor.Black;
        public ConsoleColor SelectedBackground = ConsoleColor.White;
        public ConsoleColor Foreground = ConsoleColor.White;
        public ConsoleColor Background = ConsoleColor.Black;

        public MenuItemBase(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public virtual void Display(Render.Buffer buffer)
        {
            ConsoleColor fg = IsSeletected ? SelectedForeground : Foreground;
            ConsoleColor bg = IsSeletected ? SelectedBackground : Background;
            buffer.Write(Text, bg, fg);
        }
    }
}

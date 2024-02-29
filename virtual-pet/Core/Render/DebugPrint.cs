using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render{
    internal class DebugPrint : IDisplayable{
        public void Display(Render.Buffer buffer) {
            buffer.WriteLine("Foregrounds: ", ConsoleColor.Black, ConsoleColor.White);
            buffer.Write("[Red]", ConsoleColor.Black, ConsoleColor.Red);
            buffer.WriteLine("[DarkRed]", ConsoleColor.Black, ConsoleColor.DarkRed);
            buffer.Write("[Green]", ConsoleColor.Black, ConsoleColor.Green);
            buffer.WriteLine("[DarkGreen]", ConsoleColor.Black, ConsoleColor.DarkGreen);
            buffer.Write("[Blue]", ConsoleColor.Black, ConsoleColor.Blue);
            buffer.WriteLine("[DarkBlue]", ConsoleColor.Black, ConsoleColor.DarkBlue);
            buffer.Write("[Yellow]", ConsoleColor.Black, ConsoleColor.Yellow);
            buffer.WriteLine("[DarkYellow]", ConsoleColor.Black, ConsoleColor.DarkYellow);
            buffer.Write("[Cyan]", ConsoleColor.Black, ConsoleColor.Cyan);
            buffer.WriteLine("[DarkCyan]", ConsoleColor.Black, ConsoleColor.DarkCyan);
            buffer.Write("[Magenta]", ConsoleColor.Black, ConsoleColor.Magenta);
            buffer.WriteLine("[DarkMagenta]", ConsoleColor.Black, ConsoleColor.DarkMagenta);
            
            buffer.WriteLine("Backgrounds: ", ConsoleColor.Black, ConsoleColor.White);
            buffer.Write("[Red]", ConsoleColor.Red, ConsoleColor.White);
            buffer.WriteLine("[DarkRed]", ConsoleColor.DarkRed, ConsoleColor.White);
            buffer.Write("[Green]", ConsoleColor.Green, ConsoleColor.White);
            buffer.WriteLine("[DarkGreen]", ConsoleColor.DarkGreen, ConsoleColor.White);
            buffer.Write("[Blue]", ConsoleColor.Blue, ConsoleColor.White);
            buffer.WriteLine("[DarkBlue]", ConsoleColor.DarkBlue, ConsoleColor.White);
            buffer.Write("[Yellow]", ConsoleColor.Yellow, ConsoleColor.White);
            buffer.WriteLine("[DarkYellow]", ConsoleColor.DarkYellow, ConsoleColor.White);
            buffer.Write("[Cyan]", ConsoleColor.Cyan, ConsoleColor.White);
            buffer.WriteLine("[DarkCyan]", ConsoleColor.DarkCyan, ConsoleColor.White);
            buffer.Write("[Magenta]", ConsoleColor.Magenta, ConsoleColor.White);
            buffer.WriteLine("[DarkMagenta]", ConsoleColor.DarkMagenta, ConsoleColor.White);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render
{
    public class Pixel { 
        public ConsoleColor Background { get; set; } = ConsoleColor.Black;
        public ConsoleColor Foreground { get; set; } = ConsoleColor.White;
        public char Character { get; set; } = ' ';

        public Pixel() { }
        public Pixel(ConsoleColor background, ConsoleColor foreground, char character) { 
            this.Background = background; 
            this.Foreground = foreground;
            this.Character = character;
        }

        public void Clear()
        {
            Background = ConsoleColor.Black;
            Foreground = ConsoleColor.White;
            Character = ' ';
        }

        public static implicit operator char(Pixel pixel) { return pixel.Character;}
        public static implicit operator Pixel(char character) { return new Pixel() { Character = character }; }
        public static implicit operator Pixel(ConsoleColor background) { return new Pixel() { Background = background}; }
    }
}

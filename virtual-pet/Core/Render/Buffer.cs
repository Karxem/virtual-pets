using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render
{
    public class Buffer
    {
        public uint X { get; set; }
        public uint Y { get; set; }
        public uint Width { get; private set; }
        public uint Height { get; private set; }

        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor {  get; set; }

        ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;
        ConsoleColor DefaultForegroundColor = ConsoleColor.White;

        uint line = 0;

        Pixel[,] buffer = null;

        public Buffer(uint x, uint y, uint width, uint height)
        {
            SetLocation(x, y);
            InitBuffer(width, height);
        }

        public void SetLocation(uint x, uint y) {
            this.X = x;
            this.Y = y;
        }

        public void InitBuffer(uint width, uint height) {
            this.buffer = new Pixel[width, height];
            this.Width = width;
            this.Height = height;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    this.buffer[i, j] = new Pixel();
                }
            }
        }

        public void Clear()
        {
            line = 0;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    this.buffer[i, j].Clear();
                }
            }
        }

        public void WriteLine(string str)
        {
            for (int i = 0; i < str.Length && i < Width; i++)
            {
                if (str[i] == '\n')
                {
                    line++;
                    continue;
                }
                buffer[i, line].Character = str[i];
                buffer[i, line].Background = BackgroundColor;
                buffer[i, line].Foreground = ForegroundColor;
            }
            line++;
        }

        public void WriteLine(string str, ConsoleColor back, ConsoleColor fore)
        {
            for (int i = 0; i < str.Length && i < Width; i++)
            {
                if (str[i] == '\n')
                {
                    line++;
                    continue;
                }
                buffer[i, line].Character = str[i];
                buffer[i, line].Background = back;
                buffer[i, line].Foreground = fore;
            }
            line++;
        }

        public void ResetColor()
        {
            this.BackgroundColor = DefaultBackgroundColor;
            this.ForegroundColor = DefaultForegroundColor;
        }

        public Pixel this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width ||
                    y < 0 || y >= Height)
                    return new Pixel();
                return buffer[x, y];
            }
            set
            {
                if (x < 0 || x >= Width ||
                    y < 0 || y >= Height)
                    return;
                buffer[x, y] = value;
            }
        }
    }
}

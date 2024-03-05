using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render
{
    public class Buffer : IDisposable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor {  get; set; }

        ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;
        ConsoleColor DefaultForegroundColor = ConsoleColor.White;

        public int Line { get; private set; } = 0;
        public int Cell { get; private set; } = 0;

        public Buffer Parent { get; private set; }

        Pixel[,] buffer = null;

        private List<Buffer> children = new List<Buffer>();

        public Buffer(int x, int y, int width, int height)
        {
            SetLocation(x, y);
            SetSize(width, height);
        }

        private Buffer(int x, int y, int width, int height, Buffer parent) {
            this.Parent = parent;
            SetLocation(x, y);
            SetSize(width, height);
        }

        /**
         * <summary>creates a new buffer based on this instance and becomes its child</summary>
         * <returns>the newly derived child buffer</returns>
         */
        public Buffer Derive(int x, int y, int width, int height) {
            Buffer b = new Buffer(x,y,width,height,this);
            this.children.Add(b);
            return b;
        }

        public void SetLocation(int x, int y) {
            this.X = x;
            this.Y = y;
        }


        public void Fill() {
            foreach (Buffer b in children)
            {
                FillBuffer(b);
            }
        }

        private void FillBuffer(Buffer buf) {
            for (int i = 0; i < buf.Width && i + buf.X < this.Width ;  i++)
            {
                for (int j = 0; j < buf.Height &&j + buf.Y < this.Height; j++)
                {
                    this[buf.X + i, buf.Y + j] = buf[i, j];
                }
            }
        }

        public void SetSize(int width, int height) {
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
            Line = 0;
            Cell = 0;
            foreach(Buffer b in children)
            {
                b.Clear();
            }
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    this.buffer[i, j].Clear();
                }
            }
        }

        public void Write(string str) {
            for (int i = 0; i < str.Length && Cell < Width && Line < Height; i++)
            {
                if (str[i] == '\n')
                {
                    Line++;
                    Cell = 0;
                    continue;
                }
                buffer[Cell, Line].Character = str[i];
                buffer[Cell, Line].Background = BackgroundColor;
                buffer[Cell, Line].Foreground = ForegroundColor;
                Cell++;
            }
        }



        public void Write(string str, ConsoleColor back, ConsoleColor fore, bool keepColorChange = false) {
            if (keepColorChange)
            {
                this.BackgroundColor = back;
                this.ForegroundColor = fore;
            }
            for (int i = 0; i < str.Length && Cell < Width && Line < Height; i++)
            {
                if (str[i] == '\n')
                {
                    Line++;
                    Cell = 0;
                    continue;
                }
                buffer[Cell, Line].Character = str[i];
                buffer[Cell, Line].Background = back;
                buffer[Cell, Line].Foreground = fore;
                Cell++;
            }
        }

        public void WriteLine(string str)
        {
            for (int i = 0; i < str.Length && Cell < Width && Line < Height; i++)
            {
                if (str[i] == '\n')
                {
                    Line++;
                    Cell = 0;
                    continue;
                }
                buffer[Cell, Line].Character = str[i];
                buffer[Cell, Line].Background = BackgroundColor;
                buffer[Cell, Line].Foreground = ForegroundColor;
                Cell++;
            }
            Line++;
            Cell = 0;
        }

        

        public void ResetColor()
        {
            this.BackgroundColor = DefaultBackgroundColor;
            this.ForegroundColor = DefaultForegroundColor;
        }
        
        public void WriteLine(string str, ConsoleColor back, ConsoleColor fore, bool keepColorChange = false)
        {
            if (keepColorChange)
            {
                this.BackgroundColor = back;
                this.ForegroundColor = fore;
            }
            for (int i = 0; i < str.Length && Cell < Width && Line < Height; i++)
            {
                if (str[i] == '\n')
                {
                    Line++;
                    Cell = 0;
                    continue;
                }
                buffer[Cell, Line].Character = str[i];
                buffer[Cell, Line].Background = back;
                buffer[Cell, Line].Foreground = fore;
                Cell ++;
            }
            Line++;
            Cell = 0;
        }
        public void WriteLine() {
            Line++;
            Cell = 0;
        }

        public void Dispose() {
            foreach(Buffer b in children)
            {
                b.Dispose();
            }
            Parent.children.Remove(this);
            this.buffer = null;
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

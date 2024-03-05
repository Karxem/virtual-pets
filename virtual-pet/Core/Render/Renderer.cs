using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render
{
    public class Renderer
    {

        public static Buffer MainBuffer = new Buffer(0, 0, 90, 24);
        public static Buffer PlayBuffer = new Buffer(0, 0, 90, 16);
        public static Buffer MenuBuffer = new Buffer(0, 16, 90, 6);
        public static Buffer OptionStripBuffer = new Buffer(0, 22, 90, 2);

        public static double PlayBufferHeightPct { get; set; } = 0.7;
        public static double MenuBufferHeightPct { get; set; } = 0.2;
        public static double OptionStripBufferHeightPct { get; set; } = 0.1;

        public int MinimumWidth { get; set; } = 20;
        public int MinimumHeight { get; set; } = 20;
        public int Width { get; private set; }
        public int Height { get; private set; }

        //private static StringBuilder builder = new StringBuilder();

        public void SetSize(int width, int height) {
            MainBuffer.SetSize(width, height);
            PlayBuffer.SetSize(width, height);
            MenuBuffer.SetLocation(0, PlayBuffer.Y + PlayBuffer.Height);
            MenuBuffer.SetSize(width, height);
            MenuBuffer.SetLocation(0, MenuBuffer.Y + MenuBuffer.Height);
            OptionStripBuffer.SetSize(width, height);

        }

        static  StreamWriter writer = new StreamWriter(Console.OpenStandardOutput());


        public static List<Buffer> buffers = new();

        public void Init() {
            
        }

        public static void FitToScreen() {
            int w = Console.BufferWidth;
            int h = Console.BufferHeight;

            MainBuffer.SetSize(w, h);
            PlayBuffer.SetSize(w, (int)(h* PlayBufferHeightPct));
            MenuBuffer.SetSize(w, (int)(h * MenuBufferHeightPct));
            OptionStripBuffer.SetSize(w, (int)(h * OptionStripBufferHeightPct));

        }

        public static void FillBuffer(Buffer buffer)
        {
            for (int i = 0; i < buffer.Width; i++)
            {
                for (int j = 0; j < buffer.Height; j++)
                {
                    MainBuffer[buffer.X + i, buffer.Y + j] = buffer[i, j];
                }
            }
        }

        public static void FillBuffers()
        {
            FillBuffer(PlayBuffer);
            FillBuffer(MenuBuffer);
            FillBuffer(OptionStripBuffer);
            foreach(Buffer b in buffers)
            {
                b.Fill();
                FillBuffer(b);
            }
        }

        public static void ClearBuffers()
        {
            MainBuffer.Clear();
            PlayBuffer.Clear();
            MenuBuffer.Clear();
            OptionStripBuffer.Clear();
            foreach (Buffer b in buffers)
            {
                b.Clear();
            }
        }

        
        public static void Render()
        {
            
            StringBuilder builder = new StringBuilder();
            //builder.Clear();
            //builder.Append("\u001b[2J");//\u001b[0;0H");
            FillBuffers();
            for (int line = 0; line < MainBuffer.Height && line < Console.WindowHeight; line++)
            {
                for (int i = 0; i < MainBuffer.Width && i < Console.WindowWidth; i++)
                {
                    //this monster is to optimise the console string buffer size
                    //checks if the previus cell uses the same colors, and only appends
                    //the ansi color code if it isn't
                    if(i > 0)
                    {
                        if(MainBuffer[i, line].Background == MainBuffer[i-1, line].Background)
                        {
                            if(MainBuffer[i, line].Foreground == MainBuffer[i-1, line].Foreground)
                            {
                                builder.Append(MainBuffer[i, line].Character);
                                continue;
                            }
                            builder.Append(ColorCodes.GetForeground(MainBuffer[i, line].Foreground) + MainBuffer[i, line].Character);
                            continue;
                        }
                        if (MainBuffer[i, line].Foreground == MainBuffer[i - 1, line].Foreground)
                        {
                            builder.Append(ColorCodes.GetBackground(MainBuffer[i, line].Background) + MainBuffer[i, line].Character);
                            continue;
                        }
                    }
                    builder.Append(ColorCodes.GetColor(MainBuffer[i, line].Background, MainBuffer[i, line].Foreground) + MainBuffer[i, line].Character);
                    //builder.Append(MainBuffer[i, line].Character);
                }
                builder.AppendLine();
            }
            //writer.Clear();
           // Console.Clear();
            //writer.WriteLine("\u001b[2J");4
            writer.Write("\u001b[0;0H");
            writer.Flush();
            writer.Write(builder);
            writer.Flush();
            
        }
    }
}

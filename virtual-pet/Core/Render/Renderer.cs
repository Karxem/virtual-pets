using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render
{
    public class Renderer
    {

        public static Buffer MainBuffer = new Buffer(0, 0, 90, 36);
        public static Buffer PlayBuffer = new Buffer(0, 0, 90, 28);
        public static Buffer MenuBuffer = new Buffer(0, 28, 90, 6);
        public static Buffer OptionStripBuffer = new Buffer(0, 32, 90, 2);

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

        public static List<Buffer> buffers = new();

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

        /*
         * performance increase
         */
        public static void Render()
        {
            
            StringBuilder builder = new StringBuilder();
            //builder.Clear();

            FillBuffers();
            for (int line = 0; line < MainBuffer.Height; line++)
            {
                for (int i = 0; i < MainBuffer.Width; i++)
                {
                    builder.Append(ColorCodes.GetColor(MainBuffer[i, line].Background, MainBuffer[i, line].Foreground) + MainBuffer[i, line].Character);
                }
                builder.AppendLine();
            }
            //Thread.Sleep(300);
            //Console.Out.Flush();
            Console.Clear();
            //Thread.Sleep(200);
            Console.Write(builder);
            //Console.WriteLine("Builder length: "+ builder.Length);
            //Console.WriteLine("Buffer Width: "+MainBuffer.Width+ "Height: "+ MainBuffer.Height);
        }
    }
}

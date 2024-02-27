using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render
{
    public class Renderer
    {


        public static Buffer MainBuffer = new Buffer(0, 0, 64, 32);
        public static Buffer PlayBuffer = new Buffer(0, 0, 64, 26);
        public static Buffer MenuBuffer = new Buffer(0, 26, 64, 8);


        public static List<Buffer> buffers = new();



        public static void FillBuffer(Buffer buffer)
        {
            for (int i = 0; i < buffer.Width; i++)
            {
                for (int j = 0; j < buffer.Height; j++)
                {
                    MainBuffer[(int)buffer.X + i, (int)buffer.Y + j] = buffer[i, j];
                }
            }
        }

        public static void FillBuffers()
        {
            Console.Clear();
            FillBuffer(PlayBuffer);
            FillBuffer(MenuBuffer);
            foreach(Buffer b in buffers)
            {
                FillBuffer(b);
            }
        }

        public static void ClearBuffers()
        {
            PlayBuffer.Clear();
            MenuBuffer.Clear();
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
            FillBuffers();


            for (int line = 0; line < MainBuffer.Height; line++)
            {
                for (int i = 0; i < MainBuffer.Width; i++)
                {

                    builder.Append(ColorCodes.GetColor(MainBuffer[i, line].Background, MainBuffer[i, line].Foreground) + MainBuffer[i, line].Character);
                    //Console.BackgroundColor = MainBuffer[i, line].Background;
                    //Console.ForegroundColor = MainBuffer[i, line].Foreground;
                    //Console.Write(MainBuffer[i, line].Character);
                }
                builder.AppendLine();
            }
            Console.Clear();
            Console.Write(builder);
        }

        /*
         * Dont use, looks like Shit (flickers)
         */
        public static void RenderOld()
        {
            FillBuffers();
            Console.Clear();
            for (int line = 0; line < MainBuffer.Height; line++)
            {
                for (int i = 0; i < MainBuffer.Width; i++)
                {
                    Console.BackgroundColor = MainBuffer[i, line].Background;
                    Console.ForegroundColor = MainBuffer[i, line].Foreground;
                    Console.Write(MainBuffer[i, line].Character);
                }
                Console.WriteLine();
            }
        }
    }
}

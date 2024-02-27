using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render
{
    public class Renderer
    {

        
        public static Buffer MainBuffer = new Buffer(0, 0, 32, 20);
        public static Buffer PlayBuffer = new Buffer(0, 0, 32, 12);
        public static Buffer MenuBuffer = new Buffer(0, 13, 32, 7);


        List<Buffer> buffers = new ();



        public static void FillBuffer(Buffer buffer) {
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

        }

        public static void ClearBuffers() {
            PlayBuffer.Clear();
            MenuBuffer.Clear();
        }

        public static void Render() {
            FillBuffers();
            Console.Clear();
            for(int line=0; line < MainBuffer.Height; line++)
            {
                for(int i =0; i< MainBuffer.Width; i++)
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

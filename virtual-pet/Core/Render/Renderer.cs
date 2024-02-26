using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Render
{
    public class Renderer
    {

        
        public static Buffer MainBuffer= new Buffer(0,0,128, 48);
        public static Buffer MenuBuffer = new Buffer(0,32, 60, 16);


        List<Buffer> buffers = new ();



        public static void Render()
        {
            Console.Clear();

            for(int i = 0; i < MenuBuffer.Width; i++) {
                for (int j = 0; j < MenuBuffer.Height; j++)
                {
                    MainBuffer[MenuBuffer.X + i, MenuBuffer.Y + j] = MenuBuffer[i, j];
                }
            }

            //for (int i = 0; i < MainBuffer.Width; i++)
            //{
            //    for (int j = 0; j < MainBuffer.Height; j++)
            //    {

            //    }
            //}

        }
    }
}

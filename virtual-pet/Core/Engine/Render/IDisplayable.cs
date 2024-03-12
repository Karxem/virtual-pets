using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.GameEngine.Render
{
    public interface IDisplayable
    {

        //public Render.Buffer Buffer { get; set; }

        //public void Display();
        public void Display(Buffer buffer);
    }
}

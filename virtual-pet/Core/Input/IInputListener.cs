using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Input {
    public interface IInputListener {
        public void KeyPressed(ConsoleKeyInfo info);
    }
}

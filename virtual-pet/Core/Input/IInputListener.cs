using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Input {
    public interface IInputListener {
        public void KeyPressed(ConsoleKeyInfo info);
        public OptionStrip? GetOptionStrip() => null;
        public bool RequieresPassAll() => false;
    }
}

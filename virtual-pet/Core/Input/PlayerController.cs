using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Input {
    public class PlayerController {
        public IInputListener? Listener { get; set; }


        public void Tick() {
            if(!Console.KeyAvailable) 
                return;
            ConsoleKeyInfo? key = null;
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
            }
            Listener?.KeyPressed(key.Value);
        }


        public ConsoleKeyInfo? FetchInput() {
            ConsoleKeyInfo? key = null;
            while (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
            }
            return key;
        }
    }
}

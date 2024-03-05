using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using virtual_pet.Core.Input;
using virtual_pet.Core.Utils;

namespace virtual_pet.Core.Menu {
    public class MainMenu {

        public ConsoleMenu Menu { get; private set; }
        public ClickItem ExitItem { get; private set; }

        public MainMenu() {
            Menu = new ConsoleMenu();
            ExitItem = Menu.CreateClickItem("Exit Game", onGameExit);
        
        }


        private void onGameExit(object sender, string text) {

        }
        
    }
}

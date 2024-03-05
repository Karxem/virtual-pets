
using virtual_pet.Core.Render;
using virtual_pet.Core.Utils;

namespace virtual_pet.Core.Menu {
    public class DebugMenu {

        public ConsoleMenu Menu { get; private set; }
        public ClickItem ToggleDebugPrint { get; private set; }
        public TextInputItem DebugTextInput { get; private set; }

        static DebugPrint debugPrint = new DebugPrint();
        


        public DebugMenu() {
            Menu = new ConsoleMenu();
            ToggleDebugPrint = Menu.CreateClickItem("Debug Color Print", onDebugPrint);
            DebugTextInput = Menu.CreateTextInputItem("Debug Text Input", onDebugTextInput);
        }


        private void onDebugPrint(object sender, string text) {
            if (Engine.ContainsDisplayable(debugPrint))
            {
                Engine.RemoveDisplayable(debugPrint);
                return;
            }
            Engine.AddDisplayable(debugPrint);
        }

        private void onDebugTextInput(object sender, string text) {
            Engine.PopInput();
        }
    }
}

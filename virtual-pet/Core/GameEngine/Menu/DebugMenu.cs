using virtual_pet.Core.GameEngine.Render;
using virtual_pet.Core.GameEngine.Common;

namespace virtual_pet.Core.GameEngine.Menu
{
    public class DebugMenu
    {

        public ConsoleMenu Menu { get; private set; }
        public ClickItem ToggleDebugPrint { get; private set; }
        public TextInputItem DebugTextInput { get; private set; }

        static DebugPrint debugPrint = new DebugPrint();



        public DebugMenu()
        {
            Menu = new ConsoleMenu();
            ToggleDebugPrint = Menu.CreateClickItem("Debug Color Print", onDebugPrint);
            DebugTextInput = Menu.CreateTextInputItem("Debug Text Input", onDebugTextInput);

            for (int i = 0; i < 10; i++)
            {
                Menu.CreateClickItem("Item " + i);
            }

        }


        private void onDebugPrint(object sender, string text)
        {
            if (GameEngine.ContainsDisplayable(debugPrint))
            {
                GameEngine.RemoveDisplayable(debugPrint);
                return;
            }
            GameEngine.CloseMenu();
        }

        private void onDebugTextInput(object sender, string text)
        {
            GameEngine.PopInput();
        }
    }
}

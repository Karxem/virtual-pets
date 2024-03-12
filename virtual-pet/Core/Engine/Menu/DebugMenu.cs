using virtual_pet.Core.GameEngine.Common;
using virtual_pet.Core.GameEngine.Render;

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
            if (Engine.ContainsDisplayable(debugPrint))
            {
                Engine.RemoveDisplayable(debugPrint);
                return;
            }
            Engine.CloseMenu();
        }

        private void onDebugTextInput(object sender, string text)
        {
            Engine.PopInput();
        }
    }
}

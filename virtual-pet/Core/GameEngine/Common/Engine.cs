using virtual_pet.Core.Level.Common;
using virtual_pet.Core.GameEngine.Input;
using virtual_pet.Core.GameEngine.Menu;
using virtual_pet.Core.GameEngine.Render;

namespace virtual_pet.Core.Engine.Common
{
    public class Engine
    {

        string s = "▶◀▲▼✓╠╣║╚╝╔╗↲⇡⇣←↑→↓↔↕";

        private static PlayerController controller = new PlayerController();

        private static Stack<IInputListener> inputListeners = new Stack<IInputListener>();

        private static List<IDisplayable> displayables = new List<IDisplayable>();

        private static Stack<ConsoleMenu> menus = new Stack<ConsoleMenu>();

        private static ConsoleMenu currentMenu;

        private static OptionStrip? currentOtionStrip;

        private static LevelBase? currentLevel;

        public static bool IsRunning { get; private set; } = false;

        public static void setCurrentLevel(LevelBase level)
        {
            currentLevel = level;
        }


        public static void Init()
        {
            controller.Listener = new MainInputListener();
            inputListeners.Push(controller.Listener);
            controller.Listener.IsActive = true;
            IsRunning = true;
        }

        public static void Tick()
        {
            controller.Tick();
            foreach (IDisplayable d in displayables)
            {
                d.Display(Renderer.PlayBuffer);
            }
            currentMenu?.Display(Renderer.MenuBuffer);
            currentOtionStrip?.Display(Renderer.OptionStripBuffer);
            currentLevel?.Display(Renderer.PlayBuffer);
        }

        public static void AddDisplayable(IDisplayable displayable)
        {
            displayables.Add(displayable);
        }

        public static bool ContainsDisplayable(IDisplayable dislpayable)
        {
            return displayables.Contains(dislpayable);
        }

        public static void RemoveDisplayable(IDisplayable displayable)
        {
            displayables.Remove(displayable);
        }


        public static void PassInput(IInputListener listener)
        {
            if (controller.Listener != null)
                controller.Listener.IsActive = false;
            inputListeners.Push(controller.Listener);
            controller.Listener = listener;
            listener.IsActive = true;
            currentOtionStrip = listener.GetOptionStrip();
        }

        public static void PopInput()
        {
            controller.Listener.IsActive = false;
            controller.Listener = inputListeners.Pop();
            controller.Listener.IsActive = true;
            currentOtionStrip = controller.Listener?.GetOptionStrip();
        }

        public static void OpenMenu(ConsoleMenu consoleMenu)
        {
            menus.Push(currentMenu);
            currentMenu = consoleMenu;
            PassInput(consoleMenu);
        }

        public static void CloseMenu()
        {
            currentMenu = menus.Pop();
            PopInput();
        }

    }
    public class MainInputListener : IInputListener
    {

        public bool IsActive { get; set; }
        public void KeyPressed(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    Engine.OpenMenu(Menus.MainMenu.Menu);
                    break;
                case ConsoleKey.F2:
                    Engine.OpenMenu(Menus.DebugMenu.Menu);
                    break;
                default:
                    break;

            }
        }
    }
}

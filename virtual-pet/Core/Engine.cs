using virtual_pet.Core.Input;
using virtual_pet.Core.Render;
using virtual_pet.Core.Utils;
using virtual_pet.Core.Menu;

namespace virtual_pet.Core {
    public class Engine {

        string s = "▶◀▲▼✓╠╣║╚╝╔╗↲⇡⇣←↑→↓↔↕";

        private static PlayerController controller = new PlayerController();

        private static Stack<IInputListener> inputListeners = new Stack<IInputListener>();

        private static List<IDisplayable> displayables = new List<IDisplayable>();

        private static Stack<ConsoleMenu> menus = new Stack<ConsoleMenu>();

        private static ConsoleMenu currentMenu;

        private static OptionStrip? currentOtionStrip;

        public static bool IsRunning { get; private set;  } = false;


        public static void Init() {
            controller.Listener = new MainInputListener();
            inputListeners.Push(controller.Listener);
            controller.Listener.IsActive = true;
            IsRunning = true;
        }

        public static void Tick() {
            controller.Tick();
            foreach (IDisplayable d in displayables)
            {
                d.Display(Renderer.PlayBuffer);
            }
            currentMenu?.Display(Renderer.MenuBuffer);
            currentOtionStrip?.Display(Renderer.OptionStripBuffer);
        }

        public static void AddDisplayable(IDisplayable displayable) {
            displayables.Add(displayable);
        }

        public static bool ContainsDisplayable(IDisplayable dislpayable) {
            return displayables.Contains(dislpayable);
        }

        public static void RemoveDisplayable(IDisplayable displayable) {
            displayables.Remove(displayable);
        }
        

        public static void PassInput(IInputListener listener) {
            if (controller.Listener != null)
                controller.Listener.IsActive = false;
            inputListeners.Push(controller.Listener);
            controller.Listener = listener;
            listener.IsActive = true;
            currentOtionStrip = listener.GetOptionStrip();
        }

        public static void PopInput() {
            controller.Listener.IsActive = false;
            controller.Listener = inputListeners.Pop();
            controller.Listener.IsActive = true;
            currentOtionStrip = controller.Listener?.GetOptionStrip();
        }

        public static void OpenMenu(ConsoleMenu consoleMenu) {
            menus.Push(currentMenu);
            currentMenu = consoleMenu;
            PassInput(consoleMenu);
        }

        public static void CloseMenu() {
            currentMenu = menus.Pop();
            PopInput();
        }

    }
    public class MainInputListener : IInputListener {

        public bool IsActive { get; set; }
        public void KeyPressed(ConsoleKeyInfo key) {
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

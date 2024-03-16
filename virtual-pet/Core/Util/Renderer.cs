namespace virtual_pet.Core.Util
{
    public class Renderer
    {
        private const int HeaderHeight = 6;
        private const int DisplayHeight = 20;
        private const int MenuHeight = 10;
        private const int TotalHeight = HeaderHeight + DisplayHeight + MenuHeight;

        public static void Initialize()
        {
            RenderLayout();
        }

        private static void RenderLayout()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.SetWindowSize(80, TotalHeight);

            RenderHeader();
            RenderDisplayArea();
            RenderMenu();
        }

        private static void RenderHeader()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(
@"···········································································
:  ___ ___ __        __                __      ______         __          :
: |   |   |__|.----.|  |_.--.--.---.-.|  |    |   __ \.-----.|  |_.-----. :
: |   |   |  ||   _||   _|  |  |  _  ||  |    |    __/|  -__||   _|__ --| :
:  \_____/|__||__|  |____|_____|___._||__|    |___|   |_____||____|_____| :
···········································································"
            );
        }

        private static void RenderDisplayArea()
        {
            Console.SetCursorPosition(0, HeaderHeight);
        }

        private static void RenderMenu()
        {
            Console.SetCursorPosition(0, HeaderHeight + DisplayHeight);
        }

        public static void RenderDisplayContent(string[] lines)
        {
            Console.SetCursorPosition(0, HeaderHeight + 1); 

            if(lines == null || lines.Length > 10)
            {
                return;
            }

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        public static int RenderMenuContent(List<string> menuItems, Action<int> callback)
        {
            int selectedItemIndex = 0;
            ConsoleKeyInfo key;

            do
            {
                Menu(menuItems, selectedItemIndex);

                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedItemIndex = Math.Max(0, selectedItemIndex - 1);
                        break;

                    case ConsoleKey.DownArrow:
                        selectedItemIndex = Math.Min(menuItems.Count - 1, selectedItemIndex + 1);
                        break;

                    case ConsoleKey.Enter:
                        callback?.Invoke(selectedItemIndex);
                        break;
                }

            } while (key.Key != ConsoleKey.Escape); // You can modify the exit condition if needed

            return selectedItemIndex;
        }

        public static void ClearSection(string section)
        {
            switch (section)
            {
                case "display":
                    Console.SetCursorPosition(0, HeaderHeight);

                    for (int i = 0; i < DisplayHeight; i++)
                    {
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                    }
                    break;
                case "menu":
                    Console.SetCursorPosition(0, DisplayHeight);

                    for (int i = 0; i < MenuHeight; i++)
                    {
                        Console.WriteLine(new string(' ', Console.WindowWidth));
                    }
                    break;
                default:
                    Console.WriteLine("No section with the name " + section + " found!");
                    return;
            }
        }

        private static void Menu(List<string> menuItems, int selectedItemIndex)
        {
            Console.SetCursorPosition(0, HeaderHeight + DisplayHeight);

            if (menuItems == null)
            {
                return;
            }

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == selectedItemIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }

                Console.WriteLine(menuItems[i]);

                Console.ResetColor();
            }
        }
    }
}

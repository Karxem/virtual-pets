using virtual_pet.Core.Manager;

namespace virtual_pet.Core.Util
{
    public class Renderer
    {
        public static event Action BackspacePressed;
        private const int HeaderHeight = 6;
        private const int DisplayHeight = 20;
        private const int MenuHeight = 9;
        private const int TotalHeight = HeaderHeight + DisplayHeight + MenuHeight;

        private static string[] inputBuffer = new string[DisplayHeight];

        public static void Initialize()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.SetWindowSize(80, TotalHeight + 1);

            RenderHeader();
            RenderDisplay();
            RenderMenu();
        }

        private static void RenderHeader()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(
@"···········································································
:  ___ ___ __        __                __      ______         __          :
: |   |   |__|.----.|  |_.--.--.---.-.|  |    |   __ \.-----.|  |_.-----. :
: |   |   |  ||   _||   _|  |  |  _  ||  |    |    __/|  -__||   _|__ --| :
:  \_____/|__||__|  |____|_____|___._||__|    |___|   |_____||____|_____| :
···········································································"
            );
        }

        private static void RenderDisplay()
        {
            // Initial Display Content
        }

        private static void RenderMenu()
        {
            // Initial Menu Content
        }

        public static void RenderDisplayContent(string[] lines)
        {
            Console.SetCursorPosition(0, HeaderHeight + 1);

            if (lines == null || lines.Length > DisplayHeight)
            {
                return;
            }

            int currentLine = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (currentLine == DisplayHeight - 1) // Last line is reserved for inputs
                    break;

                Console.SetCursorPosition(0, HeaderHeight + 1 + currentLine);
                Console.WriteLine(lines[i]);
                currentLine++;
            }
        }

        public static void SetDisplayContent(string content, int lineIndex)
        {
            if (lineIndex < 0 || lineIndex >= DisplayHeight)
            {
                return; // Invalid line index
            }

            Console.SetCursorPosition(0, HeaderHeight + 1 + lineIndex);
            Console.WriteLine(content);
        }

        public static void SendNotification(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, HeaderHeight);
            Console.WriteLine(message);

            Console.ResetColor();
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

                if (key.Key == ConsoleKey.Backspace)
                {
                    BackspacePressed?.Invoke();
                }

            } while (key.Key != ConsoleKey.Backspace);

            ClearSection("menu");  
            return selectedItemIndex;
        }

        public static void SetInputLine(string prompt)
        {
            int cursor = HeaderHeight + DisplayHeight - 2;
            Console.SetCursorPosition(0, cursor);
            Console.Write(prompt);

            inputBuffer[DisplayHeight - 1] = Console.ReadLine();
            Console.SetCursorPosition(0, cursor);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public static string GetInputLine()
        {
            return inputBuffer[DisplayHeight - 1];
        }

        public static void RenderProgressBar(int lineIndex, int interval)
        {
            if (lineIndex < 0 || lineIndex >= DisplayHeight)
            {
                return; // Invalid line index
            }

            int totalSteps = 50;

            int millisecondsPerStep = interval / totalSteps*2;

            for (int i = 0; i <= totalSteps; i++)
            {
                double percent = (double)i / totalSteps;

                Console.SetCursorPosition(0, HeaderHeight + 1 + lineIndex);
                Console.Write("[");
                Console.Write(new string('#', (int)(percent * totalSteps)));
                Console.Write(new string(' ', totalSteps - (int)(percent * totalSteps)));
                Console.Write("] ");
                Console.Write((percent * 100).ToString("0.00") + "%\r");

                Thread.Sleep(millisecondsPerStep);
            }
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
                    Console.SetCursorPosition(0, HeaderHeight + DisplayHeight);

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

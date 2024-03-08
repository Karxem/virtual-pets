namespace virtual_pet.Core.Utils
{
    public class ConsoleMenu
    {
        private List<string> menuItems;
        private int selectedItemIndex;

        public ConsoleMenu(List<string> items)
        {
            menuItems = items;
            selectedItemIndex = 0;
        }

        public int ShowMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                DisplayHeader();
                DisplayMenu();

                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveSelectionUp();
                        break;

                    case ConsoleKey.DownArrow:
                        MoveSelectionDown();
                        break;
                }

            } while (key.Key != ConsoleKey.Enter);

            return selectedItemIndex;
        }

        private void DisplayHeader()
        {
            CenterText("Welcome to Virtual Pet");
            CenterText("-------------------------------------------------------\n");
        }

        private void DisplayMenu()
        {
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

        private void MoveSelectionUp()
        {
            selectedItemIndex = (selectedItemIndex - 1 + menuItems.Count) % menuItems.Count;
        }

        private void MoveSelectionDown()
        {
            selectedItemIndex = (selectedItemIndex + 1) % menuItems.Count;
        }

        private static void CenterText(string text)
        {
            int width = Console.WindowWidth;
            int leftPadding = (width - text.Length) / 2;
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine(text);
        }
    }
}
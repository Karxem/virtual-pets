using virtual_pet.Core.Render;
namespace virtual_pet.Core.Utils
{
    public class ConsoleMenuBuffered
    {
        private List<string> menuItems;
        private int selectedItemIndex;

        private Render.Buffer buffer;
        public ConsoleMenuBuffered(Render.Buffer buffer, List<string> items)
        {
            this.buffer = buffer;
            menuItems = items;
            selectedItemIndex = 0;
        }

        public int ShowMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                buffer.Clear();
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
            buffer.WriteLine("Welcome to Virtual Pet");
            buffer.WriteLine("----------------------------\n");
        }

        private void DisplayMenu()
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == selectedItemIndex)
                {
                    buffer.ForegroundColor = ConsoleColor.Black;
                    buffer.BackgroundColor = ConsoleColor.White;
                }

                buffer.WriteLine(menuItems[i]);

                buffer.ResetColor();
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
    }
}
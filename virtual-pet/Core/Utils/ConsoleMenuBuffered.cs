using virtual_pet.Core.Render;
using virtual_pet.Core.Input;
namespace virtual_pet.Core.Utils
{
    public class ConsoleMenuBuffered : IInputListener
    {
        public delegate void ItemSelected(object sender, int selectedItemIndex);
        public event ItemSelected onItemSelected;


        private List<string> menuItems;
        private int selectedItemIndex;

        private Render.Buffer buffer;
        public ConsoleMenuBuffered(Render.Buffer buffer, ItemSelected onItemSelected, List<string> items)
        {
            this.buffer = buffer;
            this.onItemSelected = onItemSelected;
            menuItems = items;
            selectedItemIndex = 0;
        }

        public void KeyPressed(ConsoleKeyInfo key) {
            buffer.Clear();
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveSelectionUp();
                    break;

                case ConsoleKey.DownArrow:
                    MoveSelectionDown();
                    break;
                case ConsoleKey.Enter:
                    onItemSelected?.Invoke(this, selectedItemIndex);
                    break; 
                default: break;
            }
        }

        public void ShowMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                
                DisplayHeader();
                DisplayMenu();
                Renderer.Render();
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

            //return selectedItemIndex;
        }

        public void Display() {
            DisplayMenu();
        }
        public void Display(Render.Buffer buffer) {
            this.buffer = buffer;
            DisplayMenu();
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
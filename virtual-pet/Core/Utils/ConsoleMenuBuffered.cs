using virtual_pet.Core.Render;
using virtual_pet.Core.Input;
using System.Reflection.Metadata.Ecma335;
using System;
namespace virtual_pet.Core.Utils
{
    public class ConsoleMenuBuffered : IInputListener, IDisplayable
    {
        public delegate void ItemSelected(object sender, int selectedItemIndex);
        public event ItemSelected onItemSelected;

        private OptionStripItem[] optionStripItems =
        {
            new OptionStripItem("Select", ' ', ConsoleKey.Enter),
            new OptionStripItem("Up", ' ', ConsoleKey.UpArrow),
            new OptionStripItem("Down", ' ', ConsoleKey.DownArrow)
        };

        private OptionStrip optionStrip;
        private List<string> menuItems;
        private int selectedItemIndex;
        private int itemsPerPage = 6;
        private int currentPage = 0;

        //public Render.Buffer Buffer { get; set; }

        public ConsoleMenuBuffered(ItemSelected onItemSelected, List<string> items) { 
            optionStrip = new OptionStrip(optionStripItems);
            //this.Buffer = buffer;
            this.onItemSelected = onItemSelected;
            menuItems = items;
            selectedItemIndex = 0;
        }

        public void KeyPressed(ConsoleKeyInfo key) {
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
                case ConsoleKey.Escape:
                    onItemSelected?.Invoke(this, -1);
                    break;
                default: break;
            }
        }


        public OptionStrip? GetOptionStrip() => optionStrip;

        public void Display(Render.Buffer buffer) {
            for (int i = currentPage * itemsPerPage; i < currentPage * itemsPerPage + itemsPerPage && i < menuItems.Count; i++)
            {
                if (i == selectedItemIndex)
                {
                    buffer.ForegroundColor = ConsoleColor.Black;
                    buffer.BackgroundColor = ConsoleColor.Gray;
                }

                buffer.WriteLine(" " + menuItems[i]);

                buffer.ResetColor();
            }
        }

        private void MoveSelectionUp()
        {
            selectedItemIndex = (selectedItemIndex - 1 + menuItems.Count) % menuItems.Count;
            currentPage = selectedItemIndex / itemsPerPage;
        }

        private void MoveSelectionDown()
        {
            selectedItemIndex = (selectedItemIndex + 1) % menuItems.Count;
            currentPage = selectedItemIndex / itemsPerPage;
        }
    }
}
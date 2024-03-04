using virtual_pet.Core.Render;
using virtual_pet.Core.Input;
using System.Reflection.Metadata.Ecma335;
using System;
namespace virtual_pet.Core.Utils
{
    public class ConsoleMenu : IInputListener, IDisplayable
    {
        public delegate void ItemSelected(object sender, int selectedItemIndex);
        public event ItemSelected onItemSelected;



        private OptionStripItem[] optionStripItems =
        {
            new OptionStripItem("Exit", ' ', ConsoleKey.Escape),
            new OptionStripItem("Select", ' ', ConsoleKey.Enter),
            new OptionStripItem("Up", ' ', ConsoleKey.UpArrow),
            new OptionStripItem("Down", ' ', ConsoleKey.DownArrow)
        };

        private OptionStrip optionStrip;
        private List<MenuItemBase> menuItems;
        private int selectedItemIndex;
        private int itemsPerPage = 6;
        private int currentPage = 0;

        public bool IsActive { get; set; }

        //public object SetActive()
        //{
        //    IsActive = true;
        //    return null;
        //}
        //public object SetInactive()
        //{
        //    IsActive = false;
        //    return null;
        //}

        //public Render.Buffer Buffer { get; set; }

        public ConsoleMenu(ItemSelected onItemSelected) {
            this.onItemSelected = onItemSelected;
        }

        public ConsoleMenu(ItemSelected onItemSelected, List<MenuItemBase> items) { 
            optionStrip = new OptionStrip(optionStripItems);
            //this.Buffer = buffer;
            this.onItemSelected = onItemSelected;
            menuItems = items;
            selectedItemIndex = 0;
        }

        public ConsoleMenu(ItemSelected onItemSelected, List<string> items) {
            optionStrip = new OptionStrip(optionStripItems);
            //this.Buffer = buffer;
            this.onItemSelected = onItemSelected;
            menuItems = new List<MenuItemBase>();
            for (int i = 0; i < items.Count; i++)
            {
                ClickItem newItem = null;
                newItem = new ClickItem(i, items[i], delegate { onItemSelected?.Invoke(this, newItem.Id); });
                menuItems.Add(newItem);
            }
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
                case ConsoleKey.LeftArrow:
                    break;
                case ConsoleKey.RightArrow:
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
                menuItems[i].IsSeletected = false;
                if (i == selectedItemIndex && IsActive)
                    menuItems[i].IsSeletected = true;
                menuItems[i].Display(buffer);
                buffer.WriteLine();

                //if (i == selectedItemIndex)
                //{
                //    buffer.ForegroundColor = ConsoleColor.Black;
                //    buffer.BackgroundColor = ConsoleColor.Gray;
                //}

                //buffer.WriteLine(" " + menuItems[i]);

                //buffer.ResetColor();
            }
        }

        public ClickItem CreateClickItem(string text) {
            ClickItem item = new ClickItem(menuItems.Count, text, delegate { onItemSelected?.Invoke(this, menuItems.Count); });
            menuItems.Add(item);
            return item;
        }

        public TextInputItem CreateTextInputItem(TextInput.OnSubmit onSubmit, string text) {
            TextInput input = TextInput.GetTextInput(onSubmit, true);
            TextInputItem item = new TextInputItem(menuItems.Count, text, input);
            menuItems.Add(item);
            return item;
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

        private void MoveSelectionLeft() 
            {

        }
        private void MoveSelectionRight() 
        {

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
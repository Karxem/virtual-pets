using virtual_pet.Core.Render;
using virtual_pet.Core.Input;
using System.Reflection.Metadata.Ecma335;
using System;
using System.Text.RegularExpressions;
namespace virtual_pet.Core.Utils
{
    public class ConsoleMenu : IInputListener, IDisplayable
    {
        public const int ACTION_CLOSE = -1;
        public const int ACTION_MOVE_UP = -2;
        public const int ACTION_MOVE_DOWN = -3;
        public const int ACTION_MOVE_LEFT = -4;
        public const int ACTION_MOVE_RIGHT = -5;


        public delegate void ItemSelected(object sender, int selectedItemIndex);
        public event ItemSelected onItemSelected;

        public bool UseDefaultHandler { get; set; } = true;


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

        public ConsoleMenu() {
            optionStrip = new OptionStrip(optionStripItems);
            menuItems = new List<MenuItemBase>();
        }

        public ConsoleMenu(ItemSelected onItemSelected) {
            this.onItemSelected = onItemSelected;
            optionStrip = new OptionStrip(optionStripItems);
            menuItems = new List<MenuItemBase>();
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
                    if (UseDefaultHandler)
                    {
                        DefaultItemSelected(ACTION_MOVE_UP);
                        break;
                    }
                    onItemSelected?.Invoke(this, ACTION_MOVE_UP);
                    break;

                case ConsoleKey.DownArrow:
                    if (UseDefaultHandler)
                    {
                        DefaultItemSelected(ACTION_MOVE_DOWN);
                        break;
                    }
                    onItemSelected?.Invoke(this, ACTION_MOVE_DOWN);
                    break;
                case ConsoleKey.LeftArrow:
                    if (UseDefaultHandler)
                    {
                        DefaultItemSelected(ACTION_MOVE_LEFT);
                        break;
                    }
                    onItemSelected?.Invoke(this, ACTION_MOVE_LEFT);
                    break;
                case ConsoleKey.RightArrow:
                    if (UseDefaultHandler)
                    {
                        DefaultItemSelected(ACTION_MOVE_RIGHT);
                        break;
                    }
                    onItemSelected?.Invoke(this, ACTION_MOVE_RIGHT);
                    break;
                case ConsoleKey.Enter:
                    if (onItemSelected != null)
                    {
                        onItemSelected?.Invoke(this, selectedItemIndex);
                        break;
                    }
                    ItemClick(selectedItemIndex);
                    break;
                case ConsoleKey.Escape:
                    if (UseDefaultHandler)
                    {
                        DefaultItemSelected(ACTION_CLOSE);
                        break;
                    }
                    onItemSelected?.Invoke(this, ACTION_CLOSE);
                    break;
                default:
                    break;
            }
        }

        /**
         * <summary>
         * call this when default handler is disabled in your onItemSelected callback 
         * to use default functionality like movement and closing menu
         * </summary>
         * <code>
         * call 
         * switch(id){
         * //your handlers
         *  default:  DefaultItemSelected(id); break;
         * }
         * </code>
         */
        public void DefaultItemSelected(int id) {
            switch (id)
            {
                case ACTION_MOVE_UP:
                    MoveSelectionUp();
                    return;
                case ACTION_MOVE_DOWN:
                    MoveSelectionDown();
                    return;
                case ACTION_MOVE_LEFT:
                    MoveSelectionLeft();
                    return;
                case ACTION_MOVE_RIGHT:
                    MoveSelectionRight();
                    return;
                case ACTION_CLOSE:
                    Engine.CloseMenu();
                    return;
                default:
                    return;
            }
        }

        private void ItemClick(int id) {
            if(id <  0 || id > menuItems.Count)
                return;
            MenuItemBase item = menuItems[id];
            if (item == null)
                return;

            if(item is ClickItem)
            {
                (item as ClickItem).FireOnClick();
                return;
            }
            if(item is TextInputItem)
            {
                Engine.PassInput((item as TextInputItem).Input);
                return;
            }
            if(item is SubMenuItem){ 
                Engine.OpenMenu((item as SubMenuItem).Menu);
                return;
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
        public ClickItem CreateClickItem(string text, ClickItem.OnClickEvent onClick, bool useDefaultOnClick = false) {
            ClickItem item = new ClickItem(menuItems.Count, text, onClick);
            if(useDefaultOnClick)
                item.onClick += onClick;
            menuItems.Add(item);
            return item;
        }

        public TextInputItem CreateTextInputItem(string text, bool allowWhitespace = true) {
            return CreateTextInputItem(text, delegate { this.onItemSelected?.Invoke(this, menuItems.Count); }, allowWhitespace);
        }
        public TextInputItem CreateNumberInputItem(string text) {
            return CreateNumberInputItem(text, delegate { this.onItemSelected?.Invoke(this, menuItems.Count); });
        }
        public TextInputItem CreateRegexTextInputItem(string text, string regex) {
            return CreateRegexTextInputItem(text, regex, delegate { this.onItemSelected?.Invoke(this, menuItems.Count); });
        }

        public TextInputItem CreateTextInputItem(string text, TextInput.OnSubmit onSubmit, bool allowWhitespace = true) {
            TextInput input = TextInput.GetTextInput(onSubmit, allowWhitespace);
            TextInputItem item = new TextInputItem(menuItems.Count, text, input);
            menuItems.Add(item);
            return item;
        }
        public TextInputItem CreateNumberInputItem(string text, TextInput.OnSubmit onSubmit) {
            TextInput input = TextInput.GetNumberInput(onSubmit);
            TextInputItem item = new TextInputItem(menuItems.Count, text, input);
            menuItems.Add(item);
            return item;
        }
        public TextInputItem CreateRegexTextInputItem(string text, string regex, TextInput.OnSubmit onSubmit) {
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
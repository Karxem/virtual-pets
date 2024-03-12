using virtual_pet.Core.GameEngine.Common;

namespace virtual_pet.Core.GameEngine.Menu
{
    public class SubMenuItem : MenuItemBase
    {


        public ConsoleMenu Menu { get; private set; }

        public SubMenuItem(int id, string text) : base(id, text) { }

        public void Open()
        {
            GameEngine.OpenMenu(Menu);
        }

        public void Close()
        {
            GameEngine.CloseMenu();
        }

    }
}

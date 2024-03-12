namespace virtual_pet.Core.GameEngine.Menu
{
    public class MainMenu
    {

        public ConsoleMenu Menu { get; private set; }
        public ClickItem ExitItem { get; private set; }

        public MainMenu()
        {
            Menu = new ConsoleMenu();
            ExitItem = Menu.CreateClickItem("Exit Game", onGameExit);

        }


        private void onGameExit(object sender, string text)
        {

        }

    }
}

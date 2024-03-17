using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.Util;

namespace virtual_pet.Core.Manager
{
    public class StateManager
    {
        private static readonly PetManager petManager = new PetManager();
        private static readonly InventoryManager inventoryManager = new InventoryManager();
        private static GameState currentGamestate = new GameState();

        private static List<PetBase> pets = new List<PetBase>();
        private static List<ItemBase> items = new List<ItemBase>();

        public void Initialize()
        {
            currentGamestate = GameState.MainMenu;
            pets = petManager.GetPets();
            items = inventoryManager.GetItems();

            Renderer.Initialize();
            SetMainMenu();
        }

        public static void Shutdown()
        {

        }

        public static GameState GetGamestate()
        {
            return currentGamestate;
        }

        private static void SetMainMenu()
        {
            List<string> menuItems = new List<string>
            {
                "Pet Overview",
                "Exit"
            };

            Renderer.RenderMenuContent(menuItems, onItemSelected);
        }

        private static void onItemSelected(int selectedIndex)
        {
            if (selectedIndex == -1)
            {
                return;
            }

            switch (selectedIndex)
            {
                case 0:
                    UpdateGamestate(GameState.Overview);
                    break;
                case 1:
                    Environment.Exit(0);
                    return;
            }
        }

        private static void UpdateGamestate(GameState gamestate)
        {
            if (gamestate == GameState.MainMenu)
            {
                if (currentGamestate == GameState.Ingame)
                {
                    return;
                }

                SetMainMenu();
            }

            currentGamestate = gamestate;

            switch (gamestate)
            {
                case GameState.Overview:
                    ShowPetOverview(pets);
                    break;
                case GameState.Ingame:
                    break;
                case GameState.Exit:
                    break;
            }
        }

        private static void ShowPetOverview(List<PetBase> pets)
        {
            List<string> menuItems = new List<string>();
            foreach (var pet in pets)
            {
                menuItems.Add($"{pet.Name}");
            }

            Renderer.RenderMenuContent(menuItems, (i) =>
            {
                Renderer.ClearSection("display");

                PetBase pet = pets[i];
                Renderer.SetDisplayContent(pet.ShowPetOverview(), 0);
            });
        }
    }
}

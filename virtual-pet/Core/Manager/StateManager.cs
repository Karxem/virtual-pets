using System.Text;
using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.Util;

namespace virtual_pet.Core.Manager
{
    public class StateManager
    {
        private static readonly PetManager petManager = new PetManager();
        private static readonly InventoryManager inventoryManager = new InventoryManager();
        private Stack<GameState> menuHistory = new Stack<GameState>();
        private GameState currentGameState;

        private List<PetBase> pets = new List<PetBase>();
        private List<ItemBase> items = new List<ItemBase>();

        public StateManager()
        {
            currentGameState = GameState.MainMenu;
            menuHistory.Push(currentGameState);

            pets = petManager.GetPets();
            items = inventoryManager.GetItems();

            Renderer.Initialize();
            Renderer.BackspacePressed += GoBack;

            SetMainMenu();
        }

        public void GoBack()
        {
            Renderer.ClearSection("display");

            if (menuHistory.Count <= 1)
            {
                return;
            }

            menuHistory.Pop();
            currentGameState = menuHistory.Peek();

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    SetMainMenuDisplay();
                    break;
                case GameState.Overview:
                    SetOverviewDisplay();
                    break;
                case GameState.Train:
                    SetMainMenuDisplay();
                    break;
            }
        }

        private void SetMainMenu()
        {
            List<string> menuItems = new List<string>
            {
                "Pet Overview",
                "Train pet",
                "Exit"
            };

            SetMainMenuDisplay();
            Renderer.RenderMenuContent(menuItems, onItemSelected);
        }

        private void onItemSelected(int selectedIndex)
        {
            Renderer.ClearSection("menu");

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
                    UpdateGamestate(GameState.Train);
                    break;
                case 2:
                    Environment.Exit(0);
                    return;
            }
        }

        private void UpdateGamestate(GameState gamestate)
        {
            if (gamestate == GameState.MainMenu || menuHistory.Pop() == GameState.Ingame)
            {
                return;
            }

            currentGameState = gamestate;
            menuHistory.Push(gamestate);

            switch (gamestate)
            {
                case GameState.Overview:
                    ShowPetOverview(pets);
                    break;
                case GameState.Train:
                    TrainPets(pets);
                    break;
                case GameState.Exit:
                    break;
            }
        }

        private void ShowPetOverview(List<PetBase> pets)
        {
            SetOverviewDisplay();
            List<string> menuItems = new List<string>();
            foreach (var pet in pets)
            {
                menuItems.Add($"{pet.Name}");
            }

            PetBase selectedPet = null;
            Renderer.RenderMenuContent(menuItems, (i) =>
            {
                Renderer.ClearSection("display");
                selectedPet = pets[i];
                Renderer.SetDisplayContent(selectedPet.ShowPetOverview(), 0);

                currentGameState = GameState.PetOverview;
                menuHistory.Push(currentGameState);

                List<string> options = new List<string>
                {
                    "Feed",
                    "Play",
                    "Use Item",
                };

                Renderer.ClearSection("menu");
                Renderer.RenderMenuContent(options, (i) =>
                {
                    if (selectedPet == null)
                    {
                        return;
                    }

                    switch (i)
                    {
                        case 0:
                            selectedPet.Eat(20);
                            selectedPet.Drink(20);
                            selectedPet.Sleep(20);

                            Renderer.ClearSection("display");
                            Renderer.SendNotification($"You have fed {selectedPet.Name}!");
                            break;
                        case 1:
                            Renderer.ClearSection("display");
                            selectedPet.GainExperience();
                            break;
                        case 2:
                            List<string> inventory = new List<string>();
                            foreach (var item in items)
                            {
                                inventory.Add($"{item.Name}");
                            }

                            Renderer.RenderMenuContent(inventory, (i) =>
                            {
                                ItemBase selectedItem = items[i];
                                menuHistory.Push(currentGameState);

                                Renderer.ClearSection("display");
                                selectedItem.UseItem(selectedPet);
                                petManager.SavePet(selectedPet);
                                Renderer.SetDisplayContent(selectedPet.ShowPetOverview(), 1);
                            });
                            break;
                    }
                    Renderer.SetDisplayContent(selectedPet.ShowPetOverview(), 1);
                });
            });
        }

        private void TrainPets(List<PetBase> pets)
        {
            SetTrainingDisplay();

            List<string> menuItems = new List<string>();
            foreach (var pet in pets)
            {
                menuItems.Add($"{pet.Name}");
            }

            menuHistory.Push(currentGameState);

            PetBase selectedPet = null;
            Renderer.RenderMenuContent(menuItems, (i) =>
            {
                Renderer.ClearSection("display");
                selectedPet = pets[i];

                Renderer.RenderDisplayContent(new string[] { selectedPet.GetPetSprite() });
                Renderer.SetDisplayContent($"Training with {selectedPet.Name}", 12);
                Renderer.RenderProgressBar(13, 2000);

                selectedPet.GainExperience();
                menuHistory.Push(currentGameState);
            });
        }

        private void SetMainMenuDisplay()
        {
            Renderer.ClearSection("display");
            Console.ForegroundColor = ConsoleColor.Green;
            Renderer.SetDisplayContent(
@"      ////^\\\\
      | ^   ^ |
     @ (o) (o) @       ==========================
      |   <   |        |   Hey you!             |
      |  ___  |        |   How are your pets?   |
       \_____/         ==========================
     ____|  |____
    /    \__/    \
   /              \
  /\_/|        |\_/\
 / /  |        |  \ \
( <   |        |   > )
 \ \  |        |  / /
  \ \ |________| / /
   \ \|        |/ /", 1);
            Console.ResetColor();
        }

        private void SetOverviewDisplay()
        {
            Renderer.ClearSection("display");
            Console.ForegroundColor = ConsoleColor.Green;
            Renderer.SetDisplayContent(
@"      ////^\\\\
      | ^   ^ |
     @ (o) (o) @       ==============================================
      |   <   |        |   Which pet do you want to take care of?   |
      |  ___  |        ==============================================
       \_____/
     ____|  |____
    /    \__/    \
   /              \
  /\_/|        |\_/\
 / /  |        |  \ \
( <   |        |   > )
 \ \  |        |  / /
  \ \ |________| / /
   \ \|        |/ /", 1);
            Console.ResetColor();
        }

        private void SetTrainingDisplay()
        {
            Renderer.ClearSection("display");
            Console.ForegroundColor = ConsoleColor.Green;
            Renderer.SetDisplayContent(
@"      ////^\\\\
      | ^   ^ |
     @ (o) (o) @       =======================================
      |   <   |        |   Which pet do you want to train?   |
      |  ___  |        =======================================
       \_____/
     ____|  |____
    /    \__/    \
   /              \
  /\_/|        |\_/\
 / /  |        |  \ \
( <   |        |   > )
 \ \  |        |  / /
  \ \ |________| / /
   \ \|        |/ /", 1);
            Console.ResetColor();
        }
    }
}

using virtual_pet.Core.Manager;
using virtual_pet.Core.Level.Common;
using virtual_pet.Core.Level;
using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.GameEngine.Common;
using virtual_pet.Core.GameEngine.Menu;
using virtual_pet.Core.GameEngine.Render;
using virtual_pet.Core.Entity.Items;

namespace virtual_pet.Application
{
    public class VirtualPets
    {
        static PetManager petManager = new PetManager();
        static InventoryManager inventoryManager = new InventoryManager();

        static List<string> menuItems = new List<string>
        {
            "Show Pet Overview",
            "Fill all pet stats",
            "Add pet",
            "Add item",
            "Gain experience",
            "Test first level",
            "Exit"
        };

        static List<PetBase> pets = new List<PetBase>();
        static List<ItemBase> items = new List<ItemBase>();

        static void Main()
        {
            Engine.Init();
            Renderer.Init();
            pets = petManager.GetPets();
            items = inventoryManager.GetItems();

            Console.CursorVisible = false;
            Engine.OpenMenu(consoleMenu);

            while (Engine.IsRunning)
            {
                Renderer.ClearBuffers();
                Engine.Tick();
                Renderer.Render();
                Thread.Sleep(16);
            }

            Console.WriteLine("Exiting...");
        }

        public static void onItemSelected(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    ShowPetOverview(pets);
                    break;
                case 1:
                    FillAllPetStats(pets);
                    break;
                case 2:
                    AddNewPet();
                    break;
                case 3:
                    AddNewItem();

                    foreach (var item in items)
                    {
                        Console.WriteLine(item.Name);
                    }
                    break;
                case 4:
                    GainExperience(pets);
                    break;
                case 5:
                    LevelBase level = new TestLevel();
                    // Engine.setCurrentLevel(level);
                    level.StartLevel();
                    break;
                case 6:
                    foreach (var pet in pets)
                    {
                        petManager.SavePet(pet);
                    }

                    Environment.Exit(0);
                    return;
                default:
                    Console.WriteLine("An error occurred");
                    break;
            }

            Console.WriteLine("\nPress Enter to go back to the main menu...");
            Console.ReadLine(); // Stop it, get some help
        }

        private static void ShowPetOverview(List<PetBase> pets)
        {
            Console.Clear();
            Console.WriteLine(SendHeaderText(pets.Count));
            Console.WriteLine("-------------------------------------------------------");

            foreach (var pet in pets)
            {
                if (pet == null)
                {
                    continue;
                }

                Console.WriteLine($"< Name: {pet.Name ?? "Unknown"}, Type: {pet.GetPetType() ?? "Unknown"}, Level: {pet.Level?.Value ?? 0}");
                Console.WriteLine($"< Energy: {pet.Energy?.Value ?? 0}, Attack: {pet.Attack?.Value ?? 0}, Defense: {pet.Defense?.Value ?? 0}");
                Console.WriteLine($"< Health: {pet.Health?.Value ?? 0}, Hunger: {pet.Hunger?.Value ?? 0}, Thirst: {pet.Thirst?.Value ?? 0}\n");
            }
        }


        private static void FillAllPetStats(List<PetBase> pets)
        {
            Console.Clear();
            foreach (var pet in pets)
            {
                var petInstance = petManager.LoadPet(pet.Name);
                petInstance.FillBasicNeeds();
                petManager.SavePet(petInstance);
                Console.WriteLine($"{pet.Name} is filled up again!");
            }
        }

        private static void AddNewPet()
        {
            Console.Clear();
            Console.Write("Name your new pet: ");
            string petName = Console.ReadLine();

            List<string> petTypes = petManager.GetPetTypes();

            Console.WriteLine("Choose a pet type:");
            for (int i = 0; i < petTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {petTypes[i]}");
            }

            int typeIndex = Convert.ToInt32(Console.ReadLine());

            if (typeIndex < 1 || typeIndex > petTypes.Count)
            {
                Console.Clear();
                Console.WriteLine("Invalid pet type selection!");
                return;
            }

            string petType = petTypes[typeIndex - 1];
            PetBase newPet = petManager.CreateNewPetInstance(petName, petType);

            if (newPet == null)
            {
                return;
            }

            petManager.SavePet(newPet);
            Console.Clear();
            Console.WriteLine($"Your new pet {petName} of type {petType} was added");
        }

        private static void AddNewItem()
        {
            Console.Clear();
            ItemBase newItem = new HealingPotion(50);
            newItem.AddItem(1);
            inventoryManager.SaveItem(newItem);

            Console.WriteLine(newItem.Name + " was added to your inventory!");
        }
        private static void GainExperience(List<PetBase> pets)
        {
            Console.Clear();
            foreach (var pet in pets)
            {
                PetBase activePet = petManager.LoadPet(pet.Name);
                activePet.GainExperience();

                petManager.SavePet(activePet);
            }
        }

        private static string SendHeaderText(int count)
        {
            return count == 0 ? "You don't have any pets" : "Your current pets";
        }
    }
}

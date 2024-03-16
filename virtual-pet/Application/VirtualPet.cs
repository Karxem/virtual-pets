using virtual_pet.Core.Manager;
using virtual_pet.Core.Level.Common;
using virtual_pet.Core.Level;
using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.Entity.Items;
using virtual_pet.Core.Util;
using System.Reflection.Emit;
using System.Xml.Linq;

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
            pets = petManager.GetPets();
            items = inventoryManager.GetItems();

            Renderer.Initialize();
            Renderer.RenderMenuContent(menuItems, onItemSelected);
        }

        public static void onItemSelected(int selectedIndex)
        {
            if (selectedIndex == -1)
            {
                return;
            }

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
        }

        private static void ShowPetOverview(List<PetBase> pets)
        {
            Renderer.ClearSection("display");
            List<string> list = new List<string>();
            foreach (var pet in pets) {
                list.Add(pet.ShowPetOverview());
            }

            Renderer.RenderDisplayContent(list.ToArray());
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

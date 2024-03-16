using virtual_pet.Core.Manager;
using virtual_pet.Core.Level.Common;
using virtual_pet.Core.Level;
using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.Entity.Items;
using virtual_pet.Core.Util;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Diagnostics.Metrics;
using System.Numerics;

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
                    LevelBase level = new TestLevel();
                    // Engine.setCurrentLevel(level);
                    level.StartLevel();
                    break;
                case 4:
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

            int currentLine = 0; // Keep track of the current line for rendering each pet overview

            foreach (var pet in pets)
            {
                string petOverview = pet.ShowPetOverview();

                Renderer.SetDisplayContent(petOverview, currentLine);

                currentLine += 4;
            }
        }


        private static void FillAllPetStats(List<PetBase> pets)
        {
            List<string> lines = new List<string>();

            Renderer.ClearSection("display");
            foreach (var pet in pets)
            {
                var petInstance = petManager.LoadPet(pet.Name);
                petInstance.FillBasicNeeds();
                petManager.SavePet(petInstance);
                
                lines.Add($"{pet.Name} is filled up again!");
            }

            Renderer.RenderDisplayContent(lines.ToArray());
        }

        private static void AddNewPet()
        {
            Renderer.ClearSection("display");
            Renderer.SetDisplayContent("Please enter your new pet's name:", 0);
            
            Renderer.SetInputLine("> ");
            string petName = Renderer.GetInputLine();

            Renderer.SetDisplayContent("Choose a pet type:", 1);

            int currentLine = 2;
            List<string> petTypes = petManager.GetPetTypes();
            for (int i = 0; i < petTypes.Count; i++)
            {
                Renderer.SetDisplayContent($"{i + 1}. {petTypes[i]}", currentLine);
                currentLine++;
            }
            
            Renderer.SetInputLine("> ");
            string typeInput = Renderer.GetInputLine();
            Int32.TryParse(typeInput, out int typeIndex);
            
            if (typeIndex < 1 || typeIndex > petTypes.Count)
            {
                throw new InvalidOperationException("Invalid pet type selection!");
            }

            string petType = petTypes[typeIndex - 1];
            PetBase newPet = petManager.CreateNewPetInstance(petName, petType);
            petManager.SavePet(newPet);

            Renderer.SetDisplayContent($"{newPet.Name} with type {newPet.GetPetType()} was added!", currentLine + 1);
        }
    }
}

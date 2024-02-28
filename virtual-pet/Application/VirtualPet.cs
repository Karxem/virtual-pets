using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Managers;
using virtual_pet.Core.Models;
using virtual_pet.Core.Utils;
using virtual_pet.Core.Render;
using virtual_pet.Core.Input;
using virtual_pet.Core;

namespace virtual_pet.Application
{
    public class VirtualPets
    {
        static PlayerController controller = new PlayerController();
        static List<string> menuItems = new List<string>
            {
            "Show Pet Overview",
            "Fill all pet stats",
            "Add a pet",
            "Exit"
            };
        static ConsoleMenuBuffered consoleMenu = new ConsoleMenuBuffered(Renderer.MenuBuffer, onItemSelected, menuItems);
        static PetManager petManager = new PetManager();
        static List<PetModel> pets = new List<PetModel>();
        static bool running = true;

        static void Main()
        {
            Console.CursorVisible = false;
            //int selectedIndex = consoleMenu.ShowMenu();
            pets = petManager.GetPets();
            controller.Listener = consoleMenu;

            while (running)
            {
                //clear buffers
                Renderer.ClearBuffers();
                //fetch input
                controller.Tick();
                //
                consoleMenu.Display();
                //render the scene
                Renderer.Render();
                Thread.Sleep(30);
            }

            Console.WriteLine("Exiting...");

        }


        public static void onItemSelected(object sender, int selectedIndex) {
            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("Your Pets:");
                    Console.WriteLine("-------------------------------------------------------");

                    foreach (var pet in pets)
                    {
                        Console.WriteLine($"Name: {pet.Name}, Health: {pet.Health}, Energy: {pet.Energy}, Hunger: {pet.Hunger}, Thirst: {pet.Thirst}");
                    }
                    break;
                case 1:
                    Console.Clear();

                    foreach (var pet in pets)
                    {
                        var petInstance = petManager.LoadPet(pet.Name);
                        petInstance.FillAll();
                        petManager.SavePet(petInstance);

                        Console.WriteLine($"{pet.Name} is filled up again!");
                    }
                    break;
                case 2:
                    Console.Clear();

                    Console.Write("Name your new Pet: ");
                    string petName = Console.ReadLine();

                    PetBase newPet = new Lenora();
                    newPet.Name = petName;
                    newPet.FillAll();
                    petManager.SavePet(newPet);

                    Console.WriteLine($"Your new pet {petName} was added");
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("An error occured");
                    break;
            }

            Console.WriteLine("\nPress Enter to go back to the main menu...");
            Console.ReadLine();
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

            // Get a dynamically built list of pet types
            List<string> petTypes = PetBase.GetPetTypes();

            // Display the list of pet types
            Console.WriteLine("Choose a pet type:");
            for (int i = 0; i < petTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {petTypes[i]}");
            }

            // Read user input for pet type index
            int typeIndex = Convert.ToInt32(Console.ReadLine());

            // Validate user input and create a new pet instance
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

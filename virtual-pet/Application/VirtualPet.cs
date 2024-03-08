using System;
using System.Collections.Generic;
using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Levels;
using virtual_pet.Core.Managers;
using virtual_pet.Core.Models;
using virtual_pet.Core.Utils;

namespace virtual_pet.Application
{
    public class VirtualPets
    {
        private static readonly PetManager petManager = new PetManager();

        public static void Main()
        {
            List<string> menuItems = new List<string>
            {
                "Show Pet Overview",
                "Fill all pet stats",
                "Add a pet",
                "Gain experience",
                "Test first level",
                "Exit"
            };

            ConsoleMenu consoleMenu = new ConsoleMenu(menuItems);

            while (true)
            {
                int selectedIndex = consoleMenu.ShowMenu();
                var pets = petManager.GetPets();

                if (selectedIndex == -1)
                {
                    continue;
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
                        GainExperience(pets);
                        break;
                    case 4:
                        LevelBase level = new TestLevel();
                        level.StartFight(pets[0].Name);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("An error occurred");
                        break;
                }

                Console.WriteLine("Press Enter to go back to the main menu...");
                Console.ReadLine();
            }
        }

        private static void ShowPetOverview(List<PetModel> pets)
        {
            Console.Clear();
            Console.WriteLine(SendHeaderText(pets.Count));
            Console.WriteLine("-------------------------------------------------------");

            foreach (var pet in pets)
            {
                Console.WriteLine($"⎡ Name: {pet.Name}, Type: {pet.Type}, Level: {pet.Level}");
                Console.WriteLine($"⎢ Energy: {pet.Energy}, Attack: {pet.Attack}, Defense: {pet.Defense}");
                Console.WriteLine($"⎣ Health: {pet.Health}, Hunger: {pet.Hunger}, Thirst: {pet.Thirst}\n");
            }
        }

        private static void FillAllPetStats(List<PetModel> pets)
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


        private static void GainExperience(List<PetModel> pets)
        {
            Console.Clear();
            foreach(var pet in pets)
            {
                PetBase activePet = petManager.LoadPet(pet.Name);
                Random random = new Random();
                activePet.GainExperience(random.Next(2, 10));

                petManager.SavePet(activePet);
            }
        }

        private static string SendHeaderText(int count)
        {
            return count == 0 ? "You don't have any pets" : "Your current pets";
        }
    }
}

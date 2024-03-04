using virtual_pet.Core;
using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Managers;
using virtual_pet.Core.Models;
using virtual_pet.Core.Utils;

namespace virtual_pet.Application
{
    public class VirtualPets
    {
        static void Main()
        {
            List<string> menuItems = new List<string>
            {
            "Show Pet Overview",
            "Fill all pet stats",
            "Add a pet",
            "Exit"
            };

            ConsoleMenu consoleMenu = new ConsoleMenu(menuItems);
            PetManager petManager = new PetManager();

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
                        if (pets.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("You dont have any Pets");
                            Console.WriteLine("-------------------------------------------------------");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Your Pets:");
                            Console.WriteLine("-------------------------------------------------------");

                            foreach (var pet in pets)
                            {
                                Console.WriteLine($"Name: {pet.Name}, Health: {pet.Health}, Energy: {pet.Energy}, Hunger: {pet.Hunger}, Thirst: {pet.Thirst}");
                            }
                        }

                        break;
                    case 1:
                        if (pets.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("You dont have any Pets");
                            Console.WriteLine("-------------------------------------------------------");
                        }
                        foreach (var pet in pets)
                        {
                            var petInstance = petManager.LoadPet(pet.Name);
                            petInstance.FillAll();

                            petManager.SavePet(petInstance);

                            Console.Clear();
                            Console.WriteLine($"{pet.Name} is filled up again!");
                        }
                        break;
                    case 2:
                        Console.Clear ();

                        Console.Write("Name your new Pet: ");
                        string petName = Console.ReadLine();

                        PetBase newPet = new Lenora();
                        newPet.PetName = petName;
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

        }
    }
}
using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Managers;
using virtual_pet.Core.Models;
using virtual_pet.Core.Utils;

namespace virtual_pet.Application
{
    public class VirtualPets
    {
        private static PetManager petManager = new PetManager();

        static void Main()
        {
            List<string> menuItems = new List<string>
            {
            "Show Pet Overview",
            "Fill all pet stats",
            "Option 3",
            "Exit"
            };
            ConsoleMenu consoleMenu = new ConsoleMenu(menuItems);

            int selectedIndex = consoleMenu.ShowMenu();
            var pets = petManager.GetPets();

            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("Your Pets:");
                    Console.WriteLine("-------------------------------------------------------");
                    foreach (var pet in pets)
                    {
                        Console.WriteLine($"Name: {pet.Name}, Energy: {pet.Energy}, Hunger: {pet.Hunger}, Thirst: {pet.Thirst}");
                    }
                    break;
                case 1:
                    foreach (var pet in pets)
                    {
                        var petInstance = petManager.LoadPet(pet.Name);
                        petInstance.Eat(100);
                        petInstance.Drink(100);
                        petInstance.Sleep(100);

                        petManager.SavePet(petInstance);
                        Console.WriteLine($"{pet.Name} is filled up again!");
                    }
                    break;
                case 2:
                    Console.WriteLine("Option 3");
                    break;
                default:
                    Console.WriteLine("An error occured");
                    break;
            }

            Console.ReadLine();
        }
    }
}
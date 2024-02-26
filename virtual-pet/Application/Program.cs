using virtual_pet.Base;
using virtual_pet.Core.Managers;
using virtual_pet.Pets;

public class VirtualPets
{
    public static void Main()
    {
        PetManager petManager = new PetManager();

        var pets = petManager.GetPets();
        Console.WriteLine("Your Pets:");
        Console.WriteLine("-------------------------------------------------------");
        foreach (var pet in pets)
        {
            Console.WriteLine($"Name: {pet.Name}, Energy: {pet.Energy}, Hunger: {pet.Hunger}, Thirst: {pet.Thirst}");
        }

        Console.ReadLine();
    }
}


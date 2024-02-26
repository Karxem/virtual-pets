// See https://aka.ms/new-console-template for more information
using virtual_pet;
using virtual_pet.Base;
// Erste Tests
PetBase pet = new Lenora();
pet.Sleep(100.0);
pet.Eat(100.0);
pet.Drink(100.0);

Console.WriteLine(pet.GetInfo());
pet.Tick();
pet.Tick();
Console.WriteLine(pet.GetInfo());
pet.Sleep(20);
Console.WriteLine(pet.GetInfo());

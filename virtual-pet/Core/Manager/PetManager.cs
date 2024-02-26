using Newtonsoft.Json;
using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Models;


namespace virtual_pet.Core.Managers
{
    internal class PetManager
    {
        // TODO: Find a solution for this issue, since this is not supposed to be hardcoded to my filepath lol
        private const string FilePath = @"E:\Documents\GitHub\virtual-pets\virtual-pet\Assets\pets.json";
        private List<Pet> petModels;

        public PetManager()
        {
            LoadPets();
        }

        public void SavePet(PetBase pet)
        {
            Pet petModel = new Pet(
                name: pet.Name,
                type: pet.GetPetType(),
                energy: pet.Energy.Value,
                hunger: pet.Hunger.Value,
                thirst: pet.Thirst.Value
            );

            // Check if there is an existing pet with the same name
            var existingPet = petModels.Find(p => p.Name == petModel.Name);

            if (existingPet == null)
            {
                petModels.Add(petModel);
            }
            else
            {
                existingPet.Energy = petModel.Energy;
                existingPet.Hunger = petModel.Hunger;
                existingPet.Thirst = petModel.Thirst;
            }

            SavePetsToFile();
        }
 
        public PetBase LoadPet(string name)
        {
            // Load the PetModel from file based on the name
            var loadedPetModel = petModels.Find(p => p.Name == name);

            if (loadedPetModel == null)
            {
                return null;
            }

            // Create a new PetBase instance and set its state based on the loaded PetModel
            PetBase pet = CreatePetInstance(loadedPetModel.Type);
            pet.Name = loadedPetModel.Name;
            pet.Energy.Value = loadedPetModel.Energy;
            pet.Hunger.Value = loadedPetModel.Hunger;
            pet.Thirst.Value = loadedPetModel.Thirst;

            return pet;
        }

        public List<Pet> GetPets()
        {
            return petModels;
        }

        private void LoadPets()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }

            string json = File.ReadAllText(filePath);
            petModels = JsonConvert.DeserializeObject<List<Pet>>(json);
        }

        private void SavePetsToFile()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

            string json = JsonConvert.SerializeObject(petModels, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private PetBase CreatePetInstance(string type)
        {
            switch (type)
            {
                case "Lenora":
                    return new Lenora();
                default:
                    throw new ArgumentException("Invalid pet type");
            }
        }
    }
}

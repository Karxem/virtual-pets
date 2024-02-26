namespace virtual_pet.Core.Models
{
    internal class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Energy { get; set; }
        public double Hunger { get; set; }
        public double Thirst { get; set; }

        public Pet(string name, string type, double energy, double hunger, double thirst)
        {
            Name = name;
            Type = type;
            Energy = energy;
            Hunger = hunger;
            Thirst = thirst;
        }
    }
}

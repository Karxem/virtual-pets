namespace virtual_pet.Core.Models
{
    internal class PetModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Health { get; set; }
        public double Energy { get; set; }
        public double Hunger { get; set; }
        public double Thirst { get; set; }

        public PetModel(string name, string type, double health,double energy, double hunger, double thirst)
        {
            Name = name;
            Type = type;
            Health = health;
            Energy = energy;
            Hunger = hunger;
            Thirst = thirst;
        }
    }
}

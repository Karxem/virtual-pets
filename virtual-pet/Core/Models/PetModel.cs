using System;
using Newtonsoft.Json;

namespace virtual_pet.Core.Models
{
    [Serializable]
    public class PetModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Health { get; set; }
        public double Energy { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public double Hunger { get; set; }
        public double Thirst { get; set; }
        public double Experience { get; set; }
        public double Level { get; set; }

        public PetModel(
            string name,
            string type,
            double health,
            double energy,
            double attack,
            double defense,
            double hunger,
            double thirst,
            double exp,
            double level)
        {
            Name = name;
            Type = type;
            Health = health;
            Energy = energy;
            Attack = attack;
            Defense = defense;
            Hunger = hunger;
            Thirst = thirst;
            Experience = exp;
            Level = level;
        }
    }
}

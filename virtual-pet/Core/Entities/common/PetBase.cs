using System.Reflection;
using virtual_pet.Core.Models;

namespace virtual_pet.Core.Entities.Common
{
    internal abstract class PetBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public StatModel Health { get; } = new StatModel(0, 100);
        public StatModel Energy { get; } = new StatModel(0, 100);
        public StatModel Attack { get; } = new StatModel(0, 100);
        public StatModel Defense { get; } = new StatModel(0, 100);

        public StatModel Hunger { get; } = new StatModel(0, 100);
        public StatModel Thirst { get; } = new StatModel(0, 100);

        public StatModel Experience { get; } = new StatModel(0, 1000);
        public StatModel Level { get; } = new StatModel(0, 100);

        private static Random random = new Random();

        public void Heal(double amount) => Health.Value += amount;
        public void Sleep(double amount) => Energy.Value += amount;
        public void Drink(double amount) => Thirst.Value += amount;
        public void Eat(double amount) => Hunger.Value += amount;

        public void AddExp(double amount)
        {
            Experience.Value += amount;
        }

        public void TakeDamage(double amount) => Health.Value -= amount;

        public bool IsAlive() => Health.Value > 0.0;

        public void InitPetBaseStats()
        {
            Level.Value = 1;
            Experience.Value = 0;
            Attack.Value = 8.0;
            Defense.Value = 5.0;
            Health.Value = 100;
            Energy.Value = 100;
            Thirst.Value = 100;
            Hunger.Value = 100;
        }

        public void FillBasicNeeds()
        {
            Health.Value += 100;
            Energy.Value += 100;
            Thirst.Value += 100;
            Hunger.Value += 100;
        }

        public void Tick()
        {
            Random random = new Random();
            Health.Value -= random.Next(0, 10);
            Energy.Value -= random.Next(0, 10);
            Hunger.Value -= random.Next(0, 10);
            Thirst.Value -= random.Next(0, 10);
            Experience.Value -= random.Next(0, 5);
        }

        public void GainExperience()
        {
            var amount = random.Next(2, 10);
            Experience.Value += amount;
            Console.WriteLine($"{Name} gained {amount} experience.");

            CheckLevelUp();
        }


        public static List<string> GetPetTypes()
        {
            List<string> types = new List<string>();
            foreach (Type type in Assembly.GetAssembly(typeof(PetBase)).GetTypes().Where(myType => !myType.IsAbstract && myType.IsSubclassOf(typeof(PetBase))))
            {
                types.Add(type.Name);
            }
            types.Sort();
            return types;
        }


        private void CheckLevelUp()
        {
            double lvlUpThreshold = Level.Value * 100;

            if (Experience.Value < lvlUpThreshold)
                return;

            Level.Value++;
            Experience.Value -= lvlUpThreshold;

            Console.WriteLine($"{Name} leveled up to Level {Level.Value}!");
        }

        public abstract string GetPetType();
        public abstract void UseAbility();
    }
}

using virtual_pet.Core.Models;


namespace virtual_pet.Core.Entities.Common
{
    internal abstract class PetBase
    {
        public string PetName { get; set; }
        public StatModel Health = new StatModel(0, 100);
        public StatModel Energy = new StatModel(0, 100);
        public StatModel Attack = new StatModel(0, 100);
        public StatModel Defense = new StatModel(0, 100);

        // Basicneed stat properties
        public StatModel Hunger = new StatModel(0, 100);
        public StatModel Thirst = new StatModel(0, 100);

        // Level stat properties
        public StatModel Experience = new StatModel(0, 10000);
        public StatModel Level = new StatModel(0, 100);

        public void Heal(double amount)
        {
            Health.Value += amount;
        }
        public void Sleep(double amount)
        {
            Energy.Value += amount;
        }
        public void Drink(double amount)
        {
            Thirst.Value += amount;
        }
        public void Eat(double amount)
        {
            Hunger.Value += amount;
        }

        public void AddExp(double amount)
        {
            Experience.SetRange(0, 1000);
            Experience.Value += amount;
        }

        public void TakeDamage(double amount)
        {
            Health.Value -= amount;
        }

        public void FillAll()
        {
            Health.Value += 100;
            Energy.Value += 100;
            Thirst.Value += 100;
            Hunger.Value += 100;
            Attack.Value += 10;
            Defense.Value += 5;
            Level.Value += 1;
        }

        public void GainExperience(double amount)
        {
            Experience.Value += amount;
            Console.WriteLine($"{PetName} gained {amount} experience.");

            // Check if the pet should level up
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            double lvlUpThreshhold = Level.Value * 100;

            if (Experience.Value < lvlUpThreshhold)
            {
                return;
            }

            Level.Value++;
            Experience.Value -= lvlUpThreshhold;

            Console.WriteLine($"{PetName} leveled up to Level {Level.Value}!");
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

        public bool IsAlive()
        {
            switch (Health.Value)
            {        
                case > 0.0:
                    return true;
                case <= 0.0:
                    return false;
                default:
                    return true;
            }
        }

        public string GetInfo()
        {
            return $"[ Health: {Health.Value},Energy: {Energy.Value}, Hunger: {Hunger.Value}, Thirst: {Thirst.Value} ]";
        }

        // Abstract method to get the pet type
        public abstract string GetPetType();
    }
}

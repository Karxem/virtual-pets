using virtual_pet.Core.Models;


namespace virtual_pet.Core.Entities.Common
{
    internal abstract class PetBase
    {
        public string PetName {  get; set; }
        public StatModel Health = new(0, 100);
        public StatModel Energy = new(0, 100);
        public StatModel Hunger = new(0, 100);
        public StatModel Thirst = new(0, 100);

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

        public void FillAll()
        {
            Health.Value += 100;
            Energy.Value += 100;
            Thirst.Value += 100;
            Hunger.Value += 100;
        }

        // TODO: We should add dynamic handling here with a random value inside a range, since hard coded values are meh
        public void Tick()
        {
            Random random = new Random();

            Health.Value -= random.Next(0, 10);
            Energy.Value -= random.Next(0, 10);
            Hunger.Value -= random.Next(0, 10);
            Thirst.Value -= random.Next(0, 10);
        }

        public string GetInfo()
        {
            return $"[ Health: {Health.Value},Energy: {Energy.Value}, Hunger: {Hunger.Value}, Thirst: {Thirst.Value} ]";
        }

        // Abstract method to get the pet type
        public abstract string GetPetType();
    }
}

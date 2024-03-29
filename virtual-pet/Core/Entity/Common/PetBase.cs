﻿using System.Text;
using virtual_pet.Core.Manager;
using virtual_pet.Core.Model;
using virtual_pet.Core.Util;

namespace virtual_pet.Core.Entity.Common
{
    public abstract class PetBase
    {
        private static readonly PetManager petManager = new PetManager();
        private static Random random = new Random();

        public string Name { get; set; }
        public string Type { get; set; }
        public StatModel Health { get; set; } = new StatModel(0, 100);
        public StatModel Energy { get; set; } = new StatModel(0, 100);
        public StatModel Attack { get; set; } = new StatModel(0, 100);
        public StatModel Defense { get; set; } = new StatModel(0, 100);

        public StatModel Hunger { get; set; } = new StatModel(0, 100);
        public StatModel Thirst { get; set; } = new StatModel(0, 100);

        public StatModel Experience { get; set; } = new StatModel(0, 1000);
        public StatModel Level { get; set; } = new StatModel(0, 100);

        public void Heal(double amount) => Health.Value += amount;
        public void Sleep(double amount) => Energy.Value += amount;
        public void Drink(double amount) => Thirst.Value += amount;
        public void Eat(double amount) => Hunger.Value += amount;

        public void AddExp(double amount) => Experience.Value += amount;
        public void TakeDamage(double amount) => Health.Value -= amount;

        public bool IsAlive() => Health.Value > 0.0;

        public void InitPetBaseStats()
        {
            Level.Value = 1;
            Experience.Value = 0;
            Health.Value = 100;
            Energy.Value = 100;
            Thirst.Value = 100;
            Hunger.Value = 100;

            Attack.Value = random.Next(5, 20);
            Defense.Value = random.Next(5, 20);
        }

        public void FillBasicNeeds()
        {
            Health.Value += 100;
            Energy.Value += 100;
            Thirst.Value += 100;
            Hunger.Value += 100;
        }

        public void GainExperience()
        {
            var amount = random.Next(2, 10);
            Experience.Value += amount;
            Renderer.SendNotification($"{Name} gained {amount} experience.");

            CheckLevelUp();
        }

        public void GameTick()
        {
            Random random = new Random();
            Health.Value -= random.Next(0, 10);
            Energy.Value -= random.Next(0, 10);
            Hunger.Value -= random.Next(0, 10);
            Thirst.Value -= random.Next(0, 10);
            Experience.Value -= random.Next(0, 5);
        }

        public string ShowPetOverview()
        {
            StringBuilder overviewBuilder = new StringBuilder();
            overviewBuilder.AppendLine(GetPetSprite() + "\n");
            overviewBuilder.AppendLine($"< Name: {Name}, Type: {GetPetType()}, Level: {Level.Value}");
            overviewBuilder.AppendLine($"< Energy: {Energy.Value}, Attack: {Attack.Value}, Defense: {Defense.Value}");
            overviewBuilder.Append($"< Health: {Health.Value}, Hunger: {Hunger.Value}, Thirst: {Thirst.Value}");

            return overviewBuilder.ToString();
        }

        private void CheckLevelUp()
        {
            double lvlUpThreshold = Level.Value * 100;

            if (Experience.Value < lvlUpThreshold)
                return;

            Level.Value++;
            Experience.Value -= lvlUpThreshold;

            Energy.Value = Energy.Value + 3;
            Attack.Value = Attack.Value + 5;
            Defense.Value = Defense.Value + 2;
            petManager.SavePet(this);

            Console.WriteLine($"{Name} leveled up to Level {Level.Value}!");
        }

        public abstract string GetPetType();
        public abstract string GetPetSprite();
        public abstract void UseAbility();
    }
}

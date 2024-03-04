using System;
using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Entities.Pets;
using virtual_pet.Core.Managers;

namespace virtual_pet.Core.Entities.Common
{
    public abstract class LevelBase
    {
        private static readonly PetManager petManager = new PetManager();

        public void StartFight(string petName)
        {
            PetBase entity = CreateEntity();
            PetBase playerPet;

            if (petName == null)
            {
                return;
            }

            playerPet = petManager.LoadPet(petName);

            if (entity == null || playerPet == null)
            {
                return;
            }

            Console.Clear();
            Console.WriteLine($"{playerPet.PetName} encounters {entity.PetName}!\n");

            while (playerPet.IsAlive() && entity.IsAlive())
            {
                PlayerTurn(playerPet, entity);
                if (!entity.IsAlive()) break;

                EnemyTurn(playerPet, entity);
            }

            Console.Clear();
            if (!entity.IsAlive())
            {
                Random random = new Random();
                playerPet.GainExperience(random.Next(2, 10));
            }

            Console.WriteLine(playerPet.IsAlive() ? $"{playerPet.PetName} wins!" : $"{entity.PetName} wins!");
        }

        private void PlayerTurn(PetBase playerPet, PetBase enemyPet)
        {
            Console.WriteLine($"Your turn, {playerPet.PetName}!\n");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Use Item");
            Console.WriteLine("3. Run");

            int choice = GetPlayerChoice(1, 3);

            switch (choice)
            {
                case 1:
                    int damageDealt = CalculateDamage(playerPet, enemyPet);
                    enemyPet.TakeDamage(damageDealt);

                    Console.Clear();
                    Console.WriteLine($"\n{playerPet.PetName} dealt {damageDealt} damage to {enemyPet.PetName}!");
                    break;

                case 2:
                    // Logic for using items here
                    Console.WriteLine("You used an item!");
                    break;
                case 3:
                    Console.WriteLine("You ran away from the battle!");
                    return;
                default:
                    break;
            }
        }

        private void EnemyTurn(PetBase playerPet, PetBase enemyPet)
        {
            int damageDealt = CalculateDamage(enemyPet, playerPet);
            playerPet.TakeDamage(damageDealt);
            petManager.SavePet(playerPet);

            Console.WriteLine($"{enemyPet.PetName} dealt {damageDealt} damage to {playerPet.PetName}!\n");
        }

        private int GetPlayerChoice(int minValue, int maxValue)
        {
            int choice;
            while (true)
            {
                Console.Write("\nEnter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= minValue && choice <= maxValue)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
            return choice;
        }

        private int CalculateDamage(PetBase attacker, PetBase defender)
        {
            double damage = attacker.Attack.Value - defender.Defense.Value;
            return Math.Max(Convert.ToInt32(damage), 0);
        }

        private static PetBase CreateEntity()
        {
            PetBase entityPet = new Lenora();
            entityPet.PetName = "Entity";
            entityPet.FillBasicNeeds();

            return entityPet;
        }
    }
}

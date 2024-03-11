using virtual_pet.Core.Entities.Common;
using virtual_pet.Core.Entities.Items;
using virtual_pet.Core.Managers;

namespace virtual_pet.Core.Manager
{
	internal class CombatManager
	{
        private static readonly PetManager petManager = new PetManager();
        private static readonly InventoryManager inventoryManager = new InventoryManager();
        
        private static List<ItemBase> items;
        private static Random random;

        public CombatManager()
        {
            random = new Random();
            items = inventoryManager.GetItems();
        }
        
        public void HandleFight(List<PetBase> pets)
        {
            PetBase entity = CreateEntity();
            PetBase playerPet;

            if (pets == null)
            {
                return;
            }

            Console.Clear();
            Console.WriteLine("You are being attacked! Choose a pet:");
            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pets[i].Name}");
            }

            int petIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            if (petIndex > pets.Count)
            {
                return;
            }

            playerPet = pets[petIndex];

            if (entity == null || playerPet == null)
            {
                return;
            }

            Console.Clear();
            Console.WriteLine($"{playerPet.Name} encounters {entity.Name}!\n");

            while (playerPet.IsAlive() && entity.IsAlive())
            {
                PlayerTurn(playerPet, entity);
                if (!entity.IsAlive()) break;

                EnemyTurn(playerPet, entity);
            }

            Console.Clear();
            if (!entity.IsAlive())

            {
                playerPet.GainExperience();
            }

            Console.WriteLine(playerPet.IsAlive() ? $"{playerPet.Name} wins!" : $"{entity.Name} wins!");
        }

        private void PlayerTurn(PetBase playerPet, PetBase enemyPet)
        {
            Console.WriteLine($"Your turn, {playerPet.Name}!\n");
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
                    playerPet.UseAbility();
                    Console.WriteLine($"\n{playerPet.Name} dealt {damageDealt} damage to {enemyPet.Name}!");
                    break;
                case 2:
                    Console.Clear();
                    if(items.Count <= 0)
                    {
                        Console.WriteLine("There are no items in your inventory!");
                        break;
                    }

                    Console.WriteLine("Which item do you want to use?");
                    for (int i = 0; i < items.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {items[i].Name}");
                    }

                    int itemIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (itemIndex > items.Count)
                    {
                        return;
                    }

                    ItemBase item = inventoryManager.LoadItem(items[itemIndex].Name);
                    item.UseItem(playerPet);
                    break;
                case 3:
                    Console.WriteLine("You ran away from the battle!");
                    return;
                default:
                    Console.WriteLine("Choice was not in range");
                    break;
            }
        }

        private void EnemyTurn(PetBase playerPet, PetBase enemyPet)
        {
            int damageDealt = CalculateDamage(enemyPet, playerPet);
            playerPet.TakeDamage(damageDealt);
            petManager.SavePet(playerPet);

            Console.WriteLine($"{enemyPet.Name} dealt {damageDealt} damage to {playerPet.Name}!\n");
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
            List<string> types = petManager.GetPetTypes();

            if (types == null)
            {
                Console.WriteLine("No pet types could be found.");
                return null;
            }

            string type = types[random.Next(0, types.Count)];
            PetBase entityPet = petManager.CreateNewPetInstance(type, type);

            return entityPet;
        }
    }
}


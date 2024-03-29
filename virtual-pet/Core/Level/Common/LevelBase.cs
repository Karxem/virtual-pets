﻿using virtual_pet.Core.Manager;
using virtual_pet.Core.Model;
using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.Level.Enums;

namespace virtual_pet.Core.Level.Common
{
    public abstract class LevelBase
    {
        private static readonly PetManager petManager = new PetManager();
        private static readonly CombatManager combatManager = new CombatManager();
        private List<PetBase> pets;

        public abstract string Name { get; }
        public abstract bool HasCity { get; }
        public abstract bool HasShop { get; }

        public Shop OpenedShop;
        public bool IsShopOpen = false;

        public LevelBase()
        {
            pets = petManager.GetPets();
        }

        public void StartLevel()
        {
            //LevelEvent e = EventGenerator.GenerateRandomEvent();
            //Renderer.PlayBuffer.WriteLine(e.ToString());

            HandleEvent(LevelEvent.ShopVisit);//e);
        }

        private void HandleEvent(LevelEvent e)
        {
            switch (e)
            {
                case LevelEvent.WildPetAttack:
                    StartFight();
                    break;
                case LevelEvent.ShopVisit:
                    VisitShop();
                    break;
                default:
                    Console.WriteLine("Invalid LevelEvent was passed.");
                    break;
            }
        }

        public void OpenShop(Shop s)
        {
            if (IsShopOpen)
            {
                return;
            }

            OpenedShop = s;
            IsShopOpen = true;
        }

        public void CloseShop()
        {
            if (!IsShopOpen)
                return;
            OpenedShop = null;
            IsShopOpen = false;
        }

        public abstract void StartFight();
        public abstract void VisitShop();
    }
}

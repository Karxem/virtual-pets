﻿using virtual_pet.Core.Manager;
using virtual_pet.Core.Model;
using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.GameEngine.Render;
using virtual_pet.Core.GameEngine.Common;
using virtual_pet.Core.Level.Enums;

namespace virtual_pet.Core.Level.Common
{
    public abstract class LevelBase : IDisplayable
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

        public virtual void Display(GameEngine.Render.Buffer buffer)
        {
            if (IsShopOpen)
            {
                OpenedShop.Display(buffer);
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
            Engine.PassInput(s);
        }

        public void CloseShop()
        {
            if (!IsShopOpen)
                return;
            OpenedShop = null;
            IsShopOpen = false;
            Engine.PopInput();
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
                    Renderer.PlayBuffer.WriteLine("Invalid LevelEvent was passed.");
                    break;
            }
        }

        public abstract void StartFight();
        public abstract void VisitShop();
    }
}

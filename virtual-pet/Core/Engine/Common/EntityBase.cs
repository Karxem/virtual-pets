﻿using System.Numerics;
using virtual_pet.Core.Engine.Input;
using virtual_pet.Core.Engine.Render;

namespace virtual_pet.Core.Engine.Common
{
    internal abstract class EntityBase : IInputListener, IDisplayable
    {

        public bool IsActive { get; set; }

        public Vector2 Position { get; set; } = new Vector2();
        public Vector2 Direction { get; set; } = new Vector2();

        private OptionStrip optionStrip = new OptionStrip(
        //new OptionStripItem()
        );

        public bool Alive { get; private set; }

        public void Display(Render.Buffer buffer)
        {

        }

        public OptionStrip? GetOptionStrip() => optionStrip;

        public virtual void Tick() { }

        public virtual bool HitTest() { return false; }

        public virtual void KeyPressed(ConsoleKeyInfo key) { }
    }
}

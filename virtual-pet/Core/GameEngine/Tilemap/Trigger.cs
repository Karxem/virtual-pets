using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using virtual_pet.Core.GameEngine.Collidable;

namespace virtual_pet.Core.GameEngine.Tilemap
{
    public class Trigger : ICollidable
    {
        public TileMap Body { get; set; }

        public string TriggerMessage = "";

        public Trigger()
        {

        }

        public CollisionResult Colliding(ICollidable collidable)
        {
            return CollisionResult.NoCollision;
        }
    }
}

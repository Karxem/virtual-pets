using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Entities.Common {
    public class Trigger : ICollidable {
        public TileMap Body { get; set; }

        public string TriggerMessage = "";

        public Trigger() { 
            
        }

        public CollisionResult Colliding(ICollidable collidable) {
            return CollisionResult.NoCollision;
        }
    }
}

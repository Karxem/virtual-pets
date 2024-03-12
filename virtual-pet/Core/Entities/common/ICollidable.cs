using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.Entities.Common {
    public interface ICollidable {

        public TileMap Body { get; set; } 
        public CollisionResult Colliding(ICollidable collidable);
        public bool CollisionEnabled() => false;

    }
}
